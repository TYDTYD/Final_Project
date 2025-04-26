using UnityEngine;
using player;
public class LedgeCheck : MonoBehaviour
{
    [SerializeField] Player_Rigidbody Player_Rb;
    bool LedgeContacted;
    private void OnEnable() => UpdateManager.Instance.SubscribeUpdate(UpdateMethod);
    private void OnDisable() => UpdateManager.Instance.UnSubscribeUpdate(UpdateMethod);
    private void UpdateMethod() => Player_Rb.GetLedge = LedgeContacted;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            LedgeContacted = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            LedgeContacted = false;
        }
    }
}
