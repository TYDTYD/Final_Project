namespace player
{
    public class EdgeDetectState : IState
    {
        Player player;
        public EdgeDetectState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public PlayerStateType StateType => PlayerStateType.EdgeDetect;
        public bool CanExecute(ICommand command)
        {
            if (player.GetDamaged)
            {
                player.CurrentState = player.GetDamage;
                return false;
            }
            if (command is Attack)
                return true;
            if (command is Bomb)
                return false;
            if (command is Down)
                return true;
            if (command is Idle)
                return false;
            if (command is Jump)
                return false;
            if (command is Move)
                return false;
            if (command is Rope)
                return true;
            if (command is Up)
                return false;
            return false;
        }
    }
}
