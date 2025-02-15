using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class Player_Next_Stage : MonoBehaviour
{
    [SerializeField] MaskAnim GetMaskAnim;
    // 다음 씬으로 넘어가는 액션
    Action Moving_Scene;
    bool trigger = false;

    private void Start()
    {
        Moving_Scene += LoadScene;
    }

    void LoadScene()
    {
        GameManager.Instance.CurrentStageNumber++;
        SceneManager.LoadScene("Stage Rest");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (trigger)
            return;

        if (collision.CompareTag("Finish"))
        {
            if (Input.GetKeyDown(Interact.GetKeyCode(Interact.KeySequence.Item)))
            {
                trigger = true;
                GetMaskAnim.MaskAnimStart_Small(transform.position, Moving_Scene);
            }
        }
    }
}
