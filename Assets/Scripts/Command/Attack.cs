using UnityEngine;

public class Attack : ICommand
{
    Player GetPlayer = null;
    Rigidbody2D rigidbody;
    Player_Rigidbody player_Rigidbody;
    public Attack(Rigidbody2D rigid)
    {
        rigidbody = rigid;
    }
    public Attack(Player player)
    {
        GetPlayer = player;
        rigidbody = player.GetRigidbody;
        player_Rigidbody = player.GetPlayer_Rigidbody;
    }

    public void Execute()
    {
       
        if (GetPlayer)
        {
            if (GetPlayer.GetState == Player.State.Damage_State)
                return;
            if (GetPlayer.GetState == Player.State.Jump_State)
                return;
            if (GetPlayer.GetState == Player.State.Land_State)
                return;
            if (GetPlayer.GetState == Player.State.EdgeDetact_State)
                return;
            player_Rigidbody.isClimbing = false;
        }
    }
}