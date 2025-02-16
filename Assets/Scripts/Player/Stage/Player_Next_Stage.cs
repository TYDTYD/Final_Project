using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class Player_Next_Stage : MonoBehaviour
{
    [SerializeField] MaskVariation GetMaskVariation;
    public Action Moving_Scene;
    bool trigger = false;
    bool canInteract = false;

    private void Start()
    {
        Moving_Scene += LoadScene;
    }

    void LoadScene()
    {
        GameManager.Instance.CurrentStageNumber++;
        SceneManager.LoadScene("Stage Rest");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            canInteract = true; // 상호작용 가능 상태로 변경
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            canInteract = false; // 상호작용 종료
        }
    }

    private void Update()
    {
        if (canInteract && !trigger && Input.GetKeyDown(Interact.GetKeyCode(Interact.KeySequence.Item)))
        {
            Debug.Log("누름");
            trigger = true;
            GetMaskVariation.Darker(Moving_Scene);
        }
    }
}
