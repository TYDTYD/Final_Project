namespace player
{
    using UnityEngine;
    public class Player_Item : MonoBehaviour
    {
        GameObject obj = null;
        GameObject item = null;
        Vector2 rightPos = new Vector2(0.25f, -0.05f);
        Vector2 leftPos = new Vector2(-0.25f, -0.05f);
        [SerializeField] Player parent;
        [SerializeField] BoxCollider2D GetBox;
        bool IsCatch = false;

        private void OnEnable() => UpdateManager.Instance.SubscribeUpdate(UpdateMethod);
        private void OnDisable() => UpdateManager.Instance.UnSubscribeUpdate(UpdateMethod);
        private void UpdateMethod() => GetBox.offset = parent.GetSprite.flipX ? leftPos : rightPos;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (obj != null)
                return;
            if (collision.gameObject.TryGetComponent(out ICatchable _))
            {
                obj = collision.gameObject;
                return;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out ICatchable _))
            {
                if (collision.gameObject == obj)
                {
                    obj = null;
                }
            }
        }
        public GameObject GetObj => obj;
        public GameObject CurrentItem { get => item; set => item = value; }
        public bool GetCatch { get => IsCatch; set => IsCatch = value; }
    }
}