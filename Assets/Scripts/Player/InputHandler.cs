using UnityEngine;
using System;
using System.Collections.Generic;
public class InputHandler : MonoBehaviour
{
    public static List<KeyCode> keyCodes = new List<KeyCode>();
    public static KeyCode JumpKey;

    private void Awake()
    {
        foreach (Interact.KeySequence key in Enum.GetValues(typeof(Interact.KeySequence)))
        {
            if (key == Interact.KeySequence.Jump)
                JumpKey = Interact.GetKeyCode(key);
            keyCodes.Add(Interact.GetKeyCode(key));
        }
    }
}