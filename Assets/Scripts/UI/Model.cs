using UnityEngine;
using UniRx;
public class Model : MonoBehaviour
{
    public static Model Instance { get; private set; }
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

    public IReactiveProperty<int> health = new ReactiveProperty<int>(4);
    public IReactiveProperty<int> bomb = new ReactiveProperty<int>(4);
    public IReactiveProperty<int> rope = new ReactiveProperty<int>(4);
    public IReactiveProperty<int> money = new ReactiveProperty<int>(0);
    public IReactiveProperty<int> stage = new ReactiveProperty<int>(1);
    public IReactiveProperty<int> time = new ReactiveProperty<int>(0);

    float second = 0f;
    int beforeSecond = 0;
    bool OnStage;

    private void Update()
    {
        if (OnStage)
            second += Time.deltaTime;

        int newSecond = Mathf.FloorToInt(second);
        if (newSecond != beforeSecond)
        {
            beforeSecond = newSecond;
            time.Value++;
        }
    }

    void InitData()
    {
        health.Value = 4;
        bomb.Value = 4;
        rope.Value = 4;
        money.Value = 0;
        stage.Value = 1;
        time.Value = 0;
    }
    public bool CurrentOnStage { get => OnStage; set => OnStage = value; }
}