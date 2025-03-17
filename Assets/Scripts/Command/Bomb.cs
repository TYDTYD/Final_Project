using UnityEngine;
using player;
public class Bomb : ICommand
{
    GameObject bomb;
    Vector3 Offset = new Vector3(0, 0.4f);
    Vector3 right = new Vector3(1, 0.2f, 0);
    Vector3 left = new Vector3(-1, 0.2f, 0);
    Player GetPlayer;
    public Bomb(GameObject obj)
    {
        bomb = obj;
    }
    public void Execute()
    {

    }
    public void Execute(Player player)
    {
        if (Stage_UI_View.Instance.GetBomb > 0)
        {
            Stage_UI_View.Instance.DecreaseBomb(1);
            GameObject obj = player.gameObject;
            Vector3 dir = (obj.GetComponent<SpriteRenderer>().flipX ? left : right);
            Offset.x = dir.x * 0.5f;
            GameObject item = Object.Instantiate(bomb, obj.transform.position + Offset, Quaternion.identity);
            item.GetComponent<Rigidbody2D>().AddForce(dir * 15f, ForceMode2D.Impulse);
        }
    } 
}
