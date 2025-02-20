using UnityEngine;
using System.Collections;
public class Stage_Load : MonoBehaviour
{
    Vector3 plus = new Vector3(0.06f, 0);

    [SerializeField] MaskAnim GetMaskAnim;
    [SerializeField] Transform endDoor;
    bool trigger = false;

    void FixedUpdate()
    {
        transform.position += plus;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish") && !trigger)
        {
            int num = GameManager.Instance.GetStageNumber;
            trigger = true;
            StartCoroutine(GameManager.Instance.PreloadScene(num, GetMaskAnim.ControlScale(pos: endDoor.position, targetSize: 0f)));
            Stage_UI_Presenter.Instance.stage.Value = num;
        }
    }
}