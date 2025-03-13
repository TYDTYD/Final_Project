using UnityEngine;

public class Monster_Rigidbody : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.TryGetComponent(out IHealth health))
        {
            health.TakeDamage(1, 300, gameObject);
        }
    }
}
