namespace player
{
    public class IdleState : IState
    {
        Player player;
        public IdleState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public PlayerStateType StateType => PlayerStateType.Idle;
        public bool CanExecute(ICommand command)
        {
            if (player.GetDamaged)
            {
                player.CurrentState = player.GetDamage;
                return false;
            }
            if (!player.GetPlayer_Rigidbody.GetGrounded)
            {
                player.CurrentState = player.GetJump;
            }
            if (command is Jump)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded)
                {
                    return true;
                }
                return false;
            }
            if (command is Move)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded)
                    player.CurrentState = player.GetMove;
                else
                    return false;
            }
            if (command is Up)
            {
                if (player.GetPlayer_Rigidbody.GetLadder)
                    player.CurrentState = player.GetLadder;
                else
                    return false;
            }
            if(command is Down)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded)
                {
                    player.CurrentState = player.GetSitting;
                }
            }
            return true;
        }
    }
}