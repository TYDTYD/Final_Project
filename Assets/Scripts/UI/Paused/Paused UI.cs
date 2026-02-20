using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class PausedUI : MonoBehaviour
{
    [SerializeField] Button[] GetButtons;
    [SerializeField] MaskVariation GetMask;
    RectTransform GetRectTransform;
    Vector2 originalPos = new Vector2(0, 1000);
    Vector2 target = Vector2.zero;
    int pos = 0;
    ColorBlock colorVar, original, selected;
    private void Awake()
    {
        GetRectTransform = GetComponent<RectTransform>();
    }

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
            Time.timeScale = 1f;
            switch (pos)
            {
                case 0:
                    gameObject.SetActive(false);
                    break;
                case 1:
                    
                    break;
                case 2:
                    GameManager.Instance.initialized = false;
                    StartCoroutine(GameManager.Instance.PreloadScene(1, GetMask.Darker()));
                    break;
                case 3:
                    StartCoroutine(GameManager.Instance.PreloadScene("Setting", GetMask.Darker()));
                    break;
                case 4:
                    StartCoroutine(GameManager.Instance.PreloadScene(0, GetMask.Darker()));
                    break;
                case 5:
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
    void OnEnable()
    {
        pos = 0;
        Time.timeScale = 0f;
        GetRectTransform.anchoredPosition = originalPos;
        StartCoroutine(PausedAnim(GetRectTransform.anchoredPosition, target));
    }
    IEnumerator PausedAnim(Vector2 pos, Vector2 targetPos)
    {
        float duration = 0.1f;
        float elapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            GetRectTransform.anchoredPosition = Vector2.Lerp(pos, targetPos, elapsedTime / duration);
            yield return null;
        }

        GetRectTransform.anchoredPosition = targetPos;
        yield return null;
    }
}
