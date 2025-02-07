using UnityEngine;
public class Up : ICommand
{
    Transform transform;
    Player_Rigidbody rigidbody2D;
    Rigidbody2D rigidbody;
    Vector3 up = new Vector3(0, 0.1f);
    public Up(Player player)
    {
        rigidbody = player.GetRigidbody;
        transform = player.transform;
        rigidbody2D = player.GetPlayer_Rigidbody;
    }

    public Up(Transform _transform, Rigidbody2D rigidbody2D)
    {
        transform = _transform;
        rigidbody = rigidbody2D;
    }

    public void Execute()
    {
        if (!rigidbody2D.isLadder)
            return;
        rigidbody.linearVelocity = Vector2.zero;
        transform.position += up;
        rigidbody2D.isClimbing = true;
    }
}