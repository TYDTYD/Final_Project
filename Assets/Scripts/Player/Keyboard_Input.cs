using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UniRx.Triggers;
using UniRx;

public class Keyboard_Input : MonoBehaviour
{
    [SerializeField] private Sprite[] blackKeySprites;
    [SerializeField] public Image blackButtonDisplay;
    [SerializeField] private Dictionary<string, Sprite> blackKeys = new Dictionary<string, Sprite>();

    private void Start()
    {
        foreach (Sprite keySprite in blackKeySprites)
            blackKeys.Add(keySprite.name, keySprite);
    }
    private void OnGUI()
    {
        Event ev = Event.current;
        if (ev == null)
            return;
        if (ev.isKey && ev.keyCode!=KeyCode.Return)
        {
            string keyName = ev.keyCode.ToString();
            blackButtonDisplay.sprite = blackKeys.ContainsKey(keyName) ? blackKeys[keyName] : blackButtonDisplay.sprite;
        }
    }
}