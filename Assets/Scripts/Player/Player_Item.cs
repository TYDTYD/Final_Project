namespace player
{
    using UnityEngine;
    public class Player_Item : MonoBehaviour
    {
        GameObject obj = null;
        GameObject item = null;
        bool IsCatch = false;
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
        private void Update()
        {
            Debug.Log((item, obj));
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