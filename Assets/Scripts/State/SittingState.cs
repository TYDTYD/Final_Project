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
            if (player.GetPlayer_Health.GetHp <= 0)
                player.CurrentState = player.GetDeath;
            if (player.GetPlayer_Health.GetDamaged)
            {
                player.CurrentState = player.GetDamage;
                return false;
            }
            if (!player.GetPlayer_Rigidbody.GetGrounded)
            {
                player.CurrentState = player.GetFall;
            }
            if (command is Idle)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded)
                {
                    player.CurrentState = player.GetIdle;
                    return true;
                }
            }
            if (command is Move)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded)
                {
                    return true;
                }
                return false;
            }
                
            if (command is Up)
                return false;
            return true;
        }
    }
}