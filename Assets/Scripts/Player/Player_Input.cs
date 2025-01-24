using UnityEngine;
using System.Collections.Generic;
using System;
public class Player_Input : MonoBehaviour
{
    Action Left, Right, Up, Down, Attack, Item, Jump, Rope, Bomb;
    Dictionary<KeyCode, InputState> keyValue = new Dictionary<KeyCode, InputState>();
    Dictionary<KeyCode, InputAction> keyDelegate = new Dictionary<KeyCode, InputAction>();
    
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
        public Action GetDelegate;
        public InputAction(int v, Action d)
        {
            value = v;
            GetDelegate = d;
        }
    }

    void Start()
    {
        InputAction[] InputActions = {
            new InputAction(1, Left), new InputAction(1, Right),
            new InputAction(1, Up), new InputAction(1, Down),
            new InputAction(0, Attack), new InputAction(0, Item),
            new InputAction(2, Jump), new InputAction(0, Rope),
            new InputAction(0, Bomb)
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
                keyDelegate[press.Key].GetDelegate?.Invoke();
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