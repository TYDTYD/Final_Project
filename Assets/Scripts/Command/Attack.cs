using UnityEngine;
using player;
public class Attack : ICommand
{
    GameObject obj = null;
    Rigidbody2D rigidbody;
    public Attack(Rigidbody2D rigid)
    {
        rigidbody = rigid;
    }
    public void Execute()
    {

    }
    public void Execute(Player player)
    {
        if (player.CurrentState == Player.State.Damage_State ||
            player.CurrentState == Player.State.Land_State ||
            player.CurrentState == Player.State.EdgeDetact_State)
            return;

        obj = player.GetPlayer_Item.CurrentItem;
        player.GetPlayer_Rigidbody.GetClimbing = false;

        if (obj!=null && obj.TryGetComponent(out IItem item))
        {
            item.Use();
            return;
        }
        if (player.CurrentState != Player.State.Jump_State)
            player.CurrentState = Player.State.Attack_State;
    }
}