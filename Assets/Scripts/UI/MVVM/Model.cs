using UniRx;
public class Model
{
    IReactiveProperty<int> health = new ReactiveProperty<int>(4);
    IReactiveProperty<int> bomb = new ReactiveProperty<int>(4);
    IReactiveProperty<int> rope = new ReactiveProperty<int>(4);
    IReactiveProperty<int> money = new ReactiveProperty<int>(0);
    IReactiveProperty<int> currentMoney = new ReactiveProperty<int>(0);
    IReactiveProperty<int> stage = new ReactiveProperty<int>(1);
    IReactiveProperty<int> time = new ReactiveProperty<int>(0);
    IReactiveProperty<int> totalTime = new ReactiveProperty<int>(0);
    public IReadOnlyReactiveProperty<int> Health => health;
    public IReadOnlyReactiveProperty<int> Bomb => bomb;
    public IReadOnlyReactiveProperty<int> Rope => rope;
    public IReadOnlyReactiveProperty<int> Money => money;
    public IReadOnlyReactiveProperty<int> CurrentMoney => currentMoney;
    public IReadOnlyReactiveProperty<int> Stage => stage;
    public IReadOnlyReactiveProperty<int> Time => time;
    public IReadOnlyReactiveProperty<int> TotalTime => totalTime;
    public void UpdateHealth(int amount)
    {
        if (Health.Value <= 0)
            return;
        if (Health.Value + amount < 0)
        {
            health.Value = 0;
            return;
        }
        health.Value += amount;
    }
    public void UpdateBomb(int amount)
    {
        if (Bomb.Value <= 0)
            return;
        if (Bomb.Value + amount < 0)
        {
            bomb.Value = 0;
            return;
        }
        bomb.Value += amount;
    }
    public void UpdateRope(int amount)
    {
        if (Rope.Value <= 0)
            return;
        if (Rope.Value + amount < 0)
        {
            rope.Value = 0;
            return;
        }
        rope.Value += amount;
    }
    public void UpdateMoney(int amount)
    {
        if (Money.Value < 0)
            return;
        if (Money.Value + amount < 0)
        {
            money.Value = 0;
            return;
        }
        money.Value += amount;
        currentMoney.Value += amount;
    }
    public void UpdateStage(int amount)
    {
        if (Stage.Value <= 0)
            return;
        if (Stage.Value + amount < 0)
        {
            stage.Value = 0;
            return;
        }
        stage.Value += amount;
    }
    public void UpdateTime(int amount) => time.Value += amount;
    public void UpdateTotalTime(int time) => totalTime.Value += time;
    public void InitTime() => time.Value = 0;
    public void InitMoney() => currentMoney.Value = 0;
    public void InitHealth() => health.Value = 4;
    public void InitItem()
    {
        bomb.Value = 0;
        rope.Value = 0;
        money.Value = 0;
    }
}