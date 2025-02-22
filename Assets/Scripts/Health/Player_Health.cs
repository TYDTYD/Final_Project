namespace player
{
    using UnityEngine;
    using UniRx;
    using System;
    public class Player_Health : MonoBehaviour, IHealth
    {
        public IReactiveProperty<int> health;
        public Action DeathEvent;
        private void Start()
        {
            health = Model.Instance.health;
            health.Subscribe(_health =>
            {
                if (_health <= 0)
                    DeathEvent();
            }).AddTo(this);
        }
        public void Heal(int amount) => health.Value += amount;
        public void TakeDamage(int damage) => health.Value -= damage;
    }
}