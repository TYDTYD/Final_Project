using UnityEngine;
using player;
public class Player_Attack : MonoBehaviour
{
    [SerializeField] Player parent;
    public BoxCollider2D GetBox;
    Vector2 rightPos = new Vector2(0.2f, -0.05f);
    Vector2 leftPos = new Vector2(-0.2f, -0.05f);
    private void Update()
    {
        GetBox.offset = parent.GetSprite.flipX ? leftPos : rightPos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out IHealth health))
        {
            health.TakeDamage(1, 100, gameObject);
        }
    }
}