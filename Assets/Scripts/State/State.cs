namespace player
{
    public interface IState
    {
        PlayerStateType StateType { get; }
        public bool CanExecute(ICommand command);
    }
}