namespace player
{
    public class SittingMoveState : IState
    {
        Player player;
        public SittingMoveState(Player GetPlayer)
        {
            player = GetPlayer;
        }

        public PlayerStateType StateType => PlayerStateType.SittingMove;

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
                return true;
            if (command is Down)
                return true;
            if (command is Idle)
            {
                player.CurrentState = player.GetIdle;
                return true;
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
                return false;
            return false;
        }
    }
}