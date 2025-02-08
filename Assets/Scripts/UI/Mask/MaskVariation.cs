using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class MaskVariation : MonoBehaviour
{
    Image mask;
    void Start()
    {
        mask = GetComponent<Image>();
    }

    public void Darker(Action change)
    {
        StartCoroutine(FadeImageAlpha(mask, 1f, 0.8f,change));
    }

    public void Brighter(Action change)
    {
        StartCoroutine(FadeImageAlpha(mask, 0f, 0.8f,change));
    }

    IEnumerator FadeImageAlpha(Image image, float targetAlpha, float duration, Action change)
    {
        float startAlpha = image.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration; // 현재 진행률 (0~1)
            Color newColor = image.color;
            newColor.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            image.color = newColor;
            yield return null; // 매 프레임마다 업데이트 (더 부드러운 효과)
        }
        change?.Invoke();
        // 최종적으로 정확한 값 보정
        Color finalColor = image.color;
        finalColor.a = targetAlpha;
        image.color = finalColor;
    }

}