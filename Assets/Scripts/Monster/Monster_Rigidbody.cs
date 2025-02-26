using UnityEngine;

public class Monster_Rigidbody : MonoBehaviour
{
    Rigidbody2D GetRigidbody;


    private void Start()
    {
        GetRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(collision.TryGetComponent(out IHealth health))
            {
                health.TakeDamage(1, 500, GetRigidbody);
            }
        }
    }
}
