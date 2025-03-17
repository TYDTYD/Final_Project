using UnityEngine;
using player;
public class Monster_Girl : MonoBehaviour, IHealth
{
    GameObject player;
    Monster_Anim GetMonster_Anim;
    RectTransform GetRectTransform;
    Rigidbody2D GetRigidbody2D;
    ICommand RightMoveCommand;
    ICommand LeftMoveCommand;
    Stage_UI_View UI_View;

    int health = 1;
    float speed = 3f;
    float chaseDist = 4f;
    void Start()
    {
        player = GameManager.Instance.GetPlayer;
        GetMonster_Anim = GetComponent<Monster_Anim>();
        GetRectTransform = GetComponent<RectTransform>();
        GetRigidbody2D = GetComponent<Rigidbody2D>();
        RightMoveCommand = new Move(GetRigidbody2D, speed, false);
        LeftMoveCommand = new Move(GetRigidbody2D, speed, true);
        UI_View = Stage_UI_View.Instance;
    }
    private void OnEnable() => UpdateManager.Instance.SubscribeFixedUpdate(FixedUpdateMethod);
    private void OnDisable() => UpdateManager.Instance.UnSubscribeFixedUpdate(FixedUpdateMethod);
    void FixedUpdateMethod()
    {
        if (GetMonster_Anim.GetState == State.DEATH)
        {
            gameObject.SetActive(false);
            return;
        }
        if (IsPlayerNearby())
        {
            GetMonster_Anim.GetState = State.MOVE;
            ChasePlayer();
        }
        else
        {
            GetMonster_Anim.GetState = State.IDLE;
        }
    }
    bool IsPlayerNearby() => player != null && Vector2.Distance(transform.position, player.transform.position) < chaseDist;
    void ChasePlayer()
    {
        bool isRight = player.transform.position.x > transform.position.x;
        if (isRight)
            RightMoveCommand.Execute();
        else
            LeftMoveCommand.Execute();
        GetRectTransform.localScale = new Vector3(isRight ? -1 : 1, 1, 1);
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