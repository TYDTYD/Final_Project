using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections;

public class Setting_Ui : MonoBehaviour
{
    [SerializeField] Button[] GetButtons;
    [SerializeField] RectTransform group;
    int pos = 0;
    Vector2 CachePosition;
    float initPosition;
    ColorBlock colorVar;
    ColorBlock original;

    private void Start()
    {
        colorVar = GetButtons[pos].colors;
        original = GetButtons[pos].colors;
        colorVar.normalColor = new Color(80/255f, 80/255f, 80 / 255f);
        CachePosition = group.anchoredPosition;
        initPosition = group.anchoredPosition.y;
        GetButtons[pos].colors = colorVar;
    }
    private IEnumerator MoveUI(float start, float end)
    {
        float time = 0;
        float duration = 0.5f;
        while (time<duration)
        {
            float t = time / duration;
            t = Mathf.Sin((t * Mathf.PI) / 2);
            float currentValue = Mathf.Lerp(start, end, t);
            time += Time.deltaTime;
            CachePosition.y = currentValue;
            group.anchoredPosition = CachePosition;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            pos = pos < GetButtons.Length - 1 ? pos + 1 : pos;
            GetButtons[pos].colors = colorVar;
            GetButtons[pos - 1].colors = original;
            if (pos > 3 && pos<GetButtons.Length-1)
            {
                float start = group.anchoredPosition.y;
                StartCoroutine(MoveUI(start, initPosition + 60 * (pos - 3)));
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pos = pos > 0 ? pos - 1 : pos;
            GetButtons[pos].colors = colorVar;
            GetButtons[pos + 1].colors = original;
            if (pos >= 3)
            {
                float start = group.anchoredPosition.y;
                StartCoroutine(MoveUI(start, initPosition + 60 * (pos - 3)));
            }
        }
    }
}
