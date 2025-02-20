using UnityEngine;
using System.Collections;
using System;
public class MaskAnim : MonoBehaviour
{
    RectTransform GetRectTransform;
    Transform GetTransform;
    [SerializeField] Transform start;
    public IEnumerator ControlScale(Action act = null, Vector3? pos = null, float targetSize = 4000f, IEnumerator coroutine=null)
    {
        // 초기 크기 설정
        Vector2 initialSize = (targetSize == 0f) ? new Vector2(4000f, 4000f) : Vector2.zero;
        Vector2 targetSizeVec = (targetSize == 0f) ? Vector2.zero : new Vector2(targetSize, targetSize);

        GetRectTransform.sizeDelta = initialSize;

        // 화면 상 위치 업데이트
        if (pos.HasValue)
            GetTransform.position = Camera.main.WorldToScreenPoint(pos.Value);
        else
            GetTransform.position = Vector3.zero;

        float elapsedTime = 0f;
        float duration = 0.5f;

        // 크기 변화 애니메이션 (Lerp 사용)
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            GetRectTransform.sizeDelta = Vector2.Lerp(initialSize, targetSizeVec, elapsedTime / duration);
            yield return null;
        }

        // 최종적으로 목표 크기 설정
        GetRectTransform.sizeDelta = targetSizeVec;
        act?.Invoke();
        yield return coroutine;
    }

    private void Start()
    {
        GetRectTransform = GetComponent<RectTransform>();
        GetTransform = GetComponent<Transform>();
        StartCoroutine(ControlScale(pos: start.position));
    }
}