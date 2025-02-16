using UnityEngine;
using UniRx;
public class Stage_UI_Presenter : MonoBehaviour
{
    public static Stage_UI_Presenter Instance { get; private set; }

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
        }
    }

    [SerializeField] Player GetPlayer;
    [SerializeField] Stage_UI_View Get_View;
    public IReactiveProperty<int> health;
    public IReactiveProperty<int> bomb;
    public IReactiveProperty<int> rope;
    public IReactiveProperty<int> money;
    public IReactiveProperty<int> time;
    public IReactiveProperty<int> stage;

    private void Start()
    {
        if (GetPlayer == null)
            return;

        health = GetPlayer.GetPlayer_Health.health;
        bomb = new ReactiveProperty<int>(4);
        rope = new ReactiveProperty<int>(4);
        money = new ReactiveProperty<int>(0);
        time = new ReactiveProperty<int>(0);
        stage = new ReactiveProperty<int>(1);

        health.Subscribe(value => Get_View.Update_Hp_UI(value.ToString())).AddTo(this);
        bomb.Subscribe(value => Get_View.Update_Bomb_UI(value.ToString())).AddTo(this);
        rope.Subscribe(value => Get_View.Update_Rope_UI(value.ToString())).AddTo(this);
        money.Subscribe(value => Get_View.Update_Money_UI(value.ToString())).AddTo(this);
        time.Subscribe(value => Get_View.Update_Time_UI(value.ToString())).AddTo(this);
        stage.Subscribe(value => Get_View.Update_Stage_UI(value.ToString())).AddTo(this);
    }

    void initItem()
    {
        bomb.Value = 4;
        rope.Value = 4;
        money.Value = 0;
    }
}