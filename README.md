# SHADOW OF THE VALKYRIE
![Main Capsule](https://github.com/user-attachments/assets/40bd850d-9d49-406a-a737-22bfdf0791c9)



## Github CI 구축
![Image](https://github.com/user-attachments/assets/f97c08f0-135c-45c0-a6ce-a9ca4ef220e6)

Github Actions를 활용하여 자동화 빌드를 구축하였습니다.

## 키 변경 기능 구현
![Final_Project-Setting-WindowsMacLinux-Unity66000 0 26f1_DX11_2025-01-2423-01-05-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/f8652c26-1047-42bc-988f-3b41ad6610c1)
<details>
  <summary>
    명령 패턴 인터페이스를 활용한 입력 분리
  </summary>
  <pre>
    
```cs
public class Player_Input : MonoBehaviour
    {
        Dictionary<KeyCode, InputState> keyValue = new Dictionary<KeyCode, InputState>();
        Dictionary<KeyCode, InputAction> keyDelegate = new Dictionary<KeyCode, InputAction>();
        [SerializeField] GameObject anchor;
        [SerializeField] GameObject bomb;
        Player GetPlayer;
        Move RightMove;
        Move LeftMove;
        class InputState
        {
            // 0 => 트리거 ||  1 => 연속적 트리거
            public int value;
            public bool isPressed;
            public InputState(int v, bool p)
            {
                value = v;
                isPressed = p;
            }
        }
        void EnableInput() => enabled = true;
        void DisableInput() => enabled = false;
        struct InputAction
        {
            public int value;
            public ICommand Command;
            public InputAction(int v, ICommand c)
            {
                value = v;
                Command = c;
            }
        }
        void Start()
        {
            GetPlayer = GetComponent<Player>();
            RightMove = new Move(GetPlayer.GetRigidbody, 7f, true);
            LeftMove = new Move(GetPlayer.GetRigidbody, 7f, false);

            GetPlayer.GetPlayer_Health.DeathEvent += DisableInput;

            InputAction[] InputActions = {
            new InputAction(1, RightMove),
            new InputAction(1, LeftMove),
            new InputAction(1, new Up(transform,GetPlayer.GetRigidbody)),
            new InputAction(1, new Down(transform,GetPlayer.GetRigidbody)),
            new InputAction(0, new Attack(GetPlayer.GetRigidbody)),
            new InputAction(0, new Item(GetPlayer.GetPlayer_Item.CurrentItem)),
            new InputAction(0, new Jump(GetPlayer.GetRigidbody,15f)),
            new InputAction(0, new Rope(anchor)),
            new InputAction(0, new Bomb(bomb))
        };

            for (int i = 0; i < InputActions.Length; i++)
            {
                var key = InputHandler.keyCodes[i];
                keyValue[key] = new InputState(InputActions[i].value, false);
                keyDelegate[key] = InputActions[i];
            }
            // 업데이트 매니저의 Instance를 통해 함수를 추가합니다
            UpdateManager.Instance.SubscribeUpdate(UpdateMethod);
        }
        private void FixedUpdate()
        {
            foreach (var press in keyValue)
            {
                if (press.Value.isPressed && press.Value.value != 0)
                    keyDelegate[press.Key].Command.Execute(GetPlayer);
            }
        }
        void UpdateMethod()
        {
            foreach (var key in keyDelegate.Keys)
            {
                keyValue[key].isPressed = (keyValue[key].value == 0)
                ? Input.GetKeyDown(key)  // 단발 입력
                : Input.GetKey(key);     // 지속 입력
            }

            foreach (var press in keyValue)
            {
                if (press.Value.isPressed && press.Value.value == 0)
                    keyDelegate[press.Key].Command.Execute(GetPlayer);
            }
        }
        public Move GetRightMove => RightMove;
        public Move GetLeftMove => LeftMove;
    }
```
  </pre>
</details>
<details>
  <summary>
    명령 패턴 인터페이스
  </summary>
 <pre>
   
```cs
public interface ICommand
{
    void Execute();
    void Execute(Player player);
}
```
 </pre>
</details>
명령 패턴을 사용하여 키를 변경할 수 있도록 구현하였습니다.

## 업데이트 매니저 구현
<details>
  <summary>
    업데이트 매니저
  </summary>
  <pre>
    
```cs
public class UpdateManager : MonoBehaviour
{
    public static UpdateManager Instance { get; private set; }
    Action UpdateMethod;
    Action FixedUpdateMethod;
    Action LateUpdateMethod;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        UpdateMethod?.Invoke();
    }

    void FixedUpdate()
    {
        FixedUpdateMethod?.Invoke();
    }

    private void LateUpdate()
    {
        LateUpdateMethod?.Invoke();
    }
    // 구독하고자 하는 함수를 UpdateMethod에 추가합니다
    public void SubscribeUpdate(Action method) => UpdateMethod += method;
    // 구독 해제하고자 하는 함수를 UpdateMethod에서 제거합니다
    public void UnSubscribeUpdate(Action method) => UpdateMethod -= method;
    public void SubscribeFixedUpdate(Action method) => FixedUpdateMethod += method;
    public void UnSubscribeFixedUpdate(Action method) => FixedUpdateMethod -= method;
    public void SubscribeLateUpdate(Action method) => LateUpdateMethod += method;
    public void UnSubscribeLateUpdate(Action method) => LateUpdateMethod -= method;
}
```
  </pre>
</details>

## 마스크 기능 구현
![Final_Project-MaskTest-WindowsMacLinux-Unity66000 0 26f1_DX11_2025-02-0221-42-32-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/956c9b84-aed6-4cfd-a6f5-bfd7c9160f8d)
<details>
  <summary>
    Cut Out 마스크 기능
  </summary>
 <pre>
   
```cs
public class MaskAnim : MonoBehaviour
{
    RectTransform GetRectTransform;
    Transform GetTransform;
    [SerializeField] Transform start;
    public IEnumerator ControlScale(Action act = null, Vector3? pos = null, float targetSize = 4000f, IEnumerator coroutine=null)
    {
        // 초기 크기 설정
        Vector2 initialSize = (targetSize == 0f) ? new Vector2(4000f, 4000f) : Vector2.zero;
        Vector2 targetSizeVec = (targetSize == 0f) ? Vector2.zero : new Vector2(targetSize, targetSize);

        GetRectTransform.sizeDelta = initialSize;

        // 화면 상 위치 업데이트
        if (pos.HasValue)
            GetTransform.position = Camera.main.WorldToScreenPoint(pos.Value);
        else
            GetTransform.position = Vector3.zero;

        float elapsedTime = 0f;
        float duration = 0.5f;

        // 크기 변화 애니메이션 (Lerp 사용)
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            GetRectTransform.sizeDelta = Vector2.Lerp(initialSize, targetSizeVec, elapsedTime / duration);
            yield return null;
        }

        // 최종적으로 목표 크기 설정
        GetRectTransform.sizeDelta = targetSizeVec;
        act?.Invoke();
        yield return coroutine;
    }

    private void Start()
    {
        GetRectTransform = GetComponent<RectTransform>();
        GetTransform = GetComponent<Transform>();
        StartCoroutine(ControlScale(pos: start.position));
    }
}
```
 </pre>
</details>
마스크 기능을 통해 연출 효과를 넣었습니다.

## 로프 기능 구현
![Final_Project-Demo-WindowsMacLinux-Unity66000 0 26f1_DX11_2025-02-1518-10-13-ezgif com-video-to-gif-converter-min](https://github.com/user-attachments/assets/2bda6b15-8168-43f5-9f70-f30bb94ccdb9)
<details>
  <summary>
    로프 기능
  </summary>
 <pre>
   
```cs
public class Rope : ICommand
{
    GameObject Anchor;
    GameObject rope;

    Vector3 offset = new Vector3(0, 0.687f);

    public Rope(GameObject obj)
    {
        Anchor = obj;
    }
    IEnumerator MoveToTarget(GameObject obj,Transform start, Vector3 destination, float time)
    {
        Vector3 startPosition = start.position;
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            float t = Mathf.SmoothStep(0, 1, elapsedTime / time);
            start.position = Vector3.Lerp(startPosition, destination, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        start.position = destination;
        GameObject parent = start.GetChild(1).gameObject;
        yield return CreateRope(obj, parent.transform.position, parent, start.GetComponent<BoxCollider2D>());
    }
    IEnumerator CreateRope(GameObject owner, Vector3 startPos, GameObject parent, BoxCollider2D anchor)
    {
        Vector3 pos = startPos;
        Vector2 sizeOffset = new Vector2(0, 0.275f);
        while (pos.y > owner.transform.position.y + 1f)
        {
            pos -= offset;
            GameObject obj = Object.Instantiate(parent, rope.transform);
            anchor.size += 2 * sizeOffset;
            anchor.offset -= sizeOffset;
            obj.transform.position -= offset;
            HingeJoint2D hinge = obj.GetComponent<HingeJoint2D>();
            hinge.connectedBody = parent.GetComponent<Rigidbody2D>();
            hinge.anchor = new Vector2(1.11f, 0);
            hinge.connectedAnchor = Vector2.zero;
            parent = obj;
            yield return null;
        }
    }
    public void Execute()
    {

    }
    public void Execute(Player player)
    {
        int layerMask = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.up, 10f, layerMask);

        if (hit.collider == null)
            return;

        if (Stage_UI_View.Instance.View_Model.Rope.Value <= 0)
            return;

        Stage_UI_View.Instance.DecreaseRope(1);
        Vector3 pos = new Vector3(Mathf.Round(hit.point.x), hit.point.y);
        rope = Object.Instantiate(Anchor);
        rope.transform.position = player.transform.position;
        player.StartCoroutine(MoveToTarget(player.gameObject, rope.transform, pos, 0.2f));
    }
}
```
 </pre>
</details>
로프 기능을 통해 밧줄을 타고 다닐 수 있도록 하였습니다.

## 통계창 구현
![Final_Project-Stage1-WindowsMacLinux-Unity66000 0 26f1_DX11_2025-03-0619-46-20-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/499a337b-f5cf-4cb0-9524-38b3ef09fa6b)
<details>
  <summary>
    통계 UI
  </summary>
 <pre>
   
```cs
public class StageRestView : MonoBehaviour
{
    Stage_UI_View view;

    [SerializeField] TextMeshProUGUI ThisLevelTime;
    [SerializeField] TextMeshProUGUI ThisLevelMoney;
    [SerializeField] TextMeshProUGUI TotalTime;
    [SerializeField] TextMeshProUGUI TotalMoney;
    [SerializeField] TextMeshProUGUI hp_text;
    [SerializeField] TextMeshProUGUI bomb_text;
    [SerializeField] TextMeshProUGUI rope_text;
    [SerializeField] TextMeshProUGUI stage_text;
    void Start()
    {
        view = Stage_UI_View.Instance;
        UpdateUI();
    }
    void UpdateUI()
    {
        ThisLevelTime.text = ChangeIntToString(view.View_Model.Time.Value);
        ThisLevelMoney.text = "+" + view.View_Model.Money.Value.ToString();
        TotalTime.text = ChangeIntToString(view.View_Model.TotalTime.Value);
        TotalMoney.text = view.View_Model.Money.Value.ToString();
        hp_text.text = view.View_Model.Health.Value.ToString();
        bomb_text.text = view.View_Model.Bomb.Value.ToString();
        rope_text.text = view.View_Model.Rope.Value.ToString();
        stage_text.text = view.View_Model.Stage.Value.ToString() + " Completed!";
    }
    string ChangeIntToString(int t) => $"{t / 60:D2}:{t % 60:D2}";
}
```
 </pre>
</details>
스테이지 중간마다 플레이 기록에 따른 통계창을 볼 수 있도록 구현하였습니다.

## Coroutine + AsyncLoad를 통한 비동기 씬 로드 구현
<details>
  <summary>
    코루틴을 통한 비동기 씬 로드
  </summary>
 <pre>
   
```cs
public partial class GameManager : MonoBehaviour
{
    Dictionary<int, string> SceneIndex = new Dictionary<int, string>(); 
    float realStartTime = 0f;

    string[] scenesToLoad = { "Title", "Stage 1", "Stage 2", "Stage 3", "Stage 4", "Stage Rest", "Game Over", "Setting", "Statistic" };
    
    void CacheScenes(string[] sceneNames)
    {
        for (int i = 0; i < sceneNames.Length; i++)
        {
            string sceneName = sceneNames[i];
            SceneIndex.Add(i, sceneName);
        }
    }
    public IEnumerator PreloadScene(int index, IEnumerator coroutine)
    {
        startTime = Time.realtimeSinceStartup;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneIndex[index]);
        asyncLoad.allowSceneActivation = false;
        yield return coroutine;
        realStartTime = Time.realtimeSinceStartup;
        yield return new WaitUntil(() => asyncLoad.progress >= 0.9f);
        asyncLoad.allowSceneActivation = true;
    }
    public IEnumerator PreloadScene(string sceneName, IEnumerator coroutine)
    {
        startTime = Time.realtimeSinceStartup;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        yield return coroutine;
        realStartTime = Time.realtimeSinceStartup;
        yield return new WaitUntil(() => asyncLoad.progress >= 0.9f);
        asyncLoad.allowSceneActivation = true;
    }
}
```
 </pre>
</details>
Scene을 동기적으로 로드하면 0.5초의 로딩시간이 걸렸습니다.

비동기적으로 Scene을 로드하고, 로딩 시간동안 CutOut UI를 활용하여 애니메이션을 연출한다면 사용자 체감상 로딩 시간이 느껴지지 않을 것이라 생각했습니다.

AsyncLoad 함수를 사용하여 Scene을 비동기적으로 로드하는 동안 Coroutine을 호출하여 CutOut UI 애니메이션을 연출하였습니다.

테스트해 본 결과, 비동기적으로 Scene을 호출하고 Coroutine이 끝난 뒤, Scene 전환에 걸리는 시간과 동기적으로 Scene을 호출하는 시간은 다음과 같습니다.

![image](https://github.com/user-attachments/assets/81a7df33-bc41-473a-9be0-ae1f2f42c02b)
![image](https://github.com/user-attachments/assets/20b8e1f3-f445-45ae-9c03-5d5440fea6e5)

0.578726초 => 0.3584538초로 약 37.85% 성능을 향상시켰습니다.

![image](https://github.com/user-attachments/assets/2dd28d4d-855a-4209-8e3a-4b004237cc62)
# CutOut 애니메이션을 Coroutine을 통해 호출

비동기적으로 씬을 로드하여 딜레이를 없앴습니다.

## UniRx를 활용한 MVVM 패턴 구현
![image](https://github.com/user-attachments/assets/b00b3f3b-ce17-4bef-a2ed-9d2c63b822fd)
<details>
  <summary>
    Model
  </summary>
 <pre>
   
```cs
public class Model
{
    IReactiveProperty<int> health = new ReactiveProperty<int>(4);
    IReactiveProperty<int> bomb = new ReactiveProperty<int>(4);
    IReactiveProperty<int> rope = new ReactiveProperty<int>(4);
    IReactiveProperty<int> money = new ReactiveProperty<int>(0);
    IReactiveProperty<int> stage = new ReactiveProperty<int>(1);
    IReactiveProperty<int> time = new ReactiveProperty<int>(0);
    IReactiveProperty<int> totalTime = new ReactiveProperty<int>(0);
    public IReadOnlyReactiveProperty<int> Health => health;
    public IReadOnlyReactiveProperty<int> Bomb => bomb;
    public IReadOnlyReactiveProperty<int> Rope => rope;
    public IReadOnlyReactiveProperty<int> Money => money;
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
        if (Money.Value <= 0)
            return;
        if (Money.Value + amount < 0)
        {
            money.Value = 0;
            return;
        }
        money.Value += amount;
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
}
```
 </pre>
</details>
데이터 관리 및 규칙을 선언하였습니다.

<details>
  <summary>
    View
  </summary>
 <pre>
   
```cs
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
```
 </pre>
</details>
사용자가 보고 있는 화면 UI를 연결하고 View Model을 통해 변화된 값을 출력하였습니다.

<details>
  <summary>
    View Model
  </summary>
 <pre>
   
```cs
public class Stage_View_Model
{
    Model GetModel;
    public IReadOnlyReactiveProperty<int> Health;
    public IReadOnlyReactiveProperty<int> Bomb;
    public IReadOnlyReactiveProperty<int> Rope;
    public IReadOnlyReactiveProperty<int> Money;
    public IReadOnlyReactiveProperty<int> Stage;
    public IReadOnlyReactiveProperty<int> Time;
    public IReadOnlyReactiveProperty<int> TotalTime;
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
}
```
 </pre>
</details>
Model과 View를 연결하고 데이터를 Observable 형태로 유지하였습니다.

UniRx를 활용하여 UI를 쉽게 관리할 수 있게끔 Model - View - View Model 패턴을 구성하였습니다.

## Sprite Atlas를 활용하여 드로우 콜 감소
Sprite Atlas를 활용하여 Batches 수를 줄였습니다.
