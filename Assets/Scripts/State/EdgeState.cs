namespace player
{
    public class EdgeState : IState
    {
        Player player;
        public EdgeState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public PlayerStateType StateType => PlayerStateType.Edge;
        public bool CanExecute(ICommand command)
        {
            if (player.GetDamaged)
            {
                player.CurrentState = player.GetDamage;
                return false;
            }
            if (command is Attack)
            {
                player.CurrentState = player.GetAttackState;
                return true;
            }
            if (command is Bomb)
                return false;
            if (command is Down)
                return true;
            if (command is Idle)
            {
                return false;
            }
            if (command is Jump)
            {
                player.CurrentState = player.GetJump;
                return true;
            }
            if (command is Move)
            {

                return true;
            }
            if (command is Rope)
                return true;
            if (command is Up)
                return true;
            return false;
        }
    }
}