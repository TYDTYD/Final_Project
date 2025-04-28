namespace player
{
    using UnityEngine;
    public class MoveState : IState
    {
        Player player;
        public MoveState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public PlayerStateType StateType => PlayerStateType.Move;
        public bool CanExecute(ICommand command)
        {
            if (player.GetDamaged)
            {
                player.CurrentState = player.GetDamage;
                return false;
            }
            if (!player.GetPlayer_Rigidbody.GetGrounded)
            {
                player.CurrentState = player.GetFall;
            }
            if (command is Attack)
            {
                player.CurrentState = player.GetAttackState;
                return true;
            }
            if (command is Down)
            {
                if (player.GetPlayer_Rigidbody.GetLadder)
                {
                    return true;
                }
                return false;
            }
            if (command is Idle)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded)
                {
                    player.CurrentState = player.GetIdle;
                    return true;
                }
                return false;
            }
            if (command is Up)
            {
                if (player.GetPlayer_Rigidbody.GetLadder)
                {
                    player.CurrentState = player.GetLadder;
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}