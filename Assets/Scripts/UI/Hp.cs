using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Hp : MonoBehaviour
{
    TextMeshProUGUI text;
    public Player GetPlayer;
    Player_Health GetPlayer_Health;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        GetPlayer_Health = GetPlayer.GetPlayer_Health;
    }
    private void Update()
    {
        text.text = GetPlayer_Health.health.Value.ToString();
    }
}
