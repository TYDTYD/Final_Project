using UnityEngine;
using System.Collections;
using TMPro;
public class Credit : MonoBehaviour
{
    Vector2 initPos = new Vector2(10, -1200);
    Vector2 finalPos = new Vector2(10, 1500);
    [SerializeField] TextMeshProUGUI text;
    RectTransform rect;
    void Start()
    {
        rect = GetComponent<RectTransform>();
        StartCoroutine(MoveText());
    }
    IEnumerator MoveText()
    {
        float time = 30f;
        float idx = 0;
        while (idx < time)
        {
            idx += Time.deltaTime;
            rect.anchoredPosition = Vector2.Lerp(initPos, finalPos, idx / time);
            yield return null;
        }
        rect.anchoredPosition = finalPos;
        text.text = "Thank you for Playing!";
    }
}
