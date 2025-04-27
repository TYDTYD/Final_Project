namespace player
{
    public class LadderState : IState
    {
        Player player;
        public LadderState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public bool CanExcute(ICommand command)
        {
            if (player.GetDamaged)
                return false;
            if (command is Attack)
            {
                player.GetPlayer_Rigidbody.GetClimbing = false;
                return true;
            }
            if (command is Bomb)
                return false;
            if (command is Down)
            {
                if (player.GetPlayer_Rigidbody.GetLadder)
                    return true;
                return false;
            }
            if (command is Idle)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded)
                    return true;
                return false;
            }
            if (command is Jump)
                return true;
            if (command is Move)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded)
                    return true;
                return false;
            }
            if (command is Rope)
                return true;
            if (command is Up)
            {
                if (player.GetPlayer_Rigidbody.GetLadder)
                    return true;
                return false;
            }
            return false;
        }
    }
}