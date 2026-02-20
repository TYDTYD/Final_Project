using UnityEngine;
using System.Collections.Generic;

namespace player
{    
    public enum PlayerStateType
    {
        Idle,
        Jump,
        Ladder,
        Damage,
        Attack,
        Move,
        Fall,
        Land,
        SittingStart,
        Sitting,
        SittingMove,
        Edge,
        Death
    }

    public partial class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }

        [SerializeField] Player_Health player_health;
        [SerializeField] Player_Item player_Item;
        [SerializeField] Player_Rigidbody player_Rigidbody;
        [SerializeField] Player_Input player_Input;
        [SerializeField] Player_Attack player_Attack;

        Rigidbody2D rigidBody;
        Animator animator;
        SpriteRenderer spriteRenderer;
        Player_Tracking player_Tracking;

        Dictionary<PlayerStateType, int> animationHashes;
        IState currentState, previousState;

        IdleState idleState;
        JumpState jumpState;
        LadderState ladderState;
        DamageState damageState;
        AttackState attackState;
        MoveState moveState;
        FallState fallState;
        LandState landState;
        SittingState sittingState;
        SittingMoveState sittingMoveState;
        EdgeState edgeState;
        DeathState deathState;

        private void Awake()
        {
            // 싱글톤 패턴 구현
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // 씬 변경 시에도 유지
            }
            else
            {
                Destroy(gameObject); // 중복 방지
                return;
            }

            GameManager.Instance.Restart += InitStatus;
            player_health.DeathEvent += () => GameManager.Instance.initialized = false;

            CreateState();
            animationHashes = new Dictionary<PlayerStateType, int>
            {
                { PlayerStateType.Idle, Animator.StringToHash("Idle") },
                { PlayerStateType.Jump, Animator.StringToHash("Jump") },
                { PlayerStateType.Ladder, Animator.StringToHash("Ladder") },
                { PlayerStateType.Damage, Animator.StringToHash("Hurt") },
                { PlayerStateType.Attack, Animator.StringToHash("Attack") },
                { PlayerStateType.Move, Animator.StringToHash("Move") },
                { PlayerStateType.Fall, Animator.StringToHash("Fall") },
                { PlayerStateType.Land, Animator.StringToHash("Land") },
                { PlayerStateType.Sitting, Animator.StringToHash("Sitting") },
                { PlayerStateType.SittingMove, Animator.StringToHash("SittingMove") },
                { PlayerStateType.Edge, Animator.StringToHash("Edge_Idle") },
                { PlayerStateType.Death, Animator.StringToHash("Death") }
            };

            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            currentState = idleState;
            previousState = currentState;
        }

        private void OnDestroy()
        {
            GameManager.Instance.Restart -= InitStatus;
        }

        void InitStatus()
        {
            currentState = idleState;
        }

        void CreateState()
        {
            idleState = new IdleState(this);
            jumpState = new JumpState(this);
            ladderState = new LadderState(this);
            damageState = new DamageState(this);
            attackState = new AttackState(this);
            moveState = new MoveState(this);
            fallState = new FallState(this);
            landState = new LandState(this);
            sittingState = new SittingState(this);
            sittingMoveState = new SittingMoveState(this);
            edgeState = new EdgeState(this);
            deathState = new DeathState(this);
        }
        void SetAnimation(IState state, IState before_State)
        {
            if (animationHashes.TryGetValue(before_State.StateType, out int hashValue))
                animator.SetBool(hashValue, false);

            if (animationHashes.TryGetValue(state.StateType, out int hash))
                animator.SetBool(hash, true);
        }
        void TriggerAnimation(IState state, IState before_State)
        {
            if (animationHashes.TryGetValue(before_State.StateType, out int hashValue))
                animator.ResetTrigger(hashValue);

            if (animationHashes.TryGetValue(state.StateType, out int hash))
                animator.SetTrigger(hash);
        }
        public Player_Attack GetPlayer_Attack => player_Attack;
        public SpriteRenderer GetSprite => spriteRenderer;
        public Rigidbody2D GetRigidbody => rigidBody;
        public Player_Rigidbody GetPlayer_Rigidbody => player_Rigidbody;
        public Player_Input GetPlayer_Input => player_Input;
        public Player_Item GetPlayer_Item => player_Item;
        public Animator GetAnimator => animator;
        public Player_Health GetPlayer_Health => player_health;
        public Player_Tracking GetPlayer_Tracking
        {
            get
            {
                return player_Tracking;
            }
            set
            {
                player_Tracking = value;
            }
        }
        public IState CurrentState
        {
            get
            {
                return currentState;
            }
            set
            {
                currentState = value;
            }
        }
        public IdleState GetIdle => idleState;
        public JumpState GetJump => jumpState;
        public LadderState GetLadder => ladderState;
        public DamageState GetDamage => damageState;
        public AttackState GetAttackState => attackState;
        public MoveState GetMove => moveState;
        public FallState GetFall => fallState;
        public LandState GetLand => landState;
        public SittingState GetSitting => sittingState;
        public SittingMoveState GetSittingMove => sittingMoveState;
        public EdgeState GetEdge => edgeState;
        public DeathState GetDeath => deathState;
    }
}
