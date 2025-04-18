namespace player
{
    using UnityEngine;
    using System.Collections.Generic;

    public partial class Player : MonoBehaviour
    {
        public enum State
        {
            Idle_State,
            Jump_State,
            Ladder_State,
            Damage_State,
            Attack_State,
            Move_State,
            Fall_State,
            Land_State,
            LadderStop_State,
            EdgeDetact_State,
            SittingStart_State,
            Sitting_State,
            SittingMove_State,
            Edge_State,
            Death_State
        };

        [SerializeField] Player_Health player_health;
        [SerializeField] Player_Item player_Item;
        [SerializeField] Player_Rigidbody player_Rigidbody;
        [SerializeField] Player_Input player_Input;
        [SerializeField] Player_Attack player_Attack;

        Rigidbody2D rigidBody;
        Animator animator;
        SpriteRenderer spriteRenderer;
        Player_Tracking player_Tracking;
        Dictionary<State, int> animationHashes;

        State previousState;
        State currentState = State.Idle_State;

        private void Awake()
        {
            animationHashes = new Dictionary<State, int>
        {
            { State.Idle_State, Animator.StringToHash("Idle") },
            { State.Jump_State, Animator.StringToHash("Jump") },
            { State.Ladder_State, Animator.StringToHash("Ladder") },
            { State.Damage_State, Animator.StringToHash("Hurt") },
            { State.Attack_State, Animator.StringToHash("Attack") },
            { State.Move_State, Animator.StringToHash("Move") },
            { State.Fall_State, Animator.StringToHash("Fall") },
            { State.Land_State, Animator.StringToHash("Land") },
            { State.SittingStart_State, Animator.StringToHash("Croush") },
            { State.Sitting_State, Animator.StringToHash("Sitting") },
            { State.SittingMove_State, Animator.StringToHash("SittingMove") },
            { State.EdgeDetact_State, Animator.StringToHash("Edge_Idle") },
            { State.Death_State, Animator.StringToHash("Death") },
            { State.Edge_State, Animator.StringToHash("CanClimb") }
        };

            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void SetAnimation(State state, State before_State)
        {
            if (animationHashes.TryGetValue(before_State, out int hashValue))
                animator.SetBool(hashValue, false);

            if (animationHashes.TryGetValue(state, out int hash))
                animator.SetBool(hash, true);
        }

        void TriggerAnimation(State state, State before_State)
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
        public State CurrentState
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
    }
}
