using UnityEngine;

public class Stone : MonoBehaviour, IItem 
{
    bool isDamaged = false;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            rb.linearVelocity = Vector2.zero;
            isDamaged = false;
        }
        else if (isDamaged)
        {
            if (collision.gameObject.TryGetComponent(out IHealth health))
            {
                Debug.Log("µ¥¹ÌÁö");
                health.TakeDamage(1, 500, rb);
            }
        }
    }

    public void Use()
    {
        
    }

    public bool GetCatchable() => false;
}