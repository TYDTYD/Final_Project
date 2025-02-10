using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections.Generic;
public class Player_Input : MonoBehaviour
{
    Dictionary<KeyCode, InputState> keyValue = new Dictionary<KeyCode, InputState>();
    Dictionary<KeyCode, InputAction> keyDelegate = new Dictionary<KeyCode, InputAction>();

    Player GetPlayer;
    Idle idle;
    Jump GetJump;
    Move RightMove;
    Move LeftMove;
    KeyCode JumpKey;
    bool isJumping = false;
    float maxholdTime = 0.1f, currentholdTime = 0f;
    CompositeDisposable disposables = new CompositeDisposable();

    class InputState
    {
        // 0 => 트리거 ||  1 => 연속적 트리거  ||  2 => 특수 트리거
        public int value;
        public bool isPressed;
        public InputState(int v, bool p)
        {
            value = v;
            isPressed = p;
        }
    }

    void EnableInput()
    {
        enabled = false;
    }

    struct InputAction
    {
        public int value;
        public ICommand GetDelegate;
        public InputAction(int v, ICommand d)
        {
            value = v;
            GetDelegate = d;
        }
    }

    void Start()
    {
        GetPlayer = GetComponent<Player>();
        idle = new Idle(GetPlayer.GetRigidbody);
        JumpKey = InputHandler.JumpKey;
        GetJump = new Jump(GetPlayer.GetRigidbody, 3f);
        RightMove = new Move(GetPlayer, 7f, true);
        LeftMove = new Move(GetPlayer, 7f, false);

        this.UpdateAsObservable()
            .Where(_ => (GetPlayer.GetPlayer_Rigidbody.isGrounded || GetPlayer.GetPlayer_Rigidbody.isClimbing) && Input.GetKey(JumpKey))
            .Subscribe(_ => StartJump())
            .AddTo(disposables);

        this.FixedUpdateAsObservable()
            .Where(_ => isJumping && Input.GetKey(JumpKey))
            .Subscribe(_ => ApplyJump())
            .AddTo(disposables);

        GetPlayer.GetPlayer_Health.DeathEvent += ClearUniRx;
        GetPlayer.GetPlayer_Health.DeathEvent += EnableInput;

        InputAction[] InputActions = {
            new InputAction(1, RightMove),
            new InputAction(1, LeftMove),
            new InputAction(1, new Up(GetPlayer)),
            new InputAction(1, new Down(GetPlayer)),
            new InputAction(0, new Attack(GetPlayer)),
            new InputAction(0, new Item(GetPlayer)),
            new InputAction(2, new Bomb()), // jump
            new InputAction(0, new Rope()),
            new InputAction(0, new Bomb())
        };

        for (int i = 0; i < InputActions.Length; i++)
        {
            var key = InputHandler.keyCodes[i];
            keyValue[key] = new InputState(InputActions[i].value, false);
            keyDelegate[key] = InputActions[i];
        }
    }

    private void FixedUpdate()
    {
        bool anyKeyPressed = false;
        foreach (var press in keyValue)
        {
            if (press.Value.isPressed && press.Value.value != 0)
            {
                keyDelegate[press.Key].GetDelegate.Execute();
                anyKeyPressed = true;
            }
        }

        if (!anyKeyPressed)
            idle.Execute();
    }

    void Update()
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
                keyDelegate[press.Key].GetDelegate.Execute();
        }
    }

    void StartJump()
    {
        if (isJumping)
            return;
        isJumping = true;
        GetPlayer.GetPlayer_Rigidbody.isGrounded = false;
        GetPlayer.GetPlayer_Rigidbody.isClimbing = false;
        currentholdTime = 0f;
    }

    void ApplyJump()
    {
        if (isJumping && currentholdTime < maxholdTime)
        {
            GetJump.Execute();
            currentholdTime += Time.fixedDeltaTime;
        }

        if (currentholdTime > maxholdTime || GetPlayer.GetPlayer_Rigidbody.isGrounded)
        {
            isJumping = false;
            currentholdTime = 0f;
        }
    }

    void ClearUniRx()
    {
        disposables.Clear();
    }

    public Move GetRightMove{
        get
        {
            return RightMove;
        }
    }

    public Move GetLeftMove
    {
        get
        {
            return LeftMove;
        }
    }
}