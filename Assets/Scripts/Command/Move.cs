using UnityEngine;
public class Move : ICommand
{
    Player_Rigidbody player_Rigidbody = null;
    Rigidbody2D Rigidbody2D;
    float speed = 5f;
    float Direction;
    Player GetPlayer;

    public Move(Rigidbody2D rigidbody, float _speed, bool dir)
    {
        Rigidbody2D = rigidbody;
        speed = _speed;
        Direction = dir ? -1f : 1f;
    }

    public Move(Rigidbody2D rigidbody, Player_Rigidbody _Rigidbody, Player player,float _speed, bool dir)
    {
        Rigidbody2D = rigidbody;
        player_Rigidbody = _Rigidbody;
        GetPlayer = player;
        speed = _speed;
        Direction = dir ? -1f : 1f;
    }

    public void Execute()
    {
        if (player_Rigidbody)
        {
            if (player_Rigidbody.isClimbing)
                return;
        }
        Rigidbody2D.linearVelocityX = Direction * speed;
    }
}
