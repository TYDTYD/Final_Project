using UnityEngine;

public class FollowLight : MonoBehaviour
{
    GameObject obj;
    private void OnEnable() => UpdateManager.Instance.SubscribeLateUpdate(LateUpdateMethod);
    private void OnDisable() => UpdateManager.Instance.UnSubscribeLateUpdate(LateUpdateMethod);
    private void Start()
    {
        obj = FindAnyObjectByType<player.Player>().gameObject;
    }

    private void LateUpdateMethod()
    {
        transform.position = obj.transform.position;
    }
}
