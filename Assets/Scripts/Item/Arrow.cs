using UnityEngine;
using player;
public class Arrow : MonoBehaviour, IItem
{
    float dist = 5f;
    float force = 10f;
    bool trigger = false;
    Rigidbody2D rb;

    public void Use()
    {
        return;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (trigger)
            return;
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.right, dist, 3);
        Debug.DrawRay(transform.position, Vector2.right*dist, Color.green);
        Debug.Log(hit2D.collider.gameObject);
        if(hit2D.collider.gameObject.TryGetComponent(out IItem item) || hit2D.collider.gameObject.TryGetComponent(out Player player))
        {
            rb.AddForce(Vector2.right * force);
            trigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IHealth health))
        {
            Debug.DrawRay(transform.position, Vector2.right * dist, Color.red);
            rb.bodyType = RigidbodyType2D.Dynamic;
            health.TakeDamage(1, 100, rb);
        }
    }
}
