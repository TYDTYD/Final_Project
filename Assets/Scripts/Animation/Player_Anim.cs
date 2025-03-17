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

            // 0.5초 후 IsDamaged를 다시 false로 설정
            damageResetSubscription = Observable.Timer(TimeSpan.FromSeconds(GroggiTime))
                .Subscribe(_ => IsDamaged.Value = false);
        }
        void SetState(State newState)
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
                TriggerAnimation(currentState, previousState);
                previousState = currentState;
            }

            bool isJumpPressed = Input.GetKey(InputHandler.JumpKey);
            bool isRightPressed = Input.GetKey(InputHandler.RightKey);
            bool isLeftPressed = Input.GetKey(InputHandler.LeftKey);
            bool isDownPressed = Input.GetKey(InputHandler.DownKey);

            // 생사 여부
            if (Stage_UI_View.Instance.GetHp <= 0)
            {
                SetState(State.Death_State);
                return;
            }

            if (Stage_UI_View.Instance.GetHp != preHealth)
            {
                SetState(State.Damage_State);
                preHealth = Stage_UI_View.Instance.GetHp;
                return;
            }

            // 사다리 여부
            if (GetPlayer_Rigidbody.GetClimbing)
            {
                SetState(isJumpPressed ? State.Jump_State : State.Ladder_State);
                return;
            }

            if (CurrentState == State.Edge_State)
            {
                if (isJumpPressed)
                {
                    SetState(State.Jump_State);
                    return;
                }
                if (isDownPressed)
                {
                    SetState(State.Fall_State);
                }
                return;
            }

            // 공중 여부
            if (!GetPlayer_Rigidbody.GetGrounded)
            {
                BeforeGrounded = false;
                if (GetPlayer_Right_Flip.GetEdgeDetact)
                {
                    if (!isRightPressed)
                        return;
                    CurrentState = State.Edge_State;
                    return;
                }
                if (GetPlayer_Left_Flip.GetEdgeDetact)
                {
                    if (!isLeftPressed)
                        return;
                    CurrentState = State.Edge_State;
                    return;
                }
                if (isJumpPressed)
                {
                    CurrentState = State.Jump_State;
                    return;
                }
                CurrentState = State.Fall_State;
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
                    SetState(State.Idle_State);
                }
                return;
            }

            if (CurrentState == State.Attack_State)
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
                    SetState(State.SittingStart_State);
                    BeforeSitting = true;
                    return;
                }

                if (isRightPressed || isLeftPressed)
                {
                    SetState(State.SittingMove_State);
                    isSittingMoved = true;
                    SittingTime = 0f;
                    return;
                }
                else
                {
                    SetState(State.Sitting_State);
                    isSittingMoved = false;
                    SittingTime += Time.deltaTime;
                }
                return;
            }

            BeforeSitting = false;
            SittingTime = 0f;

            if (isJumpPressed)
            {
                SetState(State.Jump_State);
                return;
            }

            if (isRightPressed || isLeftPressed)
            {
                SetState(State.Move_State);
                return;
            }

            SetState(State.Idle_State);
        }
        public float GetSittingTime => SittingTime;
        void CheckState()
        {
            bool isJumpPressed = Input.GetKey(InputHandler.JumpKey);
            bool isRightPressed = Input.GetKey(InputHandler.RightKey);
            bool isLeftPressed = Input.GetKey(InputHandler.LeftKey);
            bool isDownPressed = Input.GetKey(InputHandler.DownKey);

            if (Stage_UI_View.Instance.GetHp <= 0)
            {
                SetState(State.Death_State);
                return;
            }

            if (IsDamaged.Value)
            {
                SetState(State.Damage_State);
                return;
            }

            if (GetPlayer_Rigidbody.GetClimbing)
            {
                SetState(isJumpPressed ? State.Jump_State : State.Ladder_State);
                return;
            }

            if (!GetPlayer_Rigidbody.GetGrounded)
            {
                HandleAirState(isJumpPressed, isRightPressed, isLeftPressed);
                return;
            }

            HandleGroundState(isJumpPressed, isRightPressed, isLeftPressed, isDownPressed);
        }
        void HandleAirState(bool isJumpPressed, bool isRightPressed, bool isLeftPressed)
        {
            if (GetPlayer_Right_Flip.GetEdgeDetact && !isRightPressed ||
                GetPlayer_Left_Flip.GetEdgeDetact && !isLeftPressed)
            {
                SetState(State.Edge_State);
                return;
            }

            SetState(isJumpPressed ? State.Jump_State : State.Fall_State);
        }
        void HandleGroundState(bool isJumpPressed, bool isRightPressed, bool isLeftPressed, bool isDownPressed)
        {
            if (isDownPressed)
            {
                HandleSittingState(isRightPressed, isLeftPressed);
                return;
            }

            if (isJumpPressed)
            {
                SetState(State.Jump_State);
                return;
            }

            if (isRightPressed || isLeftPressed)
            {
                SetState(State.Move_State);
                return;
            }

            SetState(State.Idle_State);
        }
        void HandleSittingState(bool isRightPressed, bool isLeftPressed)
        {

        }  
    }
}
