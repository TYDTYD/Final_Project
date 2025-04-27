namespace player
{
    using UnityEngine;
    using System.Collections.Generic;

    public partial class Player : MonoBehaviour
    {
        [SerializeField] Player_Health player_health;
        [SerializeField] Player_Item player_Item;
        [SerializeField] Player_Rigidbody player_Rigidbody;
        [SerializeField] Player_Input player_Input;
        [SerializeField] Player_Attack player_Attack;

        Rigidbody2D rigidBody;
        Animator animator;
        SpriteRenderer spriteRenderer;
        Player_Tracking player_Tracking;
        Dictionary<IState, int> animationHashes;
        IState previousState;
        IState currentState;
        IdleState IdleState;
        JumpState JumpState;
        LadderState LadderState;
        DamageState DamageState;
        AttackState AttackState;
        MoveState MoveState;
        FalIState FallState;
        LandState LandState;
        EdgeDetactState EdgeDetactState;
        SittingStartState SittingStartState;
        SittingState SittingState;
        SittingMoveState SittingMoveState;
        EdgeState EdgeState;
        DeathState DeathState;

        private void Awake()
        {
            IdleState = new IdleState(this);
            JumpState = new JumpState(this);
            LadderState = new LadderState(this);
            DamageState = new DamageState(this);
            AttackState = new AttackState(this);
            MoveState = new MoveState(this);
            FallState = new FalIState(this);
            LandState = new LandState(this);
            EdgeDetactState = new EdgeDetactState(this);
            SittingStartState = new SittingStartState(this);
            SittingState = new SittingState(this);
            SittingMoveState = new SittingMoveState(this);
            EdgeState = new EdgeState(this);
            DeathState = new DeathState(this);

            animationHashes = new Dictionary<IState, int>
        {
            { IdleState, Animator.StringToHash("Idle") },
            { JumpState, Animator.StringToHash("Jump") },
            { LadderState, Animator.StringToHash("Ladder") },
            { DamageState, Animator.StringToHash("Hurt") },
            { AttackState, Animator.StringToHash("Attack") },
            { MoveState, Animator.StringToHash("Move") },
            { FallState, Animator.StringToHash("Fall") },
            { LandState, Animator.StringToHash("Land") },
            { SittingStartState, Animator.StringToHash("Croush") },
            { SittingState, Animator.StringToHash("Sitting") },
            { SittingMoveState, Animator.StringToHash("SittingMove") },
            { EdgeDetactState, Animator.StringToHash("Edge_Idle") },
            { DeathState, Animator.StringToHash("Death") },
            { EdgeState, Animator.StringToHash("CanClimb") }
        };

            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void SetAnimation(IState state, IState before_State)
        {
            if (animationHashes.TryGetValue(before_State, out int hashValue))
                animator.SetBool(hashValue, false);

            if (animationHashes.TryGetValue(state, out int hash))
                animator.SetBool(hash, true);
        }

        void TriggerAnimation(IState state, IState before_State)
        {
            if (animationHashes.TryGetValue(before_State, out int hashValue))
                animator.ResetTrigger(hashValue);

            if (animationHashes.TryGetValue(state, out int hash))
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
        public IdleState GetIdle => IdleState;
        public JumpState GetJump => JumpState;
        public LadderState GetLadder => LadderState;
        public DamageState GetDamage => DamageState;
        public AttackState GetAttackState => AttackState;
        public MoveState GetMove => MoveState;
        public FalIState GetFalI => FallState;
        public LandState GetLand => LandState;
        public EdgeDetactState GetEdgeDetact => EdgeDetactState;
        public SittingStartState GetSittingStart => SittingStartState;
        public SittingState GetSitting => SittingState;
        public SittingMoveState GetSittingMove => SittingMoveState;
        public EdgeState GetEdge => EdgeState;
        public DeathState GetDeath => DeathState;
    }
}
