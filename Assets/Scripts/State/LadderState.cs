namespace player
{
    public class LadderState : IState
    {
        Player player;
        public LadderState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public PlayerStateType StateType => PlayerStateType.Ladder;
        public bool CanExecute(ICommand command)
        {
            if (player.GetPlayer_Health.GetHp <= 0)
                player.CurrentState = player.GetDeath;
            if (player.GetPlayer_Health.GetDamaged)
            {
                player.CurrentState = player.GetDamage;
                return false;
            }
            if (command is Attack)
            {
                return false;
            }
            if (command is Bomb)
                return false;
            if (command is Down)
            {
                if (player.GetPlayer_Rigidbody.GetLadder)
                {
                    player.CurrentState = player.GetLadder;
                    return true;
                }
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
            {
                player.CurrentState = player.GetJump;
                return true;
            }
            if (command is Move)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded)
                {
                    player.CurrentState = player.GetMove;
                    return true;
                }
                return false;
            }
            if (command is Rope)
                return true;
            if (command is Up)
            {
                if (player.GetPlayer_Rigidbody.GetLadder)
                {
                    player.CurrentState = player.GetLadder;
                    return true;
                }
                return false;
            }
            if(!player.GetPlayer_Rigidbody.GetLadder && !player.GetPlayer_Rigidbody.GetGrounded)
            {
                player.CurrentState = player.GetFall;
            }
            return false;
        }
    }
}