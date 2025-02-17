using UnityEngine;
using System;
public class Next_Stage : MonoBehaviour
{
    [SerializeField] MaskVariation GetMaskVariation;
    event Action Moving_Scene;
    bool trigger = false;
    bool canInteract = false;

    private void Start()
    {
        Moving_Scene += GameManager.Instance.OnRestPlaceLoad;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = true; // 상호작용 가능 상태로 변경
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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
            GameManager.Instance.GetStageNumber = GameManager.Instance.CurrentStageNumber + 1;
            GetMaskVariation.Darker(Moving_Scene);
        }
    }
}
