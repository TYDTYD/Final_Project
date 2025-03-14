using UnityEngine;
using System;
public class Next_Stage : MonoBehaviour
{
    [SerializeField] MaskVariation GetMaskVariation;
    [SerializeField] Sprite openDoor;
    [SerializeField] Sprite closeDoor;
    bool trigger = false;
    bool canInteract = false;
    SpriteRenderer GetSprite;

    private void Start()
    {
        GetSprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = true; // 상호작용 가능 상태로 변경
            GetSprite.sprite = openDoor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = false; // 상호작용 종료
            GetSprite.sprite = closeDoor;
        }
    }

    private void Update()
    {
        if (canInteract && !trigger && Input.GetKeyDown(Interact.GetKeyCode(Interact.KeySequence.Item)))
        {
            trigger = true;
            GameManager.Instance.GetStageNumber = GameManager.Instance.CurrentSceneNumber + 1;
            StartCoroutine(GameManager.Instance.PreloadScene("Stage Rest", GetMaskVariation.Darker()));
        }
    }
}
