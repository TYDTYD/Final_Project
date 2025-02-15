using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class Player_Stage : MonoBehaviour
{
    Vector3 plus = new Vector3(0.06f, 0);
    Action MovingScene;

    [SerializeField] MaskAnim GetMaskAnim;
    [SerializeField] Transform startDoor, endDoor;
    void FixedUpdate()
    {
        transform.position += plus;
    }

    private void Start()
    {
        MovingScene += LoadScene;
        GetMaskAnim.MaskAnimStart_Great(startDoor.position);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(GameManager.Instance.CurrentStageNumber);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            GetMaskAnim.MaskAnimStart_Small(endDoor.position, MovingScene);
        }
    }
}
