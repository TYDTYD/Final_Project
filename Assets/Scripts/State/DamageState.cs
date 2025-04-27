namespace player
{
    public class DamageState : IState
    {
        Player player;
        public DamageState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public bool CanExcute(ICommand command)
        {
            return false;
        }
    }
}