using UnityEngine;
using player;
public class LedgeDetection : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Player player;

    int canDectected = 0;
    private void OnEnable() => UpdateManager.Instance.SubscribeUpdate(UpdateMethod);
    private void OnDisable() => UpdateManager.Instance.UnSubscribeUpdate(UpdateMethod);
    private void UpdateMethod()
    {
        if (canDectected == 0)
            player.GetPlayer_Rigidbody.ledgeDetected = Physics2D.OverlapCircle(transform.position, radius, whatIsGround);
        else
            player.GetPlayer_Rigidbody.ledgeDetected = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            canDectected++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            canDectected--;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
