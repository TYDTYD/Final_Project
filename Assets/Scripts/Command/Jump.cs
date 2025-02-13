using UnityEngine;

public class Jump : ICommand
{
    Rigidbody2D Rigidbody2D;
    float JumpForce = 6f;
    Vector2 Direction;
    public Jump(Rigidbody2D rigidbody, float force)
    {
        Rigidbody2D = rigidbody;
        JumpForce = force;
        Direction = new Vector2(0, JumpForce);
    }
    public void Execute()
    {
        Rigidbody2D.AddForce(Direction, ForceMode2D.Impulse);
    }
}