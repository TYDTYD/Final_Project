using UnityEngine;

public class LevelBackGround : MonoBehaviour
{
    [SerializeField] Transform CameraPos;
    void Start()
    {
        transform.SetParent(CameraPos);
    }
}