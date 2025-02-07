using UnityEngine;
using System.Collections.Generic;
public class Player : MonoBehaviour
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

    Animator animator;
    SpriteRenderer spriteRenderer;
    Player_Health player_health;
    Player_Item player_Item;
    Rigidbody2D rigidBody;
    Player_Rigidbody player_Rigidbody;
    Player_Input player_Input;
    Dictionary<State, int> animationHashes;
    Player_Flip player_Flip;
    Player_Ceiling player_Ceiling;
    State previousState;
    State CurrentState = State.Idle_State;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        player_Rigidbody = GetComponent<Player_Rigidbody>();
        player_health = GetComponent<Player_Health>();
        player_Item = GetComponent<Player_Item>();
        player_Flip = GetComponentInChildren<Player_Flip>();
        player_Ceiling = GetComponentInChildren<Player_Ceiling>();
        player_Input = GetComponent<Player_Input>();
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
            { State.EdgeDetact_State, Animator.StringToHash("Edge_Detact") },
            { State.SittingStart_State, Animator.StringToHash("Croush") },
            { State.Sitting_State, Animator.StringToHash("Sitting") },
            { State.SittingMove_State, Animator.StringToHash("SittingMove") },
            { State.Edge_State, Animator.StringToHash("Edge_Idle") },
            { State.Death_State, Animator.StringToHash("Death") }
        };
    }


    void Update()
    {
        if (CurrentState != previousState)
        {
            previousState = CurrentState;
            TriggerAnimation(CurrentState);
        }
    }

    void TriggerAnimation(State state)
    {
        foreach (int hashValue in animationHashes.Values)
        {
            GetAnimator.ResetTrigger(hashValue);
        }

        if (animationHashes.TryGetValue(state, out int hash))
        {
            GetAnimator.SetTrigger(hash);
        }
    }

    public SpriteRenderer GetSprite
    {
        get
        {
            return spriteRenderer;
        }
    }

    public Rigidbody2D GetRigidbody
    {
        get
        {
            return rigidBody;
        }
    }

    public Player_Rigidbody GetPlayer_Rigidbody
    {
        get
        {
            return player_Rigidbody;
        }
    }

    public Player_Input GetPlayer_Input
    {
        get
        {
            return player_Input;
        }
    }

    public Player_Item GetPlayer_Item
    {
        get
        {
            return player_Item;
        }
    }

    public Animator GetAnimator
    {
        get
        {
            return animator;
        }
    }

    public Player_Health GetPlayer_Health
    {
        get
        {
            return player_health;
        }
    }

    public Player_Flip GetPlayer_Flip
    {
        get
        {
            return player_Flip;
        }
    }

    public Player_Ceiling GetPlayer_Ceiling
    {
        get
        {
            return player_Ceiling;
        }
    }

    public State GetState
    {
        get
        {
            return CurrentState;
        }
        set
        {
            CurrentState = value;
        }
    }
}
