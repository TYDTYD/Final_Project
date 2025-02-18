using UnityEngine;
using System;
public class Stage_Load : MonoBehaviour
{
    Vector3 plus = new Vector3(0.06f, 0);
    event Action MovingScene;

    [SerializeField] MaskAnim GetMaskAnim;
    [SerializeField] Transform endDoor;

    private void Start()
    {
        MovingScene += GameManager.Instance.OnStageLoad;
    }

    void FixedUpdate()
    {
        transform.position += plus;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            GetMaskAnim.MaskAnimStart_Small(endDoor.position, MovingScene);
            Stage_UI_Presenter.Instance.stage.Value = GameManager.Instance.GetStageNumber;
        }
    }
}