using UnityEngine;
using player;
public class Jump : ICommand
{
    Rigidbody2D Rigidbody2D;
    float JumpForce = 1f;
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

    public void Execute(Player player)
    {
        if (player.GetPlayer_Rigidbody.GetGrounded || player.GetPlayer_Rigidbody.GetClimbing)
            StartJump(player);
    }
    void StartJump(Player player)
    {
        Rigidbody2D.AddForce(Direction, ForceMode2D.Impulse);
        player.GetPlayer_Rigidbody.GetGrounded = false;
        player.GetPlayer_Rigidbody.GetClimbing = false;
    }
}