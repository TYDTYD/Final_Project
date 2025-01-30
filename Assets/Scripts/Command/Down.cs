using UnityEngine;
public class Down : ICommand
{
    Transform transform;
    Player_Rigidbody rigidbody2D;
    Vector3 down = new Vector3(0, 0.1f);
    public Down(Player player)
    {
        transform = player.transform;
        rigidbody2D = player.GetPlayer_Rigidbody;
    }

    public Down(Transform _transform)
    {
        transform = _transform;
    }

    public void Execute()
    {
        if (!rigidbody2D.isLadder)
            return;
        transform.position -= down;
        rigidbody2D.isClimbing = true;
    }
}