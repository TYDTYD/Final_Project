using UnityEngine;
using System.Collections;
public class Explosion : MonoBehaviour
{
    WaitForSeconds timer = new WaitForSeconds(3f);
    float radius = 2f;
    void Start()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return timer;
        DestroyObjectsInRadius();
        Destroy(gameObject);
    }

    void DestroyObjectsInRadius()
    {
        Vector2 position = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius); // 반경 내 오브젝트 찾기

        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != gameObject)
            {
                Destroy(col.gameObject);
            }
        }
    }
}
