using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class MaskAnim : MonoBehaviour
{
    RectTransform GetRectTransform;
    Transform GetTransform;
    WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
    [SerializeField] Transform start;
    public IEnumerator GreaterScale(Vector3? pos = null, float targetSize=4000f)
    {
        // 초기 크기와 목표 크기 설정
        Vector2 initialSize = Vector3.zero;
        GetRectTransform.sizeDelta = initialSize;
        Vector2 targetSizeVec = new Vector2(targetSize, targetSize);

        // 화면 상 위치 업데이트
        if (pos.HasValue)
        {
            GetTransform.position = Camera.main.WorldToScreenPoint(pos.Value);
        }

        float elapsedTime = 0f;

        // 크기 변화 애니메이션 (Lerp 사용)
        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            GetRectTransform.sizeDelta = Vector2.Lerp(initialSize, targetSizeVec, elapsedTime / 2f);
            yield return null;
        }

        // 최종적으로 목표 크기 설정
        GetRectTransform.sizeDelta = targetSizeVec;
        yield return null;
    }

    public IEnumerator SmallerScale()
    {
        Vector2 value = new Vector2(50f, 50f);
        while (GetRectTransform.sizeDelta.x >= 0)
        {
            GetRectTransform.sizeDelta -= value;
            yield return waitForSeconds;
        }
    }

    public IEnumerator SmallerScale(Action act)
    {
        Vector2 value = new Vector2(10f, 10f);
        while (GetRectTransform.sizeDelta.x >= 0)
        {
            GetRectTransform.sizeDelta -= value;
            yield return waitForSeconds;
        }
        act?.Invoke();
    }

    private void Start()
    {
        GetRectTransform = GetComponent<RectTransform>();
        GetTransform = GetComponent<Transform>();
        StartCoroutine(GreaterScale(start.position));
    }

    public void MaskAnimStart_Small(Vector3 pos, Action change)
    {
        GetTransform.position = Camera.main.WorldToScreenPoint(pos);
        StartCoroutine(SmallerScale(change));
    }

    public void MaskAnimStart_Small(Vector3 pos)
    {
        GetTransform.position = Camera.main.WorldToScreenPoint(pos);
        StartCoroutine(SmallerScale());
    }

    public void MaskAnimStart_Small()
    {
        GetTransform.position = Vector3.zero;
        StartCoroutine(SmallerScale());
    }

    public void MaskAnimStart_Great(Vector3 pos)
    {
        GetRectTransform.sizeDelta = Vector3.zero;
        GetTransform.position = Camera.main.WorldToScreenPoint(pos);
        StartCoroutine(GreaterScale());
    }

    public void MaskAnimStart_Great()
    {
        GetTransform.position = Vector3.zero;
        StartCoroutine(GreaterScale());
    }
}