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
        float LandTime = 0f;
        float AttackTime = 0f;
        float SittingTime = 0f;
        bool isSittingMoved = false;
        bool BeforeGrounded = false;
        bool BeforeSitting = false;
        bool isAttack = false;
        void Start()
        {
            // 체력이 감소했을 때만 IsDamaged를 true로 설정
            Stage_UI_View.Instance.View_Model.Health.Pairwise() // 이전 값과 현재 값을 비교
                .Where(pair => pair.Previous > pair.Current) // 체력이 감소할 때만 실행
                .Subscribe(_ =>
                {
                    IsDamaged.Value = true;
                    ResetDamageState();
                }).AddTo(this);
        }
        private void ResetDamageState()
        {
            // 기존 타이머가 있으면 초기화
            damageResetSubscription?.Dispose();

            // 0.5초 후 IsDamaged를 다시 false로 설정
            damageResetSubscription = Observable.Timer(TimeSpan.FromSeconds(GroggiTime))
                .Subscribe(_ => IsDamaged.Value = false)
                .AddTo(this);
        }

        void SetState(State newState)
        {
            if (CurrentState == newState) return;
            currentState = newState;
        }

        void Update()
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
            if (GetPlayer_Health.health.Value <= 0)
            {
                SetState(State.Death_State);
                return;
            }

            // 데미지 여부
            if (IsDamaged.Value)
            {
                SetState(State.Damage_State);
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
                    CurrentState = State.Jump_State;
                    return;
                }
                if (isDownPressed)
                {
                    CurrentState = State.Fall_State;
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
                /*
                if (FallTime > 1.5f)
                {
                    //GetHealth.health.Value = 0;
                    //GetPlayer.CurrentState = Player.State.Death_State;
                    FallTime = 0f;
                    return;
                }
                if (FallTime > 1.2f)
                {
                    GetPlayer.CurrentState = Player.State.Land_State;
                    LandTime = 1f;
                    FallTime = 0f;
                    return;
                }*/
                return;
            }

            // 허공 시간
            if (CurrentState == State.Land_State && LandTime > 0f)
            {
                LandTime -= Time.deltaTime;
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
    }

}
