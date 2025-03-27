using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DeathUI : MonoBehaviour
{
    [SerializeField] Button[] GetButtons;
    [SerializeField] TextMeshProUGUI[] GetTexts;
    [SerializeField] MaskVariation GetMask;
    int pos = 0;
    ColorBlock colorVar, original, selected;
    private void Start()
    {
        colorVar = GetButtons[pos].colors;
        original = GetButtons[pos].colors;
        selected = GetButtons[pos].colors;

        colorVar.normalColor = new Color(140f / 255f, 140f / 255f, 140f / 255f);
        selected.normalColor = new Color(80f / 255f, 80f / 255f, 80f / 255f);
        GetButtons[pos].colors = colorVar;

        GetTexts[0].text = Stage_UI_View.Instance.GetStage.ToString();
        GetTexts[1].text = Stage_UI_View.Instance.GetMoney.ToString();
        GetTexts[2].text = ChangeIntToString(Stage_UI_View.Instance.GetTime);
    }
    private void OnEnable() => UpdateManager.Instance.SubscribeUpdate(UpdateMethod);
    private void OnDisable() => UpdateManager.Instance.UnSubscribeUpdate(UpdateMethod);
    void UpdateMethod()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (pos)
            {
                case 0:
                    StartCoroutine(GameManager.Instance.PreloadScene(1, GetMask.Darker()));
                    break;
                case 1:
                    StartCoroutine(GameManager.Instance.PreloadScene(0, GetMask.Darker()));
                    break;
                case 2:
                    Application.Quit();
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            pos = pos < GetButtons.Length - 1 ? pos + 1 : pos;
            GetButtons[pos].colors = colorVar;
            GetButtons[pos - 1].colors = original;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pos = pos > 0 ? pos - 1 : pos;
            GetButtons[pos].colors = colorVar;
            GetButtons[pos + 1].colors = original;
        }
    }
    string ChangeIntToString(int t)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(t);
        return timeSpan.ToString(@"mm\:ss");
    }
}