using UnityEngine;

public class Monster_Girl : MonoBehaviour, IHealth
{
    GameObject player;
    Monster_Anim GetMonster_Anim;
    RectTransform GetRectTransform;

    public int health = 1;
    float speed = 3f;
    float chaseDist = 4f;
    void Start()
    {
        player = GameManager.Instance.GetPlayer;
        GetMonster_Anim = GetComponent<Monster_Anim>();
        GetRectTransform = GetComponent<RectTransform>();
    }
    void FixedUpdate()
    {
        if (GetMonster_Anim.GetState == State.DEATH)
        {
            gameObject.SetActive(false);
            return;
        }
        if (isAround())
        {
            GetMonster_Anim.GetState = State.MOVE;
            ChasePlayer();
        }
        else
        {
            GetMonster_Anim.GetState = State.IDLE;
        }
    }

    bool isAround()
    {
        if (player == null)
            return false;
        float dist = Vector2.Distance(transform.position, player.transform.position);
        if (dist < chaseDist)
            return true;
        return false;
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Vector3 dir = new Vector3(direction.x, 0);
        transform.position += speed * Time.fixedDeltaTime * dir;
        if (direction.x > 0f) GetRectTransform.localScale = new Vector3(-1, 1, 1);
        else GetRectTransform.localScale = new Vector3(1, 1, 1);
    }

    public void TakeDamage(int damage, int force, GameObject obj)
    {
        health -= damage;
        GetMonster_Anim.GetState = State.DEATH;
    }

    public void Heal(int amount)
    {
        
    }
}
