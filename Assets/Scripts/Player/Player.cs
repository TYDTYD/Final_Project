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
        Edge_State,
        Death_State
    };

    Animator GetAnimator;
    private Dictionary<State, int> animationHashes;
    private State previousState;
    public State CurrentState = State.Idle_State;

    private void Start()
    {
        GetAnimator = GetComponent<Animator>();
        animationHashes = new Dictionary<State, int>
        {
            { State.Idle_State, Animator.StringToHash("Idle") },
            { State.Jump_State, Animator.StringToHash("Jump") },
            { State.Ladder_State, Animator.StringToHash("Ladder") },
            { State.LadderStop_State, Animator.StringToHash("LadderStop") },
            { State.Damage_State, Animator.StringToHash("Hurt") },
            { State.Attack_State, Animator.StringToHash("Attack") },
            { State.Move_State, Animator.StringToHash("Move") },
            { State.Fall_State, Animator.StringToHash("Fall") },
            { State.Land_State, Animator.StringToHash("Land") },
            { State.EdgeDetact_State, Animator.StringToHash("Edge_Detact") },
            { State.SittingStart_State, Animator.StringToHash("Croush") },
            { State.Sitting_State, Animator.StringToHash("Sitting") },
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
}
