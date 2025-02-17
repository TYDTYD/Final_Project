using UnityEngine;
using UniRx;
public class Stage_UI_Presenter : MonoBehaviour
{
    public static Stage_UI_Presenter Instance { get; private set; }
    
    [SerializeField] Stage_UI_View Get_View;
    
    Player GetPlayer;

    private IReactiveProperty<int> health;
    public IReactiveProperty<int> bomb = new ReactiveProperty<int>(4);
    public IReactiveProperty<int> rope = new ReactiveProperty<int>(4);
    public IReactiveProperty<int> money = new ReactiveProperty<int>(0);
    public IReactiveProperty<int> time = new ReactiveProperty<int>(0);
    public IReactiveProperty<int> stage = new ReactiveProperty<int>(1);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        GameManager.Instance.StageLoad += OnStageStart;

        if (GetPlayer == null)
            OnStageStart();

        bomb.Subscribe(value => Get_View.Update_Bomb_UI(value.ToString())).AddTo(this);
        rope.Subscribe(value => Get_View.Update_Rope_UI(value.ToString())).AddTo(this);
        money.Subscribe(value => Get_View.Update_Money_UI(value.ToString())).AddTo(this);
        time.Subscribe(value => Get_View.Update_Time_UI(value.ToString())).AddTo(this);
        stage.Subscribe(value => Get_View.Update_Stage_UI(value.ToString())).AddTo(this);
    }

    void WaitBook()
    {
        Invoke("GotoBook", 2f);
    }
    void GotoBook()
    {
        Get_View.GetMaskVariation.Darker(CameraChange);
    }
    void CameraChange()
    {
        Get_View.GetDeathCam.gameObject.SetActive(true);
        Get_View.GetPlayCamera.gameObject.SetActive(false);
        Get_View.GetStatisticCamera.gameObject.SetActive(true);
        Get_View.GetMaskVariation.Brighter(null);
    }

    private void OnDestroy()
    {
        if (GetPlayer != null)
            GetPlayer.GetPlayer_Health.DeathEvent -= WaitBook;
    }

    void Init()
    {
        bomb.Value = 4;
        rope.Value = 4;
        money.Value = 0;
        time.Value = 0;
        stage.Value = 1;
    }

    void UpdateUI()
    {
        Get_View.Update_Hp_UI(health.Value.ToString());
        Get_View.Update_Bomb_UI(bomb.Value.ToString());
        Get_View.Update_Rope_UI(rope.Value.ToString());
        Get_View.Update_Money_UI(money.Value.ToString());
        Get_View.Update_Time_UI(time.Value.ToString());
        Get_View.Update_Stage_UI(stage.Value.ToString());
    }

    void OnStageStart() {
        GetPlayer = GameManager.Instance.GetPlayer.GetComponent<Player>();
        Get_View = GameObject.FindWithTag("View").GetComponent<Stage_UI_View>();

        health = GetPlayer.GetPlayer_Health.health;
        health.Subscribe(value => Get_View.Update_Hp_UI(value.ToString())).AddTo(this);
        GetPlayer.GetPlayer_Health.DeathEvent += WaitBook;

        UpdateUI();
    }
}