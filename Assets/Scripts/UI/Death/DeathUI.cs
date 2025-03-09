using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DeathUI : MonoBehaviour
{
    [SerializeField] Button[] GetButtons;
    [SerializeField] MaskVariation GetMask;
    int pos = 0;
    ColorBlock colorVar, original, selected;
    private void Start()
    {
        colorVar = GetButtons[pos].colors;
        original = GetButtons[pos].colors;
        selected = GetButtons[pos].colors;
        colorVar.normalColor = new Color(140 / 255f, 140 / 255f, 140 / 255f);
        selected.normalColor = new Color(80 / 255f, 80 / 255f, 80 / 255f);
        GetButtons[pos].colors = colorVar;
    }

    // Update is called once per frame
    void Update()
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
}