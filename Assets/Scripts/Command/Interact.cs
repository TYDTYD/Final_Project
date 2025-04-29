using UnityEngine;
using System;
using System.Collections.Generic;

public enum KeySequence
{
    Left,
    Right,
    Up,
    Down,
    Attack,
    Item,
    Jump,
    Rope,
    Bomb
}
public class Interact : MonoBehaviour
{
    public static Interact instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        keyCodes.Add(KeyCode.LeftArrow);
        keyCodes.Add(KeyCode.RightArrow);
        keyCodes.Add(KeyCode.UpArrow);
        keyCodes.Add(KeyCode.DownArrow);
        keyCodes.Add(KeyCode.A);
        keyCodes.Add(KeyCode.Z);
        keyCodes.Add(KeyCode.Space);
        keyCodes.Add(KeyCode.E);
        keyCodes.Add(KeyCode.B);
    }

    public List<KeyCode> keyCodes { get; private set; } = new List<KeyCode>(); 

    KeyCode Left = KeyCode.LeftArrow;
    KeyCode Right = KeyCode.RightArrow;
    KeyCode Up = KeyCode.UpArrow;
    KeyCode Down = KeyCode.DownArrow;
    KeyCode Attack = KeyCode.A;
    KeyCode Item = KeyCode.Z;
    KeyCode Jump = KeyCode.Space;
    KeyCode Rope = KeyCode.E;
    KeyCode Bomb = KeyCode.B;

    public void SetKeyCode(string code, int index)
    {
        Enum.TryParse(code, out KeyCode keyCode);
        switch (index)
        {
            case (int)KeySequence.Left:
                Left = keyCode;
                keyCodes[(int)KeySequence.Left] = Left;
                Debug.Log($"keyCode : Left => {keyCode}");
                break;
            case (int)KeySequence.Right:
                Right = keyCode;
                keyCodes[(int)KeySequence.Right] = Right;
                Debug.Log($"keyCode : Right => {keyCode}");
                break;
            case (int)KeySequence.Up:
                Up = keyCode;
                keyCodes[(int)KeySequence.Up] = Up;
                Debug.Log($"keyCode : Up => {keyCode}");
                break;
            case (int)KeySequence.Down:
                Down = keyCode;
                keyCodes[(int)KeySequence.Down] = Down;
                Debug.Log($"keyCode : Down => {keyCode}");
                break;
            case (int)KeySequence.Attack:
                Attack = keyCode;
                keyCodes[(int)KeySequence.Attack] = Attack;
                Debug.Log($"keyCode : Attack => {keyCode}");
                break;
            case (int)KeySequence.Item:
                Item = keyCode;
                keyCodes[(int)KeySequence.Item] = Item;
                Debug.Log($"keyCode : Item => {keyCode}");
                break;
            case (int)KeySequence.Jump:
                Jump = keyCode;
                keyCodes[(int)KeySequence.Jump] = Jump;
                Debug.Log($"keyCode : Jump => {keyCode}");
                break;
            case (int)KeySequence.Rope:
                Rope = keyCode;
                keyCodes[(int)KeySequence.Rope] = Rope;
                Debug.Log($"keyCode : Rope => {keyCode}");
                break;
            case (int)KeySequence.Bomb:
                Bomb = keyCode;
                keyCodes[(int)KeySequence.Bomb] = Bomb;
                Debug.Log($"keyCode : Bomb => {keyCode}");
                break;
        }
        return;
    }
}
