using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using TMPro;
using player;
public class Stage_UI_View : MonoBehaviour
{
    public static Stage_UI_View Instance { get; private set; }
    Stage_View_Model View_Model;

    [SerializeField] TextMeshProUGUI hp_text;
    [SerializeField] TextMeshProUGUI bomb_text;
    [SerializeField] TextMeshProUGUI rope_text;
    [SerializeField] TextMeshProUGUI money_text;
    [SerializeField] TextMeshProUGUI time_text;
    [SerializeField] TextMeshProUGUI stage_text;

    float second = 0f;
    int beforeSecond = 0;
    Action onPlayerDeath;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
    }
    private void OnEnable() => UpdateManager.Instance.SubscribeUpdate(UpdateMethod);
    private void OnDisable() => UpdateManager.Instance.UnSubscribeUpdate(UpdateMethod);
    private void OnDestroy()
    {
        GameManager.Instance.StageLoad -= InitTime;
        GameManager.Instance.StageLoad -= InitMoney;
    }
    private void Start()
    {
        var model = new Model();
        View_Model = new Stage_View_Model(model);

        View_Model.Health.Subscribe(hp => hp_text.text = hp.ToString()).AddTo(this);
        View_Model.Bomb.Subscribe(bomb => bomb_text.text = bomb.ToString()).AddTo(this);
        View_Model.Rope.Subscribe(rope => rope_text.text = rope.ToString()).AddTo(this);
        View_Model.Money.Subscribe(money => money_text.text = money.ToString()).AddTo(this);
        View_Model.Time.Subscribe(time => time_text.text = ChangeIntToString(time)).AddTo(this);
        View_Model.Stage.Subscribe(stage => stage_text.text = stage.ToString()).AddTo(this);

        GameManager.Instance.StageLoad += InitTime;
        GameManager.Instance.StageLoad += InitMoney;
        GameManager.Instance.GetPlayer.GetComponent<Player_Health>().DeathEvent += () => gameObject.SetActive(false);
    }
    private void UpdateMethod()
    {
        second += Time.deltaTime;
        int newSecond = Mathf.FloorToInt(second);
        if (newSecond != beforeSecond)
        {
            beforeSecond = newSecond;
            IncreaseTime();
        }
    }
    public int GetHp => View_Model.Health.Value;
    public int GetBomb => View_Model.Bomb.Value;
    public int GetRope => View_Model.Rope.Value;
    public int GetMoney => View_Model.Money.Value;
    public int GetStageMoney => View_Model.CurrentMoney.Value;
    public int GetTime => View_Model.Time.Value;
    public int GetStage => View_Model.Stage.Value;
    public int GetTotalTime => View_Model.TotalTime.Value;
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
    public void InitTime() => View_Model.InitTimeUI();
    public void InitMoney() => View_Model.InitMoney();
    string ChangeIntToString(int t)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(t);
        return timeSpan.ToString(@"mm\:ss");
    }
}