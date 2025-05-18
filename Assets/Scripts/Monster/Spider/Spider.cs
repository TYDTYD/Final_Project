using UnityEngine;
using System.Collections;

public class Spider : MonoBehaviour, IHealth
{
    int health = 1;
    bool reversed = true;
    readonly float jumpHorizontalForce = 4f;
    float detectDistance = 5f;
    float jumpDelay = 1.5f;
    float jumpForce = 15f;
    bool canJump = true;
    GameObject player;
    ICommand jump;
    Rigidbody2D GetRigidbody;
    private void Awake()
    {
        GetRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = GameManager.Instance.GetPlayer;
    }

    private void OnEnable() => UpdateManager.Instance.SubscribeUpdate(UpdateMethod);
    private void OnDisable() => UpdateManager.Instance.UnSubscribeUpdate(UpdateMethod);
    void UpdateMethod()
    {
        if (reversed)
            CheckPlayer();
        else
            FollowPlayer();
    }

    void CheckPlayer()
    {
        RaycastHit2D[] raycast = Physics2D.RaycastAll(transform.position, Vector2.down, detectDistance);

        foreach(var ray in raycast)
        {
            if (ray.collider.CompareTag("Player"))
            {
                reversed = false;
                transform.rotation = Quaternion.Euler(Vector2.zero);
                GetRigidbody.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    void FollowPlayer()
    {
        if (canJump)
        {
            if (player != null)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                float dir = (direction.x > 0) ? jumpHorizontalForce : -jumpHorizontalForce;
                jump = new Jump(GetRigidbody, jumpForce, new Vector2(dir, 0));
                StartCoroutine(ExecuteJump());
            }
        }
    }

    IEnumerator ExecuteJump()
    {
        canJump = false;
        jump.Execute();
        yield return new WaitForSeconds(jumpDelay);
        canJump = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.TryGetComponent(out IHealth health))
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

    public void TakeDamage(int damage, int force, GameObject obj)
    {
        health -= damage;
        gameObject.SetActive(false);
    }

    public void Heal(int amount)
    {
        return;
    }
}
