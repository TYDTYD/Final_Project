namespace player
{
    public class SittingState : IState
    {
        Player player;
        public SittingState(Player GetPlayer)
        {
            player = GetPlayer;
        }

        public PlayerStateType StateType => PlayerStateType.Sitting;

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
            if (command is Down)
            {
                return true;
            }
            if (command is Idle)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded)
                {
                    player.CurrentState = player.GetIdle;
                    return true;
                }
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
                
            if (command is Up)
                return false;
            return true;
        }
    }
}