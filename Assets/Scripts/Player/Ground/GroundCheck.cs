using UnityEngine;
using player;
public class GroundCheck : MonoBehaviour
{
    [SerializeField] Player_Rigidbody Player_Rb;
    int GroundCount = 0;
    private void OnEnable() => UpdateManager.Instance.SubscribeUpdate(UpdateMethod);
    private void OnDisable() => UpdateManager.Instance.UnSubscribeUpdate(UpdateMethod);
    private void UpdateMethod()
    {
        if (GroundCount == 0)
            Player_Rb.GetGrounded = false;
        else
            Player_Rb.GetGrounded = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            GroundCount++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            GroundCount--;
        }
    }
}
