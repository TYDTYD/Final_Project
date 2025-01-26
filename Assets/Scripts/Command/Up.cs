using UnityEngine;
public class Up : ICommand
{
    Transform transform;
    Player_Rigidbody rigidbody2D;
    Vector3 up = new Vector3(0, 0.1f);
    public Up(Transform _transform, Player_Rigidbody rigid)
    {
        transform = _transform;
        rigidbody2D = rigid;
    }

    public Up(Transform _transform)
    {
        transform = _transform;
    }

    public void Execute()
    {
        if (!rigidbody2D.isLadder)
            return;
        transform.position += up;
        rigidbody2D.isClimbing = true;
    }
}