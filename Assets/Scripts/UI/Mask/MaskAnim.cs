using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
public class MaskAnim : MonoBehaviour
{
    RectTransform GetTransform;
    public Action GetMaskGreater, GetMaskSmaller;
    WaitForSeconds waitForSeconds = new WaitForSeconds(0.001f);
    IEnumerator GreaterScale()
    {
        Vector2 value = new Vector2(40f, 40f);
        while (GetTransform.sizeDelta.x < 4000)
        {
            GetTransform.sizeDelta += value;
            yield return waitForSeconds;
        }
        yield return null;
    }

    IEnumerator SmallerScale()
    {
        Vector2 value = new Vector2(20f, 20f);
        while (GetTransform.sizeDelta.x >= 0)
        {
            GetTransform.sizeDelta -= value;
            yield return waitForSeconds;
        }
        yield return null;
    }

    private void Start()
    {
        GetTransform = GetComponent<RectTransform>();
        GetMaskGreater += MaskAnimStart_Great;
        GetMaskSmaller += MaskAnimStart_Small;
        GetMaskSmaller();
    }

    void MaskAnimStart_Small()
    {
        StartCoroutine(SmallerScale());
    }

    void MaskAnimStart_Great()
    {
        StartCoroutine(GreaterScale());
    }
}
