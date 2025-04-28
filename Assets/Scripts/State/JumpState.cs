namespace player
{
    public class JumpState : IState
    {
        Player player;
        public JumpState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public PlayerStateType StateType => PlayerStateType.Jump;
        public bool CanExecute(ICommand command)
        {
            if (player.GetDamaged)
            {
                player.CurrentState = player.GetDamage;
                return false;
            }
            if (command is Attack)
                return true;
            if (command is Down)
                return false;
            if (command is Idle)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded)
                {
                    player.CurrentState = player.GetIdle;
                    return true;
                }
                return false;
            }
            if (command is Move)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded)
                {
                    player.CurrentState = player.GetMove;
                }
                return true;
            }
            if(command is Up)
            {
                if (player.GetPlayer_Rigidbody.GetLadder)
                {
                    player.CurrentState = player.GetLadder;
                    return true;
                }
                return false;
            }
            player.CurrentState = player.GetFall;
            return true;
        }
    }
}