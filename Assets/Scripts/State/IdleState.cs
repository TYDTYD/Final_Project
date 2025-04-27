namespace player
{
    public class IdleState : IState
    {
        Player player;
        public IdleState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public bool CanExcute(ICommand command)
        {
            return true;
        }
    }
}