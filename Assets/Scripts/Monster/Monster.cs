using UnityEngine;

public class Monster
{
    int health;
    int groggi;
    bool catchable = true;
    public Monster(int h, int _groggi, bool c)
    {
        health = h;
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
