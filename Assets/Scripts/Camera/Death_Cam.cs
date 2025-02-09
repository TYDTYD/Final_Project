using UnityEngine;

public class Death_Cam : MonoBehaviour
{
    [SerializeField] Transform follow;
    Vector3 offset = new Vector3(0,0,10);
    void LateUpdate()
    {
        transform.position = follow.position - offset;
    }
}
