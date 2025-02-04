using UnityEngine;
using System.Collections;
using System;
public class MaskAnim : MonoBehaviour
{
    RectTransform GetRectTransform;
    Transform GetTransform;
    WaitForSeconds waitForSeconds = new WaitForSeconds(0.001f);
    [SerializeField] Transform startDoor, endDoor;
    Vector3 startPos, endPos;
    IEnumerator GreaterScale()
    {
        Vector2 value = new Vector2(40f, 40f);
        while (GetRectTransform.sizeDelta.x < 4000)
        {
            GetRectTransform.sizeDelta += value;
            yield return waitForSeconds;
        }
        yield return null;
    }

    IEnumerator SmallerScale()
    {
        Vector2 value = new Vector2(20f, 20f);
        while (GetRectTransform.sizeDelta.x >= 0)
        {
            GetRectTransform.sizeDelta -= value;
            yield return waitForSeconds;
        }
        yield return null;
    }

    private void Start()
    {
        GetRectTransform = GetComponent<RectTransform>();
        GetTransform = GetComponent<Transform>();
        startPos = Camera.main.WorldToScreenPoint(startDoor.position);

        endPos = Camera.main.WorldToScreenPoint(endDoor.position);
        GetTransform.position = startPos;
        Debug.Log(GetRectTransform.anchoredPosition);
    }

    public void MaskAnimStart_Small(Vector3 pos)
    {
        GetTransform.position = pos;
        StartCoroutine(SmallerScale());
    }

    public void MaskAnimStart_Small()
    {
        GetTransform.position = endPos;
        StartCoroutine(SmallerScale());
    }

    public void MaskAnimStart_Great(Vector3 pos)
    {
        GetTransform.position = pos;
        StartCoroutine(GreaterScale());
    }

    public void MaskAnimStart_Great()
    {
        GetTransform.position = startPos;
        StartCoroutine(GreaterScale());
    }
}