using UnityEngine;
using player;
using System.Collections;
public class Arrow : MonoBehaviour, ICatchable
{
    float dist = 5f;
    float speed = 20f;
    float invincibleTime = 0.1f;
    bool trigger = false;
    bool fall = false;
    bool isDamaged = true;
    Rigidbody2D rb;
    Collider2D col;

    private IEnumerator EnableCollisionAfterDelay()
    {
        if (col.enabled)
            yield break;
        yield return new WaitForSeconds(invincibleTime);
        col.enabled = true;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }
    private void Update()
    {
        if (fall)
            return;
        if (trigger)
        {
            transform.position += (speed * Time.deltaTime * transform.up);
            return;
        }

        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.up, dist);
        Debug.DrawRay(transform.position, transform.up*dist, Color.green);
        if (hit2D.collider != null)
        {
            if (hit2D.collider.gameObject.TryGetComponent(out ICatchable _) || hit2D.collider.gameObject.TryGetComponent(out Player _))
            {             
                trigger = true;
                StartCoroutine(EnableCollisionAfterDelay());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GravityApply();
            rb.linearVelocity = Vector2.zero;
            col.excludeLayers = LayerMask.GetMask("Target");
        }
        if (collision.gameObject.TryGetComponent(out IHealth health))
        {
            GravityApply();
            health.TakeDamage(1, 500, rb);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            rb.linearVelocity = Vector2.zero;
            col.excludeLayers = LayerMask.GetMask("Target");
            isDamaged = false;
        }
        if (collision.gameObject.TryGetComponent(out IHealth health))
        {
            health.TakeDamage(1, 500, rb);
        }
    }

    void GravityApply()
    {
        if(!fall)
            fall = true;
        trigger = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    public void Grap(GameObject obj, Vector3 pos)
    {
        transform.SetParent(obj.transform);
        transform.localPosition = pos;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
    public void Throw(GameObject obj, Vector3 left, Vector3 right)
    {
        transform.SetParent(null);
        Vector3 dir = (obj.GetComponent<SpriteRenderer>().flipX ? left : right);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(dir * 50f, ForceMode2D.Impulse);
        col.excludeLayers = 0;
        isDamaged = true;
    }
}