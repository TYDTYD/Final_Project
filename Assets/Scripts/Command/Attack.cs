using UnityEngine;
using System.Collections;
using player;
public class Attack : ICommand
{
    GameObject obj = null;
    float attackTime = 0.2f;
    WaitForSeconds cache;
    Rigidbody2D rigidbody;
    public Attack(Rigidbody2D rigid)
    {
        rigidbody = rigid;
        cache = new WaitForSeconds(attackTime);
    }
    public void Execute()
    {

    }
    public void Execute(Player player)
    {
        obj = player.GetPlayer_Item.CurrentItem;
        player.GetPlayer_Rigidbody.GetClimbing = false;

        if (obj!=null && obj.TryGetComponent(out IItem item))
            item.Use();
        else
        {
            player.GetPlayer_Attack.GetBox.enabled = true;
            player.StartCoroutine(AttackTiming(player));
        }
    }

    IEnumerator AttackTiming(Player player)
    {
        yield return cache;
        player.GetPlayer_Attack.GetBox.enabled = false;
    }
}