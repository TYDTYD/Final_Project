using UnityEngine;
public class Arrow : MonoBehaviour, ICatchable
{
    float speed = 30f;
    float threshold = 5f;
    bool fall = false;
    Rigidbody2D rb;
    GameObject Owner;
    Vector3 Offset = Vector3.zero;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (fall)
            return;
        transform.position += (speed * Time.deltaTime * transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.linearVelocity.magnitude < threshold)
        {
            if (!fall)
            {
                if (collision.gameObject.TryGetComponent(out IHealth _health))
                    _health.TakeDamage(2, 500, rb);
            }
            return;
        }
        if (collision.gameObject.TryGetComponent(out IHealth health))
        {
            if (Owner != null && Owner == collision.gameObject)
                return;
            Debug.Log($"АјАн : " + collision.gameObject);
            health.TakeDamage(1, 100, rb);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (fall)
        {
            if (collision.CompareTag("Ground"))
            {
                gameObject.layer = 3;
                Owner = null;
            }
            return;
        }

        if (collision.CompareTag("Ground"))
        {
            GravityApply();
            gameObject.layer = 3;
            Owner = null;
        }
    }
    void GravityApply()
    {
        fall = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = Vector2.zero;
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