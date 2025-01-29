using UnityEngine;
using UniRx;

public class Player_Health : MonoBehaviour, IHealth 
{
    public IReactiveProperty<int> health = new ReactiveProperty<int>(5);

    public void Heal(int amount)
    {
        health.Value += amount;
    }

    public void TakeDamage(int damage)
    {
        health.Value -= damage;
    }
}
