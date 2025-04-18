using UnityEngine;
using player;
using System.Collections;

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
    bool CanClimb(Player player)
    {
        return player.CurrentState == Player.State.Fall_State
            || player.CurrentState == Player.State.Jump_State
            || player.CurrentState == Player.State.Idle_State
            || player.CurrentState == Player.State.LadderStop_State
            || player.CurrentState == Player.State.Ladder_State;
    }
    public void Execute(Player player)
    {
        if (!CanClimb(player))
            return;
        Player_Rigidbody playerRb = player.GetPlayer_Rigidbody;
        
        if (playerRb.GetLadder)
        {
            rigidbody.linearVelocity = Vector2.zero;
            transform.position -= down;
            playerRb.GetClimbing = true;
            return;
        }
    }
}