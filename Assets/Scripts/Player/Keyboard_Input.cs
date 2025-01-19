using UnityEngine;
using System;
using TMPro;
using UniRx;
using UniRx.Triggers;
public class Keyboard_Input : MonoBehaviour
{
    public KeyCode Detect_Input()
    {
        foreach(KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(key))
            {
                if (key != KeyCode.Mouse0 && key != KeyCode.Mouse1 && key != KeyCode.Mouse2
                    && key != KeyCode.Mouse3 && key != KeyCode.Mouse4 && key != KeyCode.Mouse5
                    && key != KeyCode.Mouse6 && key != KeyCode.RightAlt)
                    return key;
            }
        }
        return KeyCode.None;
    }

    [SerializeField] TMP_InputField inputField;

    void Start()
    {
        inputField.onSubmit.AddListener(OnEndEdit);
        this.UpdateAsObservable()
            .Where(_ => inputField.isFocused)
            .Where(_ => KeyCode.None != Detect_Input())
            .Subscribe(_ => Accept_Input(Detect_Input()));
    }

    void Accept_Input(KeyCode key)
    {
        inputField.text = key.ToString();
        inputField.DeactivateInputField();  // 입력 후 InputField 비활성화(포커스 해제)
    }

    public void OnEndEdit(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inputField.text = text;
            inputField.ActivateInputField();
        }
    }
}