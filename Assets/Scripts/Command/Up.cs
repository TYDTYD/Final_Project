using UnityEngine;
using player;
public class Up : ICommand
{
    Transform transform;
    Rigidbody2D rigidbody;
    Vector3 up = new Vector3(0, 0.1f);
    public Up(Transform _transform, Rigidbody2D rigidbody2D)
    {
        transform = _transform;
        rigidbody = rigidbody2D;
    }
    public void Execute()
    {
        rigidbody.linearVelocity = Vector2.zero;
        transform.position += up;
    }
    public void Execute(Player player)
    {
        if (!player.GetPlayer_Rigidbody.GetLadder)
            return;
        rigidbody.linearVelocity = Vector2.zero;
        transform.position += up;
        player.GetPlayer_Rigidbody.GetClimbing = true;
    }
}