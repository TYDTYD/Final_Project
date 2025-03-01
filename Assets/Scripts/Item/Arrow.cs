using UnityEngine;
using player;
using System.Collections;
public class Arrow : MonoBehaviour, IItem
{
    float dist = 5f;
    float speed = 20f;
    float invincibleTime = 0.1f;
    bool trigger = false;
    bool fall = false;
    bool isDamaged = true;
    Rigidbody2D rb;
    Collider2D col;

    public void Use()
    {
        return;
    }
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

        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.up, dist, LayerMask.GetMask("Target"));
        Debug.DrawRay(transform.position, transform.up*dist, Color.green);
        if (hit2D.collider != null)
        {
            if (hit2D.collider.gameObject.TryGetComponent(out IItem _) || hit2D.collider.gameObject.TryGetComponent(out Player _))
            {             
                trigger = true;
                StartCoroutine(EnableCollisionAfterDelay());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDamaged)
            return;
        if (collision.gameObject.CompareTag("Ground"))
        {
            trigger = false;
            if (!fall)
                GravityApply();
        }
        if (collision.gameObject.TryGetComponent(out IHealth health))
        {
            trigger = false;
            Debug.Log("플레이어");
            if (!fall)
                GravityApply();
            health.TakeDamage(1, 500, rb);
        }
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
                health.TakeDamage(1, 500, rb);
            }
        }
    }

    void GravityApply()
    {
        fall = true;
        isDamaged = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public bool GetCatchable() => false;
}