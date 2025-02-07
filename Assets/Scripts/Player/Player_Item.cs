using UnityEngine;

public class Player_Item : MonoBehaviour
{
    GameObject obj = null;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out IItem item))
        {
            obj = collision.gameObject;
            return;
        }
        obj = null;
    }

    public GameObject GetObject
    {
        get
        {
            return obj;
        }
    }
}