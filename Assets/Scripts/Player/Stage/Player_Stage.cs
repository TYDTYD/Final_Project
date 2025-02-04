using UnityEngine;

public class Player_Stage : MonoBehaviour
{
    Vector3 plus = new Vector3(0.05f, 0);
    [SerializeField] MaskAnim GetMaskAnim;
    void FixedUpdate()
    {
        transform.position += plus;
    }

    private void Start()
    {
        GetMaskAnim.MaskAnimStart_Great();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            GetMaskAnim.MaskAnimStart_Small();
        }
    }
}
