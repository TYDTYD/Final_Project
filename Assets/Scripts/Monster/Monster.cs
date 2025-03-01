using UnityEngine;

public class Monster : MonoBehaviour
{
    int health;
    int groggi;
    bool catchable = true;
    public int GetGroggi
    {
        get
        {
            return groggi;
        }
    }

    public bool GetCatchable
    {
        get
        {
            return catchable;
        }
    }
}
