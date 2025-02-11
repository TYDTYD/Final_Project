using UnityEngine;
using UniRx;
using System;

public class Player_Anim : MonoBehaviour
{
    Player_Health GetHealth;
    Player_Rigidbody GetRigidbody;
    Animator animator;
    Player GetPlayer;

    ReactiveProperty<bool> IsDamaged = new ReactiveProperty<bool>(false);
    IDisposable damageResetSubscription;

    float GroggiTime = 0.5f;
    float FallTime = 0f;
    float LandTime = 0f;
    float AttackTime = 0f;
    float SittingTime = 0f;
    bool isSittingMoved = false;
    bool BeforeGrounded = false;
    bool BeforeSitting = false;
    void Start()
    {
        GetPlayer = GetComponent<Player>();
        GetHealth = GetPlayer.GetPlayer_Health;
        GetRigidbody = GetPlayer.GetPlayer_Rigidbody;
        animator = GetPlayer.GetAnimator;

        // 체력이 감소했을 때만 IsDamaged를 true로 설정
        GetHealth.health.Pairwise() // 이전 값과 현재 값을 비교
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
        // 생사 여부
        if (GetHealth.health.Value <= 0)
        {
            GetPlayer.CurrentState = Player.State.Death_State;
            return;
        }

        // 데미지 여부
        if (IsDamaged.Value)
        {
            GetPlayer.CurrentState = Player.State.Damage_State;
            return;
        }

        // 사다리 여부
        if (GetRigidbody.isClimbing)
        {
            FallTime = 0f;
            if (Input.GetKey(InputHandler.JumpKey))
            {
                GetPlayer.CurrentState = Player.State.Jump_State;
                return;
            }

            GetPlayer.CurrentState = Player.State.Ladder_State;
            return;
        }

        if (GetPlayer.CurrentState == Player.State.Edge_State)
        {
            if (Input.GetKeyDown(InputHandler.JumpKey))
            {
                GetPlayer.CurrentState = Player.State.Jump_State;
                return;
            }
            if (Input.GetKeyDown(InputHandler.DownKey))
            {
                GetPlayer.CurrentState = Player.State.Fall_State;
            }
            return;
        }

        // 공중 여부
        if (!GetRigidbody.isGrounded)
        {
            FallTime += Time.deltaTime;
            BeforeGrounded = false;
            if (GetPlayer.GetPlayer_Right_Flip.GetEdgeDetact)
            {
                if (!Input.GetKey(InputHandler.RightKey))
                    return;
                GetPlayer.CurrentState = Player.State.Edge_State;
                FallTime = 0f;
                return;
            }
            if (GetPlayer.GetPlayer_Left_Flip.GetEdgeDetact)
            {
                if (!Input.GetKey(InputHandler.LeftKey))
                    return;
                GetPlayer.CurrentState = Player.State.Edge_State;
                FallTime = 0f;
                return;
            }
            if (Input.GetKeyDown(InputHandler.JumpKey))
            {
                GetPlayer.CurrentState = Player.State.Jump_State;
                return;
            }
            GetPlayer.CurrentState = Player.State.Fall_State;
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
            FallTime = 0f;
            return;
        }

        // 허공 시간
        if (GetPlayer.CurrentState == Player.State.Land_State && LandTime > 0f)
        {
            LandTime -= Time.deltaTime;
            return;
        }

        if (Input.GetKeyDown(InputHandler.AttackKey) && AttackTime <= 0f)
        {
            GetPlayer.CurrentState = Player.State.Attack_State;
            AttackTime = 0.65f;
            return;
        }

        if(GetPlayer.CurrentState == Player.State.Attack_State && AttackTime>0f)
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
                GetPlayer.CurrentState = Player.State.SittingStart_State;
                BeforeSitting = true;
                return;
            }

            if (Input.GetKey(InputHandler.RightKey) || Input.GetKey(InputHandler.LeftKey))
            {
                GetPlayer.CurrentState = Player.State.SittingMove_State;
                isSittingMoved = true;
                SittingTime = 0f;
                return;
            }
            else
            {
                isSittingMoved = false;
                SittingTime += Time.deltaTime;
                GetPlayer.CurrentState = Player.State.Sitting_State;
            }
            GetPlayer.CurrentState = Player.State.Sitting_State;
            return;
        }

        BeforeSitting = false;
        SittingTime = 0f;

        if (Input.GetKeyDown(InputHandler.JumpKey))
        {
            GetPlayer.CurrentState = Player.State.Jump_State;
            return;
        }

        if(Input.GetKey(InputHandler.RightKey) || Input.GetKey(InputHandler.LeftKey))
        {
            GetPlayer.CurrentState = Player.State.Move_State;
            return;
        }

        GetPlayer.CurrentState = Player.State.Idle_State;
    }

    public float GetSittingTime => SittingTime;
}
