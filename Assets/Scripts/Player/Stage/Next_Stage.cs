using UnityEngine;
using TMPro;
using player;
public class Next_Stage : MonoBehaviour
{
    [SerializeField] MaskVariation GetMaskVariation;
    [SerializeField] TextMeshProUGUI text;
    bool trigger = false;
    bool canInteract = false;
    Player GetPlayer;

    private void Start()
    {
        GetPlayer = GameManager.Instance.GetPlayer.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = true; // 상호작용 가능 상태로 변경
            text.text = $"Press {GetPlayer.GetPlayer_Input.PlayerKey.Item} key";
            text.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = false; // 상호작용 종료
            text.enabled = false;
        }
    }

    private void Update()
    {
        if (canInteract && !trigger && 
            GetPlayer.GetPlayer_Input.GetKeyPress(GetPlayer.GetPlayer_Input.PlayerKey.Item))
        {
            trigger = true;
            GameManager.Instance.GetStageNumber = GameManager.Instance.CurrentSceneNumber + 1;
            StartCoroutine(GameManager.Instance.PreloadScene("Stage Rest", GetMaskVariation.Darker()));
        }
    }
}