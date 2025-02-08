using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;
public class Player_Health : MonoBehaviour, IHealth 
{
    public IReactiveProperty<int> health = new ReactiveProperty<int>(5);
    public Action DeathEvent;

    private void Start()
    {
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
