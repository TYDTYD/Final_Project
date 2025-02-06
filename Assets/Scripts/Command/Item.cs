using UnityEngine;

public class Item : ICommand
{
    //bool isActive = false;
    bool isCatch = false;
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
        if (isCatch)
        {
            if(obj.TryGetComponent(out Monster monster))
            {
                if (!monster.GetCatchable)
                    return;
                if (monster.GetGroggi <= 0)
                    return;
            }
            if(obj.TryGetComponent(out IItem item))
            {
                item.Use();
            }
            obj.transform.SetParent(null);
            Vector3 dir = (GetPlayer.GetSprite.flipX ? left : right);
            obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            obj.GetComponent<Rigidbody2D>().AddForce(dir * 50f,ForceMode2D.Impulse);
        }
        else
        {
            obj.transform.SetParent(GetPlayer.transform);
            obj.transform.localPosition = pos;
            obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
        isCatch = !isCatch;
    }
}