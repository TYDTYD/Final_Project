using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Player_Rigidbody : MonoBehaviour
{
    Rigidbody2D GetRigidbody2D;
    Player GetPlayer;
    [SerializeField] Transform GetTransform;

    CompositeDisposable disposables = new CompositeDisposable();
    public bool Grounded = false;
    public bool isGrounded = false;
    public bool isLadder = false;
    public bool isClimbing = false;
    float gravity = 6f;
    void Start()
    {
        GetPlayer = GetComponent<Player>();
        GetRigidbody2D = GetComponent<Rigidbody2D>();

        this.UpdateAsObservable()
            .Select(_ => isGrounded)
            .DistinctUntilChanged()
            .ThrottleFrame(5)
            .Subscribe(x => Grounded = x);

        this.ObserveEveryValueChanged(_ => isClimbing)
            .Subscribe(climbing =>
            {
                if (climbing)
                {
                    GetRigidbody2D.gravityScale = 0f;
                    GetRigidbody2D.linearVelocityY = 0f;
                }
                else
                    GetRigidbody2D.gravityScale = gravity;
            })
            .AddTo(disposables);
        GetPlayer.GetPlayer_Health.DeathEvent += ClearUniRx;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            return;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            return;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            if (isClimbing)
            {
                transform.position = new Vector3(
                    collision.transform.position.x - GetTransform.localPosition.x, transform.position.y);
            }
            return;
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

    void ClearUniRx()
    {
        disposables.Clear();
    }
}