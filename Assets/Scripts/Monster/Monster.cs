using UnityEngine;

public class Monster : MonoBehaviour
{
    int groggi;
    bool catchable = true;
    Monster(int _groggi, bool c)
    {
        groggi = _groggi;
        catchable = c;
    }
    protected virtual void Attack()
    {

    }

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
