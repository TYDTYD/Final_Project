using UnityEngine;
using player;
public class JetPack : MonoBehaviour, IBag
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite original;
    [SerializeField] Sprite boost;

    GameObject Owner;
    Player player;
    Vector3 Offset = Vector3.zero;
    Vector3 localOffset = new Vector3(0,-0.04f);
    Vector3 momentum = new Vector3(0, 61f);
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (player != null)
        {
            Player_Input player_Input = player.GetPlayer_Input;
            Interact KeyManager = player_Input.PlayerKey;
            KeyCode UpKey = KeyManager.keyCodes[(int)KeySequence.Up];
            bool Pressed = player_Input.GetKeyPress(UpKey);
            bool StateCheck = (player.CurrentState == player.GetJump || player.CurrentState == player.GetFall);
            if (Pressed && StateCheck)
            {
                Owner.GetComponent<Rigidbody2D>().AddForce(momentum);
                spriteRenderer.sprite = Pressed ? boost : original;
            }
            else
            {
                spriteRenderer.sprite = original;
            }
        }
        if (Owner != null)
        {
            transform.localPosition = localOffset;
            spriteRenderer.flipX = Owner.GetComponent<SpriteRenderer>().flipX;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            gameObject.layer = 3;
            player = null;
            Owner = null;
        }
    }
    public void PutOn(GameObject obj, Vector3 pos)
    {
        Owner = obj;
        Owner.TryGetComponent(out player);
        gameObject.layer = 3;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.SetParent(obj.transform);
        transform.localPosition = pos;
    }
    public void TakeOff(GameObject obj, Vector3 left, Vector3 right)
    {
        transform.SetParent(null);
        Vector3 dir = (obj.GetComponent<SpriteRenderer>().flipX ? left : right);
        Offset.x = dir.x;
        transform.position = obj.transform.position + Offset;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(dir);
        gameObject.layer = 0;
    }
}
