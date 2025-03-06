using UnityEngine;
using System.Collections;

public class StasticChartAnim : MonoBehaviour
{
    RectTransform GetRectTransform;
    Vector2 pos, middlePos, targetPos;
    void Start()
    {
        GetRectTransform = GetComponent<RectTransform>();
        pos = GetRectTransform.anchoredPosition;
        targetPos = new Vector2(0, -100);
        middlePos = new Vector2(0, -110);
        StartCoroutine(MoveChart(pos,targetPos));
    }

    IEnumerator MoveChart(Vector2 pos,Vector2 targetPos)
    {
        float elapsedTime = 0f;
        float duration = 0.4f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            GetRectTransform.anchoredPosition = Vector2.Lerp(pos, middlePos, elapsedTime / duration);
            yield return null;
        }

        GetRectTransform.anchoredPosition = middlePos;
        duration = 0.1f;
        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            GetRectTransform.anchoredPosition = Vector2.Lerp(middlePos, targetPos, elapsedTime / duration);
            yield return null;
        }

        GetRectTransform.anchoredPosition = targetPos;
        yield return null;
    }
}