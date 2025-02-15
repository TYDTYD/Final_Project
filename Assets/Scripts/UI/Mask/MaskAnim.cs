using UnityEngine;
using System.Collections;
using System;
public class MaskAnim : MonoBehaviour
{
    RectTransform GetRectTransform;
    Transform GetTransform;
    WaitForSeconds waitForSeconds = new WaitForSeconds(0.0001f);
    IEnumerator GreaterScale()
    {
        Vector2 value = new Vector2(50f, 50f);
        while (GetRectTransform.sizeDelta.x < 4000)
        {
            GetRectTransform.sizeDelta += value;
            yield return waitForSeconds;
        }
        yield return null;
    }

    IEnumerator SmallerScale()
    {
        Vector2 value = new Vector2(50f, 50f);
        while (GetRectTransform.sizeDelta.x >= 0)
        {
            GetRectTransform.sizeDelta -= value;
            yield return waitForSeconds;
        }
        yield return null;
    }

    IEnumerator SmallerScale(Action act)
    {
        Vector2 value = new Vector2(10f, 10f);
        while (GetRectTransform.sizeDelta.x >= 0)
        {
            GetRectTransform.sizeDelta -= value;
            yield return waitForSeconds;
        }
        act?.Invoke();
        yield return null;
    }

    private void Start()
    {
        GetRectTransform = GetComponent<RectTransform>();
        GetTransform = GetComponent<Transform>();
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
        GetTransform.position = Camera.main.WorldToScreenPoint(pos);
        StartCoroutine(GreaterScale());
    }

    public void MaskAnimStart_Great()
    {
        GetTransform.position = Vector3.zero;
        StartCoroutine(GreaterScale());
    }
}