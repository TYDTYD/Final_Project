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
            canInteract = true; // ��ȣ�ۿ� ���� ���·� ����
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            canInteract = false; // ��ȣ�ۿ� ����
        }
    }

    private void Update()
    {
        if (canInteract && !trigger && Input.GetKeyDown(Interact.GetKeyCode(Interact.KeySequence.Item)))
        {
            Debug.Log("����");
            trigger = true;
            GetMaskVariation.Darker(Moving_Scene);
        }
    }
}
