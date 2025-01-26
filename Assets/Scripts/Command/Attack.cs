using UnityEngine;

public class Attack : ICommand
{
    Rigidbody2D rigidbody;
    Player_Rigidbody player_Rigidbody = null;
    public Attack(Rigidbody2D rigid)
    {
        rigidbody = rigid;
    }
    public Attack(Rigidbody2D rigid, Player_Rigidbody player)
    {
        rigidbody = rigid;
        player_Rigidbody = player;
    }

    public void Execute()
    {
        if (player_Rigidbody)
        {
            player_Rigidbody.isClimbing = false;
        }
    }
}