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
    public void Execute(Player player)
    {
        Player_Rigidbody playerRb = player.GetPlayer_Rigidbody;
        if (player.CurrentState == Player.State.EdgeDetact_State)
        {
            playerRb.canClimb = false;
            player.StartCoroutine(SetGrap(player));
            return;
        }
        if (playerRb.GetLadder)
        {
            rigidbody.linearVelocity = Vector2.zero;
            transform.position -= down;
            playerRb.GetClimbing = true;
            return;
        }
    }
    IEnumerator SetGrap(Player player)
    {
        yield return delay;
        if (player != null)
            player.GetPlayer_Rigidbody.canGrabLedge = true;
    }
}