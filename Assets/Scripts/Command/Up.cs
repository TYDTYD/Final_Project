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
        if (player.GetPlayer_Rigidbody.GetLadder)
        {
            rigidbody.linearVelocity = Vector2.zero;
            transform.position += up;
            player.GetPlayer_Rigidbody.GetClimbing = true;
            return;
        }
    }
}