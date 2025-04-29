namespace player
{
    public class FallState : IState
    {
        Player player;
        public FallState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public PlayerStateType StateType => PlayerStateType.Fall;
        public bool CanExecute(ICommand command)
        {
            if (player.GetPlayer_Health.GetDamaged)
            {
                player.CurrentState = player.GetDamage;
                return false;
            }
            if (player.GetRigidbody.linearVelocityY > 5f)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded)
                {
                    player.CurrentState = player.GetLand;
                    return false;
                }
            }
            if (command is Attack)
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
                {
                    player.CurrentState = player.GetIdle;
                    return true;
                }
                return false;
            }
            if (command is Jump)
                return false;
            if (command is Move)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded)
                    player.CurrentState = player.GetMove;
                return true;
            }
            if (command is Up)
            {
                if (player.GetPlayer_Rigidbody.GetLadder)
                {
                    player.CurrentState = player.GetLadder;
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}