using UnityEngine;
using System;
public class Interact : MonoBehaviour
{
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
    static KeyCode Attack = KeyCode.A;
    static KeyCode Left = KeyCode.LeftArrow;
    static KeyCode Right = KeyCode.RightArrow;
    static KeyCode Up = KeyCode.UpArrow;
    static KeyCode Down = KeyCode.DownArrow;
    static KeyCode Item = KeyCode.Z;
    static KeyCode Rope = KeyCode.E;
    static KeyCode Bomb = KeyCode.B;
    static KeyCode Jump = KeyCode.Space;

    public static void SetKeyCode(string code, int index)
    {
        Enum.TryParse(code, out KeyCode keyCode);
        switch (index)
        {
            case (int)KeySequence.Left:
                Left = keyCode;
                break;
            case (int)KeySequence.Right:
                Right = keyCode;
                break;
            case (int)KeySequence.Up:
                Up = keyCode;
                break;
            case (int)KeySequence.Down:
                Down = keyCode;
                break;
            case (int)KeySequence.Attack:
                Attack = keyCode;
                break;
            case (int)KeySequence.Item:
                Item = keyCode;
                break;
            case (int)KeySequence.Jump:
                Jump = keyCode;
                break;
            case (int)KeySequence.Rope:
                Rope = keyCode;
                break;
            case (int)KeySequence.Bomb:
                Bomb = keyCode;
                break;
        }
        InputHandler.AcceptKey();
        return;
    }

    public static KeyCode GetKeyCode(KeySequence command)
    {
        switch (command)
        {
            case KeySequence.Left:
                return Left;
            case KeySequence.Right:
                return Right;
            case KeySequence.Up:
                return Up;
            case KeySequence.Down:
                return Down;
            case KeySequence.Attack:
                return Attack;
            case KeySequence.Item:
                return Item;
            case KeySequence.Jump:
                return Jump;
            case KeySequence.Rope:
                return Rope;
            case KeySequence.Bomb:
                return Bomb;
        }
        Debug.Log("경고! 키가 설정되어 있지 않습니다");
        return KeyCode.None;
    }
}

public class Bomb : ICommand
{
    public void Execute()
    {

    }
}

public class Rope : ICommand
{
    public void Execute()
    {

    }
}