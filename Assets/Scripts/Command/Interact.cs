using UnityEngine;
using System;
using System.Collections.Generic;
public class Interact : MonoBehaviour
{
    public static Interact instance;

    public Interact Instance
    {
        get => instance = this;
    }
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

    public List<KeyCode> keyCodes = new List<KeyCode>();

    public KeyCode Left = KeyCode.LeftArrow;
    public KeyCode Right = KeyCode.RightArrow;
    public KeyCode Up = KeyCode.UpArrow;
    public KeyCode Down = KeyCode.DownArrow;
    public KeyCode Attack = KeyCode.A;
    public KeyCode Item = KeyCode.Z;
    public KeyCode Jump = KeyCode.Space;
    public KeyCode Rope = KeyCode.E;
    public KeyCode Bomb = KeyCode.B;

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
