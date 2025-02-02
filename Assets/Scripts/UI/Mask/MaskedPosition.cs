using UnityEngine;

public class MaskedPosition : MonoBehaviour
{
    Vector2 pos = new Vector2(960, 540);
    RectTransform GetRectTransform;
    private void Start()
    {
        GetRectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        GetRectTransform.position = pos;
    }
}
