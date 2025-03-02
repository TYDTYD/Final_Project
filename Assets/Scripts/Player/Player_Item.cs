namespace player
{
    using UnityEngine;
    public class Player_Item : MonoBehaviour
    {
        GameObject obj = null;
        bool IsCatch = false;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out ICatchable _))
            {
                obj = collision.gameObject;
                return;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (obj == null)
                return;
            if (collision.gameObject.TryGetComponent(out ICatchable _))
            {
                if (collision.gameObject == obj)
                {
                    obj = null;
                }
            }
        }

        public GameObject GetObject => obj;
        public bool GetCatch { get => IsCatch; set => IsCatch = value; }
    }
}