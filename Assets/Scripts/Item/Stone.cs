using UnityEngine;

public class Stone : MonoBehaviour, IItem 
{
    bool isDamaged = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Ladder"))
        {
            isDamaged = false;
        }
        else if (isDamaged)
        {
            if (collision.gameObject.TryGetComponent(out IHealth health))
            {
                health.TakeDamage(1);
            }
            if (collision.gameObject.TryGetComponent(out Rigidbody2D rb))
            {
                Vector3 knockbackDir = (collision.transform.position - transform.position).normalized;
                rb.AddForce(knockbackDir * 500);
            }
        }
    }
}