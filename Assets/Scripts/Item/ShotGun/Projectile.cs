using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    int damage = 4;
    Rigidbody2D rb;
    bool isReleased = false;
    IObjectPool<Projectile> objectPool;
    public IObjectPool<Projectile> ObjectPool { set => objectPool = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        isReleased = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            return;

        if (collision.gameObject.TryGetComponent(out IHealth health))
        {
            health.TakeDamage(damage, 100, gameObject);
            
        }

        if (!isReleased)
        {
            isReleased = true;
            Deactivate();
        }
    }

    public void Deactivate()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        objectPool?.Release(this);
    }
}