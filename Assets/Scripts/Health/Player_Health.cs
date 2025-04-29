namespace player
{
    using UnityEngine;
    using UniRx;
    using System;
    public class Player_Health : MonoBehaviour, IHealth
    {
        public event Action DeathEvent;
        Player GetPlayer;
        Rigidbody2D GetRigidbody2D;

        ReactiveProperty<bool> IsDamaged = new ReactiveProperty<bool>(false);
        IDisposable damageResetSubscription;

        float GroggiTime = 0.5f;
        int hp;
        private void Start()
        {
            GetPlayer = GetComponent<Player>();
            GetRigidbody2D = GetPlayer.GetRigidbody;
            hp = Stage_UI_View.Instance.GetHp;
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
                ResetDamageState();
                Debug.Log($"hp : {hp}");
                Debug.Log($"Stage_UI_View.Instance.GetHp : {Stage_UI_View.Instance.GetHp}");
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
        private void ResetDamageState()
        {
            // 기존 타이머가 있으면 초기화
            damageResetSubscription?.Dispose();
            IsDamaged.Value = true;
            // 0.5초 후 IsDamaged를 다시 false로 설정
            damageResetSubscription = Observable.Timer(TimeSpan.FromSeconds(GroggiTime))
                .Subscribe(_ => IsDamaged.Value = false);
        }
        public bool GetDamaged => IsDamaged.Value;
    }
}