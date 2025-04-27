namespace player
{
    public interface IState
    {
        public bool CanExcute(ICommand command);
    }

    public class EdgeDetactState : IState
    {
        Player player;
        public EdgeDetactState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public bool CanExcute(ICommand command)
        {
            return false;
        }
    }

    public class SittingStartState : IState
    {
        Player player;
        public SittingStartState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public bool CanExcute(ICommand command)
        {
            return false;
        }
    }

    public class SittingState : IState
    {
        Player player;
        public SittingState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public bool CanExcute(ICommand command)
        {
            return false;
        }
    }

    public class SittingMoveState : IState
    {
        Player player;
        public SittingMoveState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public bool CanExcute(ICommand command)
        {
            return false;
        }
    }

    public class EdgeState : IState
    {
        Player player;
        public EdgeState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        public bool CanExcute(ICommand command)
        {
            return false;
        }
    }

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
