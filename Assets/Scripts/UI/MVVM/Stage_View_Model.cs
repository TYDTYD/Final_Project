using UniRx;
public class Stage_View_Model
{
    Model GetModel;
    public IReactiveProperty<int> Health { get; private set; }
    public IReactiveProperty<int> Bomb { get; private set; }
    public IReactiveProperty<int> Rope { get; private set; }
    public IReactiveProperty<int> Money { get; private set; }
    public IReactiveProperty<int> Stage { get; private set; }
    public IReactiveProperty<int> Time { get; private set; }
    public IReactiveProperty<int> TotalTime { get; private set; }
    public Stage_View_Model(Model model)
    {
        GetModel = model;
        Health = GetModel.Health;
        Bomb = GetModel.Bomb;
        Rope = GetModel.Rope;
        Money = GetModel.Money;
        Stage = GetModel.Stage;
        Time = GetModel.Time;
        TotalTime = GetModel.TotalTime;
    }

    public void UpdateHealthUI(int amount) => GetModel.UpdateHealth(amount);
    public void UpdateBombUI(int amount) => GetModel.UpdateBomb(amount);
    public void UpdateRopeUI(int amount) => GetModel.UpdateRope(amount);
    public void UpdateMoneyUI(int amount) => GetModel.UpdateMoney(amount);
    public void UpdateStageUI(int amount) => GetModel.UpdateStage(amount);
    public void UpdateTimeUI(int time) => GetModel.UpdateTime(time);
    public void UpdateTotalTimeUI(int time) => GetModel.UpdateTotalTime(time);
    public void InitTimeUI() => GetModel.InitTime();
    public void InitData() => GetModel.InitData();
}