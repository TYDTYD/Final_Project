using UnityEngine;
using player;
public class Down : ICommand
{
    Transform transform;
    Rigidbody2D rigidbody;
    WaitForSeconds delay = new(0.1f);
    Vector3 down = new(0, 0.1f);
    public Down(Transform _transform, Rigidbody2D rigidbody2D)
    {
        transform = _transform;
        rigidbody = rigidbody2D;
    }
    public void Execute()
    {

    }

    public void Execute(Player player)
    {
        if (player.CurrentState.StateType == PlayerStateType.Sitting)
        {
            player.CurrentState = player.GetSitting;
            return;
        }
        if(player.CurrentState.StateType == PlayerStateType.Ladder)
        {
            player.CurrentState = player.GetLadder;
            rigidbody.linearVelocity = Vector2.zero;
            transform.position -= down;
            player.GetPlayer_Rigidbody.GetClimbing = true;
            return;
        }
        if(player.CurrentState.StateType == PlayerStateType.Edge)
        {
            player.CurrentState = player.GetFall;
            return;
        }
    }
}