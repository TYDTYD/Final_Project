using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UniRx.Triggers;
using UniRx;

public class Keyboard_Input : MonoBehaviour
{
    bool isKey;
    [SerializeField] private Sprite[] blackKeySprites;
    [SerializeField] private Sprite[] whiteKeySprites;
    [SerializeField] private Image blackButtonDisplay;
    [SerializeField] private Image whiteButtonDisplay;
    [SerializeField] private Dictionary<string, Sprite> blackKeys = new Dictionary<string, Sprite>();
    [SerializeField] private Dictionary<string, Sprite> whiteKeys = new Dictionary<string, Sprite>();

    private void Start()
    {
        foreach (Sprite keySprite in blackKeySprites)
            blackKeys.Add(keySprite.name, keySprite);
        foreach (Sprite keySprite in whiteKeySprites)
            whiteKeys.Add(keySprite.name, keySprite);
    }

    private void KeyCodeRet()
    {
        Event ev = Event.current;
        if (ev.isKey)
        {
            string keyName = ev.keyCode.ToString();
            blackButtonDisplay.sprite = blackKeys.ContainsKey(keyName) ? blackKeys[keyName] : blackButtonDisplay.sprite;
            whiteButtonDisplay.sprite = whiteKeys.ContainsKey(keyName) ? whiteKeys[keyName] : whiteButtonDisplay.sprite;
        }
        
    }
}