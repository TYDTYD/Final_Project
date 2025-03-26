using UnityEngine;
using player;
public class LedgeDetection : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Player player;

    bool canDectected;

    private void Update()
    {
        if (canDectected)
            player.GetPlayer_Rigidbody.ledgeDetected = Physics2D.OverlapCircle(transform.position, radius, whatIsGround);
        else
            player.GetPlayer_Rigidbody.ledgeDetected = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            canDectected = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            canDectected = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
