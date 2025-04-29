namespace player
{
    public class DamageState : IState
    {
        Player player;
        public DamageState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public PlayerStateType StateType => PlayerStateType.Damage;
        public bool CanExecute(ICommand command)
        {
            if (player.GetPlayer_Health.GetDamaged)
                return false;
            if (command is Down)
            {
                if (player.GetPlayer_Rigidbody.GetLadder)
                {
                    player.CurrentState = player.GetLadder;
                    return true;
                }
                player.CurrentState = player.GetSitting;
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
            if (command is Move)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded)
                {
                    player.CurrentState = player.GetMove;
                }
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
            player.CurrentState = player.GetFall;
            return true;
        }
    }
}