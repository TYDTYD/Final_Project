using UnityEngine;

public class Monster_Rigidbody : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(collision.TryGetComponent(out IHealth health))
            {
                health.TakeDamage(1, 500, gameObject);
            }
        }
    }
}
