using UnityEngine;
using UniRx;
using System;
public class Player_Health : MonoBehaviour, IHealth 
{
    public IReactiveProperty<int> health = new ReactiveProperty<int>(4);
    public Action DeathEvent;

    private void Start()
    {
        health.Subscribe(_health =>
        {
            if (_health <= 0)
            {
                Debug.Log("Á×À½");
                DeathEvent();
            }
        }).AddTo(this);
    }
    public void Heal(int amount)
    {
        health.Value += amount;
    }

    public void TakeDamage(int damage)
    {
        health.Value -= damage;
    }
}
