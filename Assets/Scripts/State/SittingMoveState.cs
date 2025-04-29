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
            if (player.GetPlayer_Health.GetDamaged)
            {
                player.CurrentState = player.GetDamage;
                return false;
            }
            if (!player.GetPlayer_Rigidbody.GetGrounded)
            {
                player.CurrentState = player.GetFall;
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