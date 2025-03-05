using UnityEngine;
using UniRx;
using TMPro;
public class Stage_UI_View : MonoBehaviour
{
    public static Stage_UI_View Instance { get; private set; }

    Stage_View_Model View_Model;
    public IReactiveProperty<int> Health { get; private set; }
    public IReactiveProperty<int> Bomb { get; private set; }
    public IReactiveProperty<int> Rope { get; private set; }
    public IReactiveProperty<int> Money { get; private set; }
    public IReactiveProperty<int> Stage { get; private set; }
    public IReactiveProperty<int> Time { get; private set; }

    [SerializeField] TextMeshProUGUI hp_text;
    [SerializeField] TextMeshProUGUI bomb_text;
    [SerializeField] TextMeshProUGUI rope_text;
    [SerializeField] TextMeshProUGUI money_text;
    [SerializeField] TextMeshProUGUI time_text;
    [SerializeField] TextMeshProUGUI stage_text;

    float second = 0f;
    int beforeSecond = 0;
    bool OnStage = true;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }
    private void Start()
    {
        var model = new Model();
        View_Model = new Stage_View_Model(model);

        Health = View_Model.Health;
        Bomb = View_Model.Bomb;
        Rope = View_Model.Rope;
        Money = View_Model.Money;
        Time = View_Model.Time;
        Stage = View_Model.Stage;

        Health.Subscribe(hp => hp_text.text = hp.ToString()).AddTo(this);
        Bomb.Subscribe(bomb => bomb_text.text = bomb.ToString()).AddTo(this);
        Rope.Subscribe(rope => rope_text.text = rope.ToString()).AddTo(this);
        Money.Subscribe(money => money_text.text = money.ToString()).AddTo(this);
        Time.Subscribe(time => time_text.text = ChangeIntToString(time)).AddTo(this);
        Stage.Subscribe(stage => stage_text.text = stage.ToString()).AddTo(this);
    }

    private void Update()
    {
        second += UnityEngine.Time.deltaTime;
        int newSecond = Mathf.FloorToInt(second);
        if (newSecond != beforeSecond)
        {
            beforeSecond = newSecond;
            IncreaseTime();
        }
    }
    public void IncreaseHealth(int amount) => View_Model.UpdateHealthUI(amount);
    public void DecreaseHealth(int amount) => View_Model.UpdateHealthUI(-amount);
    public void IncreaseBomb(int amount) => View_Model.UpdateBombUI(amount);
    public void DecreaseBomb(int amount) => View_Model.UpdateBombUI(-amount);
    public void IncreaseRope(int amount) => View_Model.UpdateRopeUI(amount);
    public void DecreaseRope(int amount) => View_Model.UpdateRopeUI(-amount);
    public void IncreaseMoney(int amount) => View_Model.UpdateMoneyUI(amount);
    public void DecreaseMoney(int amount) => View_Model.UpdateMoneyUI(-amount);
    public void IncreaseStage() => View_Model.UpdateStageUI(1);
    public void IncreaseTime() => View_Model.UpdateTimeUI(1);
    string ChangeIntToString(int t) => $"{t / 60:D2}:{t % 60:D2}";
    public bool CurrentOnStage { get => OnStage; set => OnStage = value; }
}