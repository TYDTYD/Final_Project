using UnityEngine;

public class Player_Rigidbody : MonoBehaviour
{
    Rigidbody2D GetRigidbody2D;
    public bool isGrounded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") && collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
        }
    }
}
