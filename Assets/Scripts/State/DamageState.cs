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
            player.CurrentState = player.GetDamage;
            return false;
        }
    }
}