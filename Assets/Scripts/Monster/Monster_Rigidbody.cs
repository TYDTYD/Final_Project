using UnityEngine;

public class Monster_Rigidbody : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out IHealth health))
            {
                foreach (ContactPoint2D contact in collision.contacts)
                {
                    Vector2 normal = contact.normal; // 충돌한 표면의 방향 (노멀 벡터)
                    if (normal.y < -0.5f)
                    {
                        if (collision.gameObject.TryGetComponent(out Rigidbody2D playerRb))
                            playerRb.AddForce(new Vector2(0, 20), ForceMode2D.Impulse);
                        gameObject.SetActive(false);
                        return;
                    }
                }
                health.TakeDamage(1, 300, gameObject);
            }
        }
    }
}
