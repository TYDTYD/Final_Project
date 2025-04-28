namespace player
{
    public class AttackState : IState
    {
        Player player;
        public AttackState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public PlayerStateType StateType => PlayerStateType.Attack;
        public bool CanExecute(ICommand command)
        {
            if (player.GetDamaged)
            {
                player.CurrentState = player.GetDamage;
                return false;
            }
            if (command is Down)
                return false;
            if (command is Idle)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded && !player.GetPlayer_Attack.GetBox.enabled)
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
                {
                    player.CurrentState = player.GetMove;
                    return true;
                }
                return false;
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