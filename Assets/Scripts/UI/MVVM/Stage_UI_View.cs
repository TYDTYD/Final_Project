using UnityEngine;
using UniRx;
using TMPro;
using player;
public class Stage_UI_View : MonoBehaviour
{
    public static Stage_UI_View Instance { get; private set; }
    public Stage_View_Model View_Model;

    [SerializeField] TextMeshProUGUI hp_text;
    [SerializeField] TextMeshProUGUI bomb_text;
    [SerializeField] TextMeshProUGUI rope_text;
    [SerializeField] TextMeshProUGUI money_text;
    [SerializeField] TextMeshProUGUI time_text;
    [SerializeField] TextMeshProUGUI stage_text;

    float second = 0f;
    int beforeSecond = 0;
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

        GameManager.Instance.RestAreaLoad += UpdateTotalTime;
        GameManager.Instance.StageLoad += InitTime;
        GameManager.Instance.GetPlayer.GetComponent<Player_Health>().DeathEvent += () => gameObject.SetActive(false);
    }

    private void Update()
    {
        second += Time.deltaTime;
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
    public void InitTime() => View_Model.InitTimeUI();
    public void UpdateTotalTime() => View_Model.UpdateTotalTimeUI(View_Model.Time.Value);
    string ChangeIntToString(int t) => $"{t / 60:D2}:{t % 60:D2}";
}