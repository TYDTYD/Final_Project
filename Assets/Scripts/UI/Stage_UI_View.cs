using UnityEngine;
using Unity.Cinemachine;
using TMPro;
public class Stage_UI_View : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hp_text;
    [SerializeField] TextMeshProUGUI bomb_text;
    [SerializeField] TextMeshProUGUI rope_text;
    [SerializeField] TextMeshProUGUI money_text;
    [SerializeField] TextMeshProUGUI time_text;
    [SerializeField] TextMeshProUGUI stage_text;

    [SerializeField] MaskAnim MaskAnim;
    [SerializeField] MaskVariation MaskVariation;
    [SerializeField] CinemachineCamera StatisticCamera;
    [SerializeField] CinemachineCamera PlayCamera;
    [SerializeField] Camera DeathCam;
    
    void Update_UI(TextMeshProUGUI ui, string text)
    {
        if (ui != null)
            ui.text = text;
    }

    public void Update_Hp_UI(string text) => Update_UI(hp_text, text);
    public void Update_Bomb_UI(string text) => Update_UI(bomb_text, text);
    public void Update_Rope_UI(string text) => Update_UI(rope_text, text);
    public void Update_Money_UI(string text) => Update_UI(money_text, text);
    public void Update_Time_UI(string text) => Update_UI(time_text, text);
    public void Update_Stage_UI(string text) => Update_UI(stage_text, text);

    public MaskAnim GetMaskAnim => MaskAnim;
    public MaskVariation GetMaskVariation => MaskVariation;
    public CinemachineCamera GetStatisticCamera => StatisticCamera;
    public CinemachineCamera GetPlayCamera => PlayCamera;
    public Camera GetDeathCam => DeathCam;
}