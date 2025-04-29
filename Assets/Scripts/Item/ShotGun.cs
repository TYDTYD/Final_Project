using UnityEngine;
using UnityEngine.Pool;

public class ShotGun : MonoBehaviour, ICatchable, IItem
{
    Rigidbody2D rb;
    SpriteRenderer sprite;
    GameObject Owner;
    Vector3 Offset = Vector3.zero;

    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform shootPositionRight;
    [SerializeField] Transform shootPositionLeft;
    IObjectPool<Projectile> objectPool;
    int defaultCapacity = 20;
    int maxSize = 100;

    float threshold = 5f;
    int damage = 1;
    float speed = 40f;
    float nextTimeToShoot;
    float cooldown = 1f;
    float knockBackForce = 12f;

    private void Awake()
    {
        objectPool = new ObjectPool<Projectile>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, true, defaultCapacity, maxSize);
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Owner != null)
            sprite.flipX = Owner.GetComponent<SpriteRenderer>().flipX;
    }
    Projectile CreateProjectile()
    {
        Projectile projectileInstance = Instantiate(projectilePrefab);
        projectileInstance.ObjectPool = objectPool;
        return projectileInstance;
    }
    void OnGetFromPool(Projectile pooledObject) => pooledObject.gameObject.SetActive(true);
    void OnReleaseToPool(Projectile pooledObject) => pooledObject.gameObject.SetActive(false);
    void OnDestroyPooledObject(Projectile pooledObject) => Destroy(pooledObject.gameObject);
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.linearVelocity.magnitude < threshold)
        {
            return;
        }
        if (collision.gameObject.TryGetComponent(out IHealth health))
        {
            if (Owner != null && Owner == collision.gameObject)
                return;
            health.TakeDamage(damage, 100, gameObject);
        }
    }
    public void Grap(GameObject obj, Vector3 pos)
    {
        Owner = obj;
        gameObject.layer = 3;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.SetParent(obj.transform);
        transform.localPosition = pos;
    }
    public void Throw(GameObject obj, Vector3 left, Vector3 right)
    {
        transform.SetParent(null);
        Vector3 dir = (obj.GetComponent<SpriteRenderer>().flipX ? left : right);
        Offset.x = dir.x;
        transform.position = obj.transform.position + Offset;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(dir * 20f, ForceMode2D.Impulse);
        gameObject.layer = 0;
    }
    public void Use()
    {
        if (objectPool != null && Time.time > nextTimeToShoot)
        {
            Shoot();
            KnockBack();
        }
    }
    void Shoot()
    {
        int shotCount = 4;

        bool isFlipped = sprite.flipX;
        Transform currentShootPos = isFlipped ? shootPositionLeft : shootPositionRight;
        Vector2 shootDir = isFlipped ? Vector2.left : Vector2.right;

        for(int i=0; i<shotCount; i++)
        {
            Projectile bullet = objectPool.Get();
            if (bullet == null)
                continue;

            bullet.transform.SetPositionAndRotation(currentShootPos.position, currentShootPos.rotation);
            
            float randomAngle = Random.Range(-45f, 45f);
            Vector2 rotatedDir = Quaternion.Euler(0, 0, randomAngle) * shootDir;

            bullet.GetComponent<Rigidbody2D>().AddForce(rotatedDir.normalized * speed, ForceMode2D.Impulse);
        }

        nextTimeToShoot = Time.time + cooldown;
    }
    void KnockBack()
    {
        if (Owner == null)
            return;

        bool isFlipped = sprite.flipX;
        Vector2 baseDir = isFlipped ? Vector2.right : Vector2.left;
        Vector2 knockBackDir = (baseDir + Vector2.up).normalized;

        Rigidbody2D rb = Owner.GetComponent<Rigidbody2D>();
        rb.AddForce(knockBackDir * knockBackForce, ForceMode2D.Impulse);
    }
}
