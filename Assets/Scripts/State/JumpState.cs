namespace player
{
    public class JumpState : IState
    {
        Player player;
        public JumpState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public bool CanExcute(ICommand command)
        {
            if (player.GetDamaged)
                return false;
            if (command is Attack)
                return true;
            if (command is Bomb)
                return true;
            if (command is Down)
                return false;
            if (command is Idle)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded)
                    return true;
                return false;
            }
            if (command is Jump)
                return false;
            if (command is Move)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded)
                    return true;
                return false;
            }
            if(command is Rope)
                return true;
            if(command is Up)
            {
                if (player.GetPlayer_Rigidbody.GetLadder)
                    return true;
                return false;
            }
            return false;
        }
    }
}