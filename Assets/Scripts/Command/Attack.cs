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
            if (GetPlayer.CurrentState == Player.State.Damage_State ||
                GetPlayer.CurrentState == Player.State.Jump_State ||
                GetPlayer.CurrentState == Player.State.Land_State ||
                GetPlayer.CurrentState == Player.State.EdgeDetact_State)
                return;
            player_Rigidbody.isClimbing = false;
        }
    }
}