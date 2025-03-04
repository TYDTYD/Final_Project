using UnityEngine;

public enum State
{
    IDLE,
    MOVE,
    ATTACK,
    DEATH
}

public class Monster_Anim : MonoBehaviour
{
    [SerializeField] Animator GetAnimator;
    State CurrentState = State.IDLE;
    State PreviousState;

    void Update()
    {
        if (CurrentState == PreviousState)
            return;
        PreviousState = CurrentState;
        switch (CurrentState)
        {
            case State.IDLE:
                GetAnimator.SetBool(Animator.StringToHash("1_Move"), false);
                break;
            case State.MOVE:
                GetAnimator.SetBool(Animator.StringToHash("1_Move"), true);
                break;
            case State.ATTACK:
                GetAnimator.SetBool(Animator.StringToHash("1_Move"), false);
                GetAnimator.SetTrigger(Animator.StringToHash("2_Attack"));
                break;
            case State.DEATH:
                GetAnimator.SetBool(Animator.StringToHash("1_Move"), false);
                GetAnimator.SetTrigger(Animator.StringToHash("isDeath"));
                break;
        }
    }
    public State GetState {
        get => CurrentState;
        set => CurrentState = value;
    }
}
