using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Player_Rigidbody : MonoBehaviour
{
    Rigidbody2D GetRigidbody2D;
    [SerializeField] Transform GetTransform;
    Player GetPlayer;

    public bool isGrounded = false;
    public bool isLadder = false;
    public bool isClimbing = false;
    float gravity = 5f;
    void Start()
    {
        GetRigidbody2D = GetComponent<Rigidbody2D>();
        GetPlayer = GetComponent<Player>();
        this.ObserveEveryValueChanged(_ => isClimbing)
            .Subscribe(climbing => {
                if (climbing)
                {
                    GetRigidbody2D.gravityScale = 0f;
                    GetRigidbody2D.linearVelocityY = 0f;
                }
                else
                    GetRigidbody2D.gravityScale = gravity;
            })
            .AddTo(this);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") && collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
        }
        else
            isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            GetPlayer.CurrentState = Player.State.Ladder_State;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            if (isClimbing)
            {
                
                transform.position = new Vector3(
                    collision.transform.position.x - GetTransform.localPosition.x * 2f, transform.position.y);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}