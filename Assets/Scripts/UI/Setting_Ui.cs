using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections;

public class Setting_Ui : MonoBehaviour
{
    WaitForSeconds seconds = new WaitForSeconds(2f);
    [SerializeField] Button[] GetButtons;
    [SerializeField] VerticalLayoutGroup group;
    float start, end;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //GetButtons[0].getcom
    }

    private IEnumerator MoveUI(float start, float end)
    {
        float time = 0;
        float duration = 1f;
        while (time<duration)
        {
            float t = time / duration;
            t = Mathf.Sin((t * Mathf.PI) / 2);

            float currentValue = Mathf.Lerp(start, end, t);

            time += Time.deltaTime;
            Debug.Log(currentValue);
            yield return null;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(MoveUI(0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {

        }
    }
}
