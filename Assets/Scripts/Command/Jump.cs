using UnityEngine;
using player;
public class Jump : ICommand
{
    Rigidbody2D Rigidbody2D;
    float JumpForce = 1f;
    Player GetPlayer;
    Vector2 Direction;
    public Jump(Rigidbody2D rigidbody, float force)
    {
        Rigidbody2D = rigidbody;
        JumpForce = force;
        Direction = new Vector2(0, JumpForce);
    }
    public Jump(Player player, Rigidbody2D rigidbody, float force)
    {
        GetPlayer = player;
        Rigidbody2D = rigidbody;
        JumpForce = force;
        Direction = new Vector2(0, JumpForce);
    }
    public void Execute()
    {
        if (GetPlayer)
        {
            if(GetPlayer.GetPlayer_Rigidbody.GetGrounded || GetPlayer.GetPlayer_Rigidbody.GetClimbing)
                StartJump();
        }
        else
        {
            Rigidbody2D.AddForce(Direction, ForceMode2D.Impulse);
        }
    }

    void StartJump()
    {
        Rigidbody2D.AddForce(Direction, ForceMode2D.Impulse);
        GetPlayer.GetPlayer_Rigidbody.GetGrounded = false;
        GetPlayer.GetPlayer_Rigidbody.GetClimbing = false;
    }
}