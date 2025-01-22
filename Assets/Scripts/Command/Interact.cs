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
    static KeyCode Attack;
    static KeyCode Left;
    static KeyCode Right;
    static KeyCode Up;
    static KeyCode Down;
    static KeyCode Item;
    static KeyCode Rope;
    static KeyCode Bomb;
    static KeyCode Jump;

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

public class Jump : ICommand
{
    public void Execute()
    {

    }
}

public class Move : ICommand
{
    public void Execute()
    {

    }
}

public class Attack : ICommand
{
    public void Execute()
    {

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

