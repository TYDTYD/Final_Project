using UnityEngine;
using UniRx;
using System;

public class Player_Anim : MonoBehaviour
{
    Player_Health GetHealth;
    Player_Rigidbody GetRigidbody;
    Player_Edge_Detact GetPlayer_Edge_Detact;
    Player GetPlayer;

    ReactiveProperty<bool> IsDamaged = new ReactiveProperty<bool>(false);
    IDisposable damageResetSubscription;

    float GroggiTime = 0.5f;
    float FallTime = 0f;
    float LandTime = 0f;
    public float SittingTime = 0f;
    public bool isSittingMoved = false;
    bool BeforeGrounded = false;
    bool BeforeSitting = false;
    void Start()
    {
        GetHealth = GetComponent<Player_Health>();
        GetRigidbody = GetComponent<Player_Rigidbody>();
        GetPlayer_Edge_Detact = GetComponentInChildren<Player_Edge_Detact>();
        GetPlayer = GetComponent<Player>();
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
            GetPlayer.GetState = Player.State.Death_State;
            return;
        }

        // 데미지 여부
        if (IsDamaged.Value)
        {
            GetPlayer.GetState = Player.State.Damage_State;
            return;
        }

        // 사다리 여부
        if (GetRigidbody.isClimbing)
        {
            if (Input.GetKey(Interact.Jump))
            {
                GetPlayer.GetState = Player.State.Jump_State;
                return;
            }

            if (Input.GetKey(Interact.Attack))
            {
                GetPlayer.GetState = Player.State.Attack_State;
                return;
            }

            GetPlayer.GetState = Player.State.Ladder_State;
            return;
        }

        // 공중 여부
        if (!GetRigidbody.isGrounded)
        {
            FallTime += Time.deltaTime;
            BeforeGrounded = false;
            if (Input.GetKey(Interact.Attack))
            {
                GetPlayer.GetState = Player.State.Attack_State;
                return;
            }
            if (Input.GetKeyDown(Interact.Jump))
            {
                GetPlayer.GetState = Player.State.Jump_State;
                return;
            }
            GetPlayer.GetState = Player.State.Fall_State;
            return;
        }

        // 착지 여부
        if (!BeforeGrounded)
        {
            BeforeGrounded = true;
            if (FallTime > 2f)
            {
                GetPlayer.GetState = Player.State.Land_State;
                LandTime = 0.5f;
                FallTime = 0f;
                return;
            }
            if(FallTime > 3f)
            {
                GetPlayer.GetState = Player.State.Death_State;
                FallTime = 0f;
                return;
            }
            FallTime = 0f;
            return;
        }

        if(GetPlayer.GetState == Player.State.Land_State && LandTime > 0f)
        {
            LandTime -= Time.deltaTime;
            return;
        }

        // 모서리 여부
        if (GetPlayer.GetState == Player.State.EdgeDetact_State)
        {
            GetPlayer.GetState = Player.State.Edge_State;
            return;
        }

        if (GetPlayer.GetState == Player.State.Edge_State)
        {
            if (Input.GetKeyDown(Interact.Jump))
            {
                GetPlayer.GetState = Player.State.Jump_State;
                return;
            }
            if (Input.GetKeyDown(Interact.Down))
            {
                GetPlayer.GetState = Player.State.Fall_State;
            }
            return;
        }
        // todo 모서리에서 점프, 위로 다시 올라가기 작업 고려 애니메이션 클립 추가

        // 앉기 여부
        if (Input.GetKey(Interact.Down))
        {
            if (!BeforeSitting)
            {
                GetPlayer.GetState = Player.State.SittingStart_State;
                BeforeSitting = true;
                return;
            }

            if (Input.GetKey(Interact.Right) || Input.GetKey(Interact.Left))
            {
                GetPlayer.GetState = Player.State.SittingMove_State;
                isSittingMoved = true;
                SittingTime = 0f;
                return;
            }
            else
            {
                isSittingMoved = false;
                SittingTime += Time.deltaTime;
                GetPlayer.GetState = Player.State.Sitting_State;
            }

            if (GetPlayer_Edge_Detact.isEdge)
            {
                GetPlayer.GetState = Player.State.EdgeDetact_State;
                return;
            }
            GetPlayer.GetState = Player.State.Sitting_State;
            return;
        }

        BeforeSitting = false;
        SittingTime = 0f;

        if (Input.GetKeyDown(Interact.Jump))
        {
            GetPlayer.GetState = Player.State.Jump_State;
            return;
        }

        if(Input.GetKey(Interact.Right) || Input.GetKey(Interact.Left))
        {
            GetPlayer.GetState = Player.State.Move_State;
            return;
        }

        GetPlayer.GetState = Player.State.Idle_State;
    }
}
