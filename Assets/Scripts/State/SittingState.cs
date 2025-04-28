namespace player
{
    public class SittingState : IState
    {
        Player player;
        public SittingState(Player GetPlayer)
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
            if (command is Attack)
            {
                return true;
            }
            if (command is Bomb)
                return true;
            if (command is Down)
                return true;
            if (command is Idle)
                return true;
            if (command is Jump)
                return true;
            if (command is Move)
                return true;
            if (command is Rope)
                return true;
            if (command is Up)
                return false;
            return false;
        }
    }
}