using UnityEngine;
using player;
public class Item : ICommand
{
    GameObject obj = null;
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
            obj = GetPlayer.GetPlayer_Item.GetObject;
        }

        if (obj is null)
            return;
        ICatchable Catchable;
        if (!obj.TryGetComponent(out Catchable))
            return;

        if (GetPlayer.GetPlayer_Item.GetCatch)
            Catchable.Throw(GetPlayer.gameObject, left, right);
        else
            Catchable.Grap(GetPlayer.gameObject, pos);
        GetPlayer.GetPlayer_Item.GetCatch = !GetPlayer.GetPlayer_Item.GetCatch;
    }
}