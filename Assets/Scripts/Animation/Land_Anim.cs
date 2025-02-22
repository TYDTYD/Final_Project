namespace player
{
    using UnityEngine;
    public class Land_Anim : StateMachineBehaviour
    {
        Player player;
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (player == null)
                player = animator.gameObject.GetComponent<Player>();
            player.GetRigidbody.linearVelocity = Vector2.zero;
        }
    }
}