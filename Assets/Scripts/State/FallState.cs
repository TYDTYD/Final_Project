namespace player
{
    public class FalIState : IState
    {
        Player player;
        public FalIState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public bool CanExcute(ICommand command)
        {
            if (player.GetDamaged)
            {
                player.CurrentState = player.GetDamage;
                return false;
            }
            if (player.GetRigidbody.linearVelocityY > 5f)
            {
                player.CurrentState = player.GetLand;
                return false;
            }
            if (command is Attack)
                return true;
            if (command is Bomb)
                return true;
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
                return false;
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