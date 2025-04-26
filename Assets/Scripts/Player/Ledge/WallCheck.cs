using UnityEngine;
using player;
public class WallCheck : MonoBehaviour
{
    [SerializeField] Player_Rigidbody Player_Rb;
    bool wallContacted;
    private void OnEnable() => UpdateManager.Instance.SubscribeUpdate(UpdateMethod);
    private void OnDisable() => UpdateManager.Instance.UnSubscribeUpdate(UpdateMethod);
    private void UpdateMethod() => Player_Rb.GetWall = wallContacted;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            wallContacted = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            wallContacted = false;
        }
    }
}