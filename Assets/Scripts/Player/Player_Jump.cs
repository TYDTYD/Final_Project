using UnityEngine;
using UniRx;
using UniRx.Triggers;
public class Player_Jump : MonoBehaviour
{
    Rigidbody2D GetRigidbody2D;
    Player_Rigidbody Player_Rigidbody;
    Jump GetJump;
    KeyCode JumpKey;
    bool isJumping = false;
    float maxholdTime = 0.1f, currentholdTime = 0f;

    void Start()
    {
        GetRigidbody2D = GetComponent<Rigidbody2D>();
        Player_Rigidbody = GetComponent<Player_Rigidbody>();
        JumpKey = InputHandler.JumpKey;
        GetJump = new Jump(GetRigidbody2D, 3f);
        
        this.UpdateAsObservable()
            .Where(_ => (Player_Rigidbody.isGrounded || Player_Rigidbody.isClimbing) && Input.GetKey(JumpKey))
            .Subscribe(_ => StartJump())
            .AddTo(this);

        this.FixedUpdateAsObservable()
            .Where(_ => isJumping && Input.GetKey(JumpKey))
            .Subscribe(_ => ApplyJump())
            .AddTo(this);
    }

    void StartJump()
    {
        isJumping = true;
        Player_Rigidbody.isGrounded = false;
        Player_Rigidbody.isClimbing = false;
        currentholdTime = 0f;
    }

    void ApplyJump()
    {
        if (isJumping && currentholdTime < maxholdTime)
        {
            GetJump.Execute();
            currentholdTime += Time.fixedDeltaTime;
        }

        if (currentholdTime > maxholdTime)
        {
            isJumping = false;
            currentholdTime = 0f;
        }
    }
}
