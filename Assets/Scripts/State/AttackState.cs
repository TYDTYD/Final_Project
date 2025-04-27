namespace player
{
    public class AttackState : IState
    {
        Player player;
        public AttackState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public bool CanExcute(ICommand command)
        {
            if (player.GetDamaged)
                return false;
            if (command is Bomb)
                return true;
            if (command is Down)
                return false;
            if (command is Idle)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded && !player.GetPlayer_Attack.GetBox.enabled)
                    return true;
                return false;
            }
            if (command is Jump)
                return false;
            if (command is Move)
            {
                if (player.GetPlayer_Rigidbody.GetGrounded)
                    return true;
                return false;
            }
            if (command is Rope)
                return true;
            if (command is Up)
            {
                if (player.GetPlayer_Rigidbody.GetLadder)
                    return true;
                return false;
            }
            return false;
        }
    }
}