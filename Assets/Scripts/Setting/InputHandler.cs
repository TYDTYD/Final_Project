using UnityEngine;
using System;
using System.Collections.Generic;
public class InputHandler : MonoBehaviour
{
    public static List<KeyCode> keyCodes=new List<KeyCode>();

    private void Start()
    {
        foreach (Interact.KeySequence key in Enum.GetValues(typeof(Interact.KeySequence)))
        {
            keyCodes.Add(Interact.GetKeyCode(key));
        }
    }
    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}