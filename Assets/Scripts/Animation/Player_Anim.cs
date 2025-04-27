namespace player
{
    using UnityEngine;
    using UniRx;
    using System;
    public partial class Player : MonoBehaviour
    {
        ReactiveProperty<bool> IsDamaged = new ReactiveProperty<bool>(false);
        IDisposable damageResetSubscription;

        float GroggiTime = 0.5f;
        float AttackTime = 0f;
        float SittingTime = 0f;
        int preHealth = 0;
        bool isSittingMoved = false;
        bool BeforeGrounded = false;
        bool BeforeSitting = false;
        bool isAttack = false;
        private void ResetDamageState()
        {
            // 기존 타이머가 있으면 초기화
            damageResetSubscription?.Dispose();
            IsDamaged.Value = true;
            // 0.5초 후 IsDamaged를 다시 false로 설정
            damageResetSubscription = Observable.Timer(TimeSpan.FromSeconds(GroggiTime))
                .Subscribe(_ => IsDamaged.Value = false);
        }
        void SetState(IState newState)
        {
            if (CurrentState == newState) return;
            currentState = newState;
        }
        private void Start() => preHealth = Stage_UI_View.Instance.GetHp;
        private void OnEnable() => UpdateManager.Instance.SubscribeUpdate(UpdateMethod);
        private void OnDisable() => UpdateManager.Instance.UnSubscribeUpdate(UpdateMethod);
        void UpdateMethod()
        {
            if (currentState != previousState)
            {
                Debug.Log(currentState);
                TriggerAnimation(currentState, previousState);
                SetAnimation(currentState, previousState);
                previousState = currentState;
            }

            bool isJumpPressed = GetPlayer_Input.GetKeyPress(GetPlayer_Input.PlayerKey.Jump);
            bool isUpPressed = GetPlayer_Input.GetKeyPress(GetPlayer_Input.PlayerKey.Up);
            bool isRightPressed = GetPlayer_Input.GetKeyPress(GetPlayer_Input.PlayerKey.Right);
            bool isLeftPressed = GetPlayer_Input.GetKeyPress(GetPlayer_Input.PlayerKey.Left);
            bool isDownPressed = GetPlayer_Input.GetKeyPress(GetPlayer_Input.PlayerKey.Down);

            // 생사 여부
            if (Stage_UI_View.Instance.GetHp <= 0)
            {
                SetState(DeathState);
                return;
            }

            if (Stage_UI_View.Instance.GetHp != preHealth)
            {
                SetState(DamageState);
                ResetDamageState();
                preHealth = Stage_UI_View.Instance.GetHp;
                return;
            }

            // 사다리 여부
            if (GetPlayer_Rigidbody.GetClimbing)
            {
                SetState(isJumpPressed ? JumpState: LadderState);
                return;
            }

            // 공중 여부
            if (!GetPlayer_Rigidbody.GetGrounded)
            {
                BeforeGrounded = false;
                if (isJumpPressed)
                {
                    CurrentState = JumpState;
                    return;
                }
                CurrentState = FallState;
                return;
            }

            // 착지 여부
            if (!BeforeGrounded)
            {
                BeforeGrounded = true;
                return;
            }

            // 공격 여부
            if (isAttack)
            {
                GetPlayer_Attack.GetBox.enabled = true;
                AttackTime -= Time.deltaTime;
                if (AttackTime <= 0f)
                {
                    GetPlayer_Attack.GetBox.enabled = false;
                    isAttack = false;
                    AttackTime = 0f;
                    SetState(IdleState);
                }
                return;
            }

            if (CurrentState == AttackState)
            {
                isAttack = true;
                AttackTime = 0.2f;
                return;
            }

            // todo 모서리에서 점프, 위로 다시 올라가기 작업 고려 애니메이션 클립 추가

            // 앉기 여부
            if (isDownPressed)
            {
                if (!BeforeSitting)
                {
                    SetState(SittingStartState);
                    BeforeSitting = true;
                    return;
                }

                if (isRightPressed || isLeftPressed)
                {
                    SetState(SittingMoveState);
                    isSittingMoved = true;
                    SittingTime = 0f;
                    return;
                }
                else
                {
                    SetState(SittingState);
                    isSittingMoved = false;
                    SittingTime += Time.deltaTime;
                }
                return;
            }

            BeforeSitting = false;
            SittingTime = 0f;

            if (isJumpPressed)
            {
                SetState(JumpState);
                return;
            }

            if (isRightPressed || isLeftPressed)
            {
                SetState(MoveState);
                return;
            }

            SetState(IdleState);
        }
        public float GetSittingTime => SittingTime;
        public bool GetDamaged => IsDamaged.Value;
    }
}
