using UnityEngine;

public class Edge_Anim : StateMachineBehaviour
{
    Player GetPlayer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GetPlayer == null)
        {
            Debug.Log("º® ºÙ±â");
            GetPlayer = animator.gameObject.GetComponent<Player>();
        }
        GetPlayer.GetRigidbody.linearVelocity = Vector2.zero;
        GetPlayer.GetRigidbody.gravityScale = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetPlayer.GetRigidbody.linearVelocity = Vector2.zero;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetPlayer.GetRigidbody.gravityScale = 5f;
    }
}
