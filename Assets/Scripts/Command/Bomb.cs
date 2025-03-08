using UnityEngine;

public class Bomb : ICommand
{
    GameObject bomb;
    public Bomb(GameObject obj)
    {
        bomb = obj;
    }
    public void Execute()
    {
        GameObject obj = Object.Instantiate(bomb);
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1));
    }
}
