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

        void Update()
        {
            if (currentState != previousState)
            {
                TriggerAnimation(currentState, previousState);
                previousState = currentState;
            }

            // 생사 여부
            if (GetPlayer_Health.health.Value <= 0)
            {
                CurrentState = State.Death_State;
                return;
            }

            // 데미지 여부
            if (IsDamaged.Value)
            {
                CurrentState = Player.State.Damage_State;
                return;
            }

            // 사다리 여부
            if (GetPlayer_Rigidbody.GetClimbing)
            {
                if (Input.GetKey(InputHandler.JumpKey))
                {
                    CurrentState = Player.State.Jump_State;
                    return;
                }

                CurrentState = Player.State.Ladder_State;
                return;
            }

            if (CurrentState == Player.State.Edge_State)
            {
                if (Input.GetKeyDown(InputHandler.JumpKey))
                {
                    CurrentState = Player.State.Jump_State;
                    return;
                }
                if (Input.GetKeyDown(InputHandler.DownKey))
                {
                    CurrentState = Player.State.Fall_State;
                }
                return;
            }

            // 공중 여부
            if (!GetPlayer_Rigidbody.GetGrounded)
            {
                BeforeGrounded = false;
                if (GetPlayer_Right_Flip.GetEdgeDetact)
                {
                    if (!Input.GetKey(InputHandler.RightKey))
                        return;
                    CurrentState = State.Edge_State;
                    return;
                }
                if (GetPlayer_Left_Flip.GetEdgeDetact)
                {
                    if (!Input.GetKey(InputHandler.LeftKey))
                        return;
                    CurrentState = State.Edge_State;
                    return;
                }
                if (Input.GetKeyDown(InputHandler.JumpKey))
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

            if (CurrentState == State.Attack_State && AttackTime <= 0f)
            {
                AttackTime = 0.65f;
                return;
            }

            if (CurrentState == State.Attack_State && AttackTime > 0f)
            {
                AttackTime -= Time.deltaTime;
                return;
            }

            // todo 모서리에서 점프, 위로 다시 올라가기 작업 고려 애니메이션 클립 추가

            // 앉기 여부
            if (Input.GetKey(InputHandler.DownKey))
            {
                if (!BeforeSitting)
                {
                    CurrentState = State.SittingStart_State;
                    BeforeSitting = true;
                    return;
                }

                if (Input.GetKey(InputHandler.RightKey) || Input.GetKey(InputHandler.LeftKey))
                {
                    CurrentState = State.SittingMove_State;
                    isSittingMoved = true;
                    SittingTime = 0f;
                    return;
                }
                else
                {
                    isSittingMoved = false;
                    SittingTime += Time.deltaTime;
                    CurrentState = State.Sitting_State;
                }
                CurrentState = State.Sitting_State;
                return;
            }

            BeforeSitting = false;
            SittingTime = 0f;

            if (Input.GetKeyDown(InputHandler.JumpKey))
            {
                CurrentState = State.Jump_State;
                return;
            }

            if (Input.GetKey(InputHandler.RightKey) || Input.GetKey(InputHandler.LeftKey))
            {
                CurrentState = State.Move_State;
                return;
            }

            CurrentState = State.Idle_State;
        }

        public float GetSittingTime => SittingTime;
    }

}
