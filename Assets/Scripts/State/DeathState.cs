namespace player
{
    public class DeathState : IState
    {
        Player player;
        public DeathState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public PlayerStateType StateType => PlayerStateType.Death;
        public bool CanExecute(ICommand command)
        {
            player.CurrentState = player.GetDeath;
            return false;
        }
    }
}