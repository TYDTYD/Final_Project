using UnityEngine;
using player;
public class Item : ICommand
{
    GameObject obj = null;
    GameObject item = null;
    Player GetPlayer = null;
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

    public Item(Player player)
    {
        GetPlayer = player;
    }
    public void Execute()
    {
        if (GetPlayer != null)
        {
            obj = GetPlayer.GetPlayer_Item.GetObj;
            item = GetPlayer.GetPlayer_Item.CurrentItem;
        }

        if (GetPlayer.GetPlayer_Item.CurrentItem != null)
        {
            item.GetComponent<ICatchable>().Throw(GetPlayer.gameObject, left, right);
            GetPlayer.GetPlayer_Item.GetCatch = false;
            GetPlayer.GetPlayer_Item.CurrentItem = null;
        }
        else
        {
            GetPlayer.GetPlayer_Item.CurrentItem = obj;
            GetPlayer.GetPlayer_Item.GetCatch = true;
            if(obj)
                obj.GetComponent<ICatchable>().Grap(GetPlayer.gameObject, pos);
        }
    }
}