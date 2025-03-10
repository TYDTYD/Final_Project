using UnityEngine;
using player;
public class Item : ICommand
{
    GameObject obj = null;
    GameObject item = null;
    Vector3 right = new Vector3(1, 0.2f, 0);
    Vector3 left = new Vector3(-1, 0.2f, 0);
    Vector3 pos = new Vector3(0, -0.05f, 0);
    public Item()
    {

    }
    public Item(GameObject _obj)
    {
        obj = _obj;
    }
    public void Execute()
    {

    }
    public void Execute(Player player)
    {
        obj = player.GetPlayer_Item.GetObj;
        item = player.GetPlayer_Item.CurrentItem;
        if (player.GetPlayer_Item.CurrentItem != null)
        {
            item.GetComponent<ICatchable>().Throw(player.gameObject, left, right);
            player.GetPlayer_Item.GetCatch = false;
            player.GetPlayer_Item.CurrentItem = null;
        }
        else
        {
            player.GetPlayer_Item.CurrentItem = obj;
            player.GetPlayer_Item.GetCatch = true;
            if (obj)
                obj.GetComponent<ICatchable>().Grap(player.gameObject, pos);
        }
    }
}