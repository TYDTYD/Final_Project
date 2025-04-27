using UnityEngine;
using player;
public class Move : ICommand
{
    Rigidbody2D Rigidbody2D;
    float speed = 6f, RunSpeed = 6f, SittingSpeed = 2f;
    float Direction;
    float wallContacted = 1f;
    const float Left = -1f, Right = 1f;
    public Move(Rigidbody2D rigidbody, float _speed, bool dir)
    {
        Rigidbody2D = rigidbody;
        speed = _speed;
        Direction = dir ? Left : Right;
    }
    public void Execute()
    {
        Rigidbody2D.linearVelocityX = Direction * speed;
    }
    public void Execute(Player player)
    {
        player.GetSprite.flipX = (Direction < 0) ? true : false;
        speed = (player.CurrentState == player.GetSittingMove) ? SittingSpeed : RunSpeed;
        speed *= wallContacted;
        Rigidbody2D.linearVelocityX = Direction * speed;
    }
    public float GetDirection => Direction;
    public float GetSpeed => speed;
    public float GetWallContact
    {
        get => wallContacted;
        set => wallContacted = value;
    }
}
