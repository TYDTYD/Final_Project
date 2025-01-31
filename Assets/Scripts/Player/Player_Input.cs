using UnityEngine;
using System.Collections.Generic;
using System;
public class Player_Input : MonoBehaviour
{
    Dictionary<KeyCode, InputState> keyValue = new Dictionary<KeyCode, InputState>();
    Dictionary<KeyCode, InputAction> keyDelegate = new Dictionary<KeyCode, InputAction>();

    Player GetPlayer;
    Rigidbody2D Rigidbody2D;
    Idle idle;

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
        Rigidbody2D = GetPlayer.GetRigidbody;
        idle = new Idle(Rigidbody2D);

        InputAction[] InputActions = {
            new InputAction(1, new Move(GetPlayer, 7f, true)),
            new InputAction(1, new Move(GetPlayer, 7f, false)),
            new InputAction(1, new Up(GetPlayer)),
            new InputAction(1, new Down(GetPlayer)),
            new InputAction(0, new Attack(GetPlayer)),
            new InputAction(0, new Item()),
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
}