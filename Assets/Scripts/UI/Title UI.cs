using UnityEngine;
using UnityEngine.UI;
public class TitleUI : MonoBehaviour
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (pos)
            {
                case 0:
                    GetMask.Darker(GameManager.Instance.PreloadScene(1));
                    break;
                case 1:
                    GetMask.Darker(GameManager.Instance.PreloadScene("Setting"));
                    break;
                case 2:
                    break;
                case 3:
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