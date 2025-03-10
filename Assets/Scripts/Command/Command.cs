using player;

public interface ICommand
{
    void Execute();
    void Execute(Player player);
}