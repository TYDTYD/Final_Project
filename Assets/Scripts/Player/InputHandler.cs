using UnityEngine;
using System;
using System.Collections.Generic;
public class InputHandler : MonoBehaviour
{
    public static List<KeyCode> keyCodes = new List<KeyCode>();
    public static KeyCode LeftKey;
    public static KeyCode RightKey;
    public static KeyCode UpKey;
    public static KeyCode DownKey;
    public static KeyCode AttackKey;
    public static KeyCode ItemKey;
    public static KeyCode JumpKey;
    public static KeyCode RopeKey;
    public static KeyCode BombKey;

    private void Awake()
    {
        AcceptKey();
    }

    public static void AcceptKey()
    {
        foreach (Interact.KeySequence key in Enum.GetValues(typeof(Interact.KeySequence)))
        {
            if (key == Interact.KeySequence.Jump)
                JumpKey = Interact.GetKeyCode(key);
            if (key == Interact.KeySequence.Up)
                UpKey = Interact.GetKeyCode(key);
            if (key == Interact.KeySequence.Down)
                DownKey = Interact.GetKeyCode(key);
            if (key == Interact.KeySequence.Attack)
                AttackKey = Interact.GetKeyCode(key);
            if (key == Interact.KeySequence.Item)
                ItemKey = Interact.GetKeyCode(key);
            if (key == Interact.KeySequence.Rope)
                RopeKey = Interact.GetKeyCode(key);
            if (key == Interact.KeySequence.Bomb)
                BombKey = Interact.GetKeyCode(key);
            if (key == Interact.KeySequence.Left)
                LeftKey = Interact.GetKeyCode(key);
            if (key == Interact.KeySequence.Right)
                RightKey = Interact.GetKeyCode(key);
            keyCodes.Add(Interact.GetKeyCode(key));
        }
    }
}