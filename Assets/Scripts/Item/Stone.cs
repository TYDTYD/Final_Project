using UnityEngine;

public class Stone : MonoBehaviour, ICatchable
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
            Debug.Log("¶¥");
            rb.linearVelocity = Vector2.zero;
            //col.excludeLayers = LayerMask.GetMask("Target");
            isDamaged = false;
        }
        else if (isDamaged)
        {
            if (collision.gameObject.TryGetComponent(out IHealth health))
            {
                Debug.Log("°ø°Ý");
                Debug.Log(gameObject);
                health.TakeDamage(1, 500, rb);
            }
        }
    }
    public void Grap(GameObject obj, Vector3 pos)
    {
        isDamaged = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.SetParent(obj.transform);
        transform.localPosition = pos;
    }
    public void Throw(GameObject obj, Vector3 left, Vector3 right)
    {
        transform.SetParent(null);
        Vector3 dir = (obj.GetComponent<SpriteRenderer>().flipX ? left : right);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(dir * 20f, ForceMode2D.Impulse);
        isDamaged = true;
    }
}