using UnityEngine;
public class Move : ICommand
{
    Player GetPlayer = null;
    Player_Rigidbody player_Rigidbody;
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

    public Move(Player player, float _speed, bool dir)
    {
        GetPlayer = player;
        Rigidbody2D = player.GetRigidbody;
        player_Rigidbody = player.GetPlayer_Rigidbody;
        speed = _speed;
        Direction = dir ? Left : Right;
    }

    public void Execute()
    {
        if (GetPlayer)
        {
            if (player_Rigidbody.isClimbing ||
                GetPlayer.CurrentState == Player.State.Land_State ||
                GetPlayer.CurrentState == Player.State.Attack_State ||
                GetPlayer.CurrentState == Player.State.Damage_State ||
                GetPlayer.CurrentState == Player.State.Death_State)
                return;

            GetPlayer.GetSprite.flipX = (Direction < 0) ? true : false;
            speed = (GetPlayer.CurrentState == Player.State.SittingMove_State) ? SittingSpeed : RunSpeed;
        }
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
