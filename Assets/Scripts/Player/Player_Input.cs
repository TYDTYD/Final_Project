using UnityEngine;
using System.Collections.Generic;
using System;
public class Player_Input : MonoBehaviour
{
    Dictionary<KeyCode, InputState> keyValue = new Dictionary<KeyCode, InputState>();
    Dictionary<KeyCode, InputAction> keyDelegate = new Dictionary<KeyCode, InputAction>();

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

        Rigidbody2D = GetComponent<Rigidbody2D>();

        idle = new Idle(Rigidbody2D);
        InputAction[] InputActions = {
            new InputAction(1, new Move(Rigidbody2D, 5f, true)), 
            new InputAction(1, new Move(Rigidbody2D, 5f, false)),
            new InputAction(1, new Bomb()), // up
            new InputAction(1, new Bomb()), // down
            new InputAction(0, new Attack()), 
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
        foreach (var press in keyValue)
        {
            if (press.Value.isPressed)
                keyDelegate[press.Key].GetDelegate.Execute();
            else
                idle.Execute();
        }
    }

    void Update()
    {
        foreach (var key in keyDelegate.Keys)
        {
            switch (keyValue[key].value)
            {
                case 0: // 단발 입력
                    keyValue[key].isPressed = Input.GetKeyDown(key);
                    break;
                case 1: // 지속 입력
                    keyValue[key].isPressed = Input.GetKey(key);
                    break;
                case 2: // 특수 입력
                    keyValue[key].isPressed = Input.GetKey(key);
                    break;
            }
        }
    }
}