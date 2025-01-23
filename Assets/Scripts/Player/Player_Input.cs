using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class Player_Input : MonoBehaviour
{
    Rigidbody2D GetRigidbody2D;
    delegate void PlayerDelegate();
    List<PressDelegate> PlayerDelegates = new List<PressDelegate>();
    PlayerDelegate Left, Right, Up, Down, Attack, Item, Jump, Rope, Bomb;
    
    Jump GetJump;
    Dictionary<KeyCode, PressValue> keyValue = new Dictionary<KeyCode, PressValue>();
    Dictionary<KeyCode, PressDelegate> keyDelegate = new Dictionary<KeyCode, PressDelegate>();
    
    class PressValue
    {
        // 0 트리거 1 연속적 트리거 2 특수 트리거
        public int value;
        public bool isPressed;
        public PressValue(int v, bool p)
        {
            value = v;
            isPressed = p;
        }
    }

    struct PressDelegate
    {
        public int value;
        public PlayerDelegate GetDelegate;
        public PressDelegate(int v, PlayerDelegate d)
        {
            value = v;
            GetDelegate = d;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetRigidbody2D = GetComponent<Rigidbody2D>();
        PressDelegate LeftPress = new PressDelegate(1, Left);
        PressDelegate RightPress = new PressDelegate(1, Right);
        PressDelegate UpPress = new PressDelegate(1, Up);
        PressDelegate DownPress = new PressDelegate(1, Down);
        PressDelegate AttackPress = new PressDelegate(0, Attack);
        PressDelegate ItemPress = new PressDelegate(0, Item);
        PressDelegate JumpPress = new PressDelegate(2, Jump);
        PressDelegate RopePress = new PressDelegate(0, Rope);
        PressDelegate BombPress = new PressDelegate(0, Bomb);
        PlayerDelegates.AddRange(new PressDelegate[] { LeftPress, RightPress, UpPress, 
            DownPress, AttackPress, ItemPress, JumpPress, RopePress, BombPress });
        GetJump = new Jump(GetRigidbody2D, 100f);
        Jump += GetJump.Execute;
        int i = 0;
        foreach(KeyCode key in InputHandler.keyCodes)
        {
            keyValue[key] = new PressValue(PlayerDelegates[i].value, false);
            keyDelegate[key] = PlayerDelegates[i++];
        }
    }

    private void FixedUpdate()
    {
        foreach (var press in keyValue)
        {
            if (press.Value.isPressed)
            {
                keyDelegate[press.Key].GetDelegate();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var key in keyDelegate.Keys)
        {
            keyValue[key].isPressed = Input.GetKeyDown(key);
        }
    }

   
}