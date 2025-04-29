using UnityEngine;
namespace player
{
    public class Ladder_Anim : StateMachineBehaviour
    {
        Player player;
        Player_Input input;
        Interact KeyManager;
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (player == null)
            {
                player = animator.gameObject.GetComponent<Player>();
                input = player.GetPlayer_Input;
                KeyManager = input.PlayerKey;
            }
                
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!input.GetKeyPress(KeyManager.keyCodes[(int)KeySequence.Up]) 
                && !input.GetKeyPress(KeyManager.keyCodes[(int)KeySequence.Down]))
                animator.speed = 0;
            else
                animator.speed = 1f;
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.speed = 1f;
        }
    }
}