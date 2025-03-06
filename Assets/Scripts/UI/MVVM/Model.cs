using UniRx;
public class Model
{
    public IReactiveProperty<int> Health { get; private set; } = new ReactiveProperty<int>(4);
    public IReactiveProperty<int> Bomb { get; private set; } = new ReactiveProperty<int>(4);
    public IReactiveProperty<int> Rope { get; private set; } = new ReactiveProperty<int>(4);
    public IReactiveProperty<int> Money { get; private set; } = new ReactiveProperty<int>(0);
    public IReactiveProperty<int> Stage { get; private set; } = new ReactiveProperty<int>(1);
    public IReactiveProperty<int> Time { get; private set; } = new ReactiveProperty<int>(0);
    public IReactiveProperty<int> TotalTime { get; private set; } = new ReactiveProperty<int>(0);
    public void UpdateHealth(int amount)
    {
        if (Health.Value <= 0)
            return;
        if (Health.Value + amount < 0)
        {
            Health.Value = 0;
            return;
        }
        Health.Value += amount;
    }
    public void UpdateBomb(int amount)
    {
        if (Bomb.Value <= 0)
            return;
        if (Bomb.Value + amount < 0)
        {
            Bomb.Value = 0;
            return;
        }
        Bomb.Value += amount;
    }
    public void UpdateRope(int amount)
    {
        if (Rope.Value <= 0)
            return;
        if (Rope.Value + amount < 0)
        {
            Rope.Value = 0;
            return;
        }
        Rope.Value += amount;
    }
    public void UpdateMoney(int amount)
    {
        if (Money.Value <= 0)
            return;
        if (Money.Value + amount < 0)
        {
            Money.Value = 0;
            return;
        }
        Money.Value += amount;
    }
    public void UpdateStage(int amount)
    {
        if (Stage.Value <= 0)
            return;
        if (Stage.Value + amount < 0)
        {
            Stage.Value = 0;
            return;
        }
        Stage.Value += amount;
    }
    public void UpdateTime(int amount) => Time.Value += amount;
    public void UpdateTotalTime(int time) => TotalTime.Value += time;
    public void InitTime() => Time.Value = 0;
    public void InitData()
    {
        Health.Value = 4;
        Bomb.Value = 4;
        Rope.Value = 4;
        Money.Value = 0;
        Stage.Value = 1;
        Time.Value = 0;
        TotalTime.Value = 0;
    }
}