using UnityEngine;

public interface IHealth
{
    void TakeDamage(int damage, int force, Rigidbody2D rb);
    void Heal(int amount);
}
