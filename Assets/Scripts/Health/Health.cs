using UnityEngine;

public interface IHealth
{
    void TakeDamage(int damage, int force, GameObject obj);
    void Heal(int amount);
}
