using UnityEngine;

public class Idle : ICommand
{
    Rigidbody2D Rigidbody2D;
    public Idle(Rigidbody2D rigidbody)
    {
        Rigidbody2D = rigidbody;
    }
    public void Execute()
    {
        if (Rigidbody2D.linearVelocityX > 0f)
            Rigidbody2D.linearVelocityX -= 0.1f;
        else if (Rigidbody2D.linearVelocityX < 0f)
            Rigidbody2D.linearVelocityX += 0.1f;
    }
}
