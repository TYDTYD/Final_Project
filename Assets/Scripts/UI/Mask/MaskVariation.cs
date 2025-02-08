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
            float t = elapsedTime / duration; // ���� ����� (0~1)
            Color newColor = image.color;
            newColor.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            image.color = newColor;
            yield return null; // �� �����Ӹ��� ������Ʈ (�� �ε巯�� ȿ��)
        }
        change?.Invoke();
        // ���������� ��Ȯ�� �� ����
        Color finalColor = image.color;
        finalColor.a = targetAlpha;
        image.color = finalColor;
    }

}