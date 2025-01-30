using UnityEngine;

public class Attack : ICommand
{
    Rigidbody2D rigidbody;
    Player_Rigidbody player_Rigidbody = null;
    public Attack(Rigidbody2D rigid)
    {
        rigidbody = rigid;
    }
    public Attack(Player player)
    {
        rigidbody = player.GetRigidbody;
        player_Rigidbody = player.GetPlayer_Rigidbody;
    }

    public void Execute()
    {
        if (player_Rigidbody)
        {
            player_Rigidbody.isClimbing = false;
        }
    }
}