using UnityEngine;
using player;
public class ArrowTrap : MonoBehaviour
{
    [SerializeField] GameObject Arrow;
    Vector3 Offset = Vector3.zero;
    float dist = 5f;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position + transform.right*2/3, transform.right, dist);
        Debug.DrawRay(transform.position + transform.right * 2 / 3, transform.right * dist, Color.green);
        if (hit2D.collider != null)
        {
            if (hit2D.collider.gameObject.TryGetComponent(out ICatchable _) || hit2D.collider.gameObject.TryGetComponent(out Player _))
                ShootArrow();
        }
    }

    void ShootArrow()
    {
        Instantiate(Arrow, transform.position + transform.right, Quaternion.Euler(0, 0, -90));
        enabled = false;
    }
}
