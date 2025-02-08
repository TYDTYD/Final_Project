using UnityEngine;

public class Player_Stage : MonoBehaviour
{
    Vector3 plus = new Vector3(0.06f, 0);
    [SerializeField] MaskAnim GetMaskAnim;
    [SerializeField] Transform startDoor, endDoor;
    void FixedUpdate()
    {
        transform.position += plus;
    }

    private void Start()
    {
        GetMaskAnim.MaskAnimStart_Great(startDoor.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            GetMaskAnim.MaskAnimStart_Small(endDoor.position);
        }
    }
}
