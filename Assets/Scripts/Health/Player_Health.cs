namespace player
{
    using UnityEngine;
    using UniRx;
    using System;
    public class Player_Health : MonoBehaviour, IHealth
    {
        public IReactiveProperty<int> health;
        public Action DeathEvent;
        Player GetPlayer;
        Rigidbody2D GetRigidbody2D;
        private void Start()
        {
            GetPlayer = GetComponent<Player>();
            GetRigidbody2D = GetPlayer.GetRigidbody;
            health = Stage_UI_View.Instance.View_Model.Health;
            health.Subscribe(_health =>
            {
                if (_health <= 0)
                    DeathEvent();
            }).AddTo(this);
        }
        public void Heal(int amount) => health.Value += amount;
        public void TakeDamage(int damage, int force, GameObject obj)
        {
            Stage_UI_View.Instance.DecreaseHealth(damage);
            Vector3 knockbackDir = (obj.transform.position - transform.position).normalized;
            GetRigidbody2D.AddForce(knockbackDir * force);
        }
    }
}