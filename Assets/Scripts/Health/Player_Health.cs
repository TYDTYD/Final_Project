namespace player
{
    using UnityEngine;
    using System;
    public class Player_Health : MonoBehaviour, IHealth
    {
        public event Action DeathEvent;
        Player GetPlayer;
        Rigidbody2D GetRigidbody2D;

        int hp;
        private void Start()
        {
            GetPlayer = GetComponent<Player>();
            GetRigidbody2D = GetPlayer.GetRigidbody;
        }
        private void OnEnable() => UpdateManager.Instance.SubscribeUpdate(UpdateMethod);
        private void OnDisable() => UpdateManager.Instance.UnSubscribeUpdate(UpdateMethod);
        private void OnDestroy()
        {
            if(DeathEvent == null)
                return;
            foreach(Delegate d in DeathEvent.GetInvocationList())
                DeathEvent -= (Action)d;
        }
        void UpdateMethod()
        {
            if (hp != Stage_UI_View.Instance.GetHp)
            {
                if (Stage_UI_View.Instance.GetHp <= 0)
                    DeathEvent?.Invoke();
                hp = Stage_UI_View.Instance.GetHp;
            }
        }
        public void Heal(int amount) => Stage_UI_View.Instance.IncreaseHealth(amount);
        public void TakeDamage(int damage, int force, GameObject obj)
        {
            Stage_UI_View.Instance.DecreaseHealth(damage);
            Vector3 dir = (transform.position - obj.transform.position).normalized;
            Vector3 knockbackDir = new Vector3((dir.x > 0 ? 1f : -1f), 1f, 0f);
            GetRigidbody2D.AddForce(knockbackDir * force);
        }
    }
}