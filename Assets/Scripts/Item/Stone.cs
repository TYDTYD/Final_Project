using UnityEngine;

public class Stone : MonoBehaviour, ICatchable
{
    float threshold = 5f;
    Rigidbody2D rb;
    GameObject Owner;
    Vector3 Offset = Vector3.zero;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            gameObject.layer = 3;
            Owner = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.linearVelocity.magnitude < threshold)
        {
            return;
        }
        if (collision.gameObject.TryGetComponent(out IHealth health))
        {
            Debug.Log(rb.linearVelocity.magnitude);
            if (Owner != null && Owner == collision.gameObject)
                return;

            Debug.Log($"АјАн : " + collision.gameObject);
            health.TakeDamage(1, 100, gameObject);
        }
    }
    public void Grap(GameObject obj, Vector3 pos)
    {
        Owner = obj;
        gameObject.layer = 3;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.SetParent(obj.transform);
        transform.localPosition = pos;
    }
    public void Throw(GameObject obj, Vector3 left, Vector3 right)
    {
        transform.SetParent(null);
        Vector3 dir = (obj.GetComponent<SpriteRenderer>().flipX ? left : right);
        Offset.x = dir.x;
        transform.position = obj.transform.position + Offset;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(dir * 20f, ForceMode2D.Impulse);
        gameObject.layer = 0;
    }
}