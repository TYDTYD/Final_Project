using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour, IHealth
{
    bool isAttackStarted = false;
    bool isCharging = false;
    float speed = 8f;
    float chargeDistance = 10f;
    float waitAfterCharge = 3f;
    int damage = 10;
    int force = 3;
    int hp = 20;
    bool isDeath = false;
    [SerializeField] Animator animator;

    GameObject player;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameManager.Instance.GetPlayer;
    }

    private void Update()
    {
        if (!isAttackStarted)
        {
            AttackPlayer();
        }    
    }

    void AttackPlayer()
    {
        isAttackStarted = true;
        // animation play

        float dir = player.transform.position.x > transform.position.x ? 1f : -1f;
        StartCoroutine(Charge(dir));
    }

    IEnumerator Charge(float dir)
    {
        if (isDeath)
            yield break;
        float moved = 0f;
        isCharging = true;

        transform.localScale = new Vector3(-dir * 3f, 3f);

        animator.SetBool("1_Move", true);

        while (chargeDistance > moved)
        {            
            float delta = speed * Time.deltaTime;
            transform.position += new Vector3(delta * dir, 0f);
            moved += delta;
            yield return null;
        }

        animator.SetBool("1_Move", false);
        isCharging = false;
        yield return new WaitForSeconds(waitAfterCharge);
        isAttackStarted = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDeath)
            return;
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.TryGetComponent(out IHealth health))
            {
                health.TakeDamage(damage, force, gameObject);
            }
            return;
        }

        if (!isCharging)
            return;

        if (collision.gameObject.CompareTag("Ground"))
        {
            if(!collision.gameObject.TryGetComponent(out IHealth health))
            {
                foreach(var contact in collision.contacts)
                {
                    // 수평방향으로 부딪혔을 경우
                    if(Mathf.Abs(contact.normal.x) > 0.5f)
                    {
                        collision.gameObject.SetActive(false);
                    }
                }
            }
        }
        else
        {
            if(!collision.gameObject.CompareTag("Item"))
                collision.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage, int force, GameObject obj)
    {
        hp -= damage;

        if(hp <= 0)
        {            
            isDeath = true;
            StopAllCoroutines();
            animator.SetBool("4_Death", true);
            Invoke("Death", 5f);
            return;
        }
        Debug.Log($"Boss Health : {hp}, Damage : {damage}, attacked by obj : {obj.name}");
        animator.SetBool("3_Damaged", true);
    }

    void Death()
    {
        gameObject.SetActive(false);
    }

    public void Heal(int amount)
    {
        
    }
}
