using UnityEngine;
public class Move : ICommand
{
    Player GetPlayer = null;
    Player_Rigidbody player_Rigidbody;
    Rigidbody2D Rigidbody2D;
    float speed = 6f, RunSpeed = 6f, SittingSpeed = 2f;
    float Direction;

    public Move(Rigidbody2D rigidbody, float _speed, bool dir)
    {
        Rigidbody2D = rigidbody;
        speed = _speed;
        Direction = dir ? -1f : 1f;
    }

    public Move(Player player,float _speed, bool dir)
    {
        GetPlayer = player;
        Rigidbody2D = player.GetRigidbody;
        player_Rigidbody = player.GetPlayer_Rigidbody;
        speed = _speed;
        Direction = dir ? -1f : 1f;
    }

    public void Execute()
    {
        if (GetPlayer)
        {
            if (player_Rigidbody.isClimbing)
                return;
            if (GetPlayer.GetState == Player.State.Land_State)
                return;
            if (GetPlayer.GetState == Player.State.Attack_State)
                return;

            GetPlayer.GetSprite.flipX = (Direction < 0) ? true : false;
            if (GetPlayer.GetState == Player.State.SittingMove_State)
                speed = SittingSpeed;
            else
                speed = RunSpeed;
        }
        Rigidbody2D.linearVelocityX = Direction * speed;
    }
}
