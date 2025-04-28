namespace player
{
    public class DeathState : IState
    {
        Player player;
        public DeathState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public bool CanExcute(ICommand command)
        {
            return false;
        }
    }
}