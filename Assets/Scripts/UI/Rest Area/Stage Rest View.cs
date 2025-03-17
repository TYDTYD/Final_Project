using UnityEngine;
using TMPro;
using System;

public class StageRestView : MonoBehaviour
{
    Stage_UI_View view;

    [SerializeField] TextMeshProUGUI ThisLevelTime;
    [SerializeField] TextMeshProUGUI ThisLevelMoney;
    [SerializeField] TextMeshProUGUI TotalTime;
    [SerializeField] TextMeshProUGUI TotalMoney;
    [SerializeField] TextMeshProUGUI hp_text;
    [SerializeField] TextMeshProUGUI bomb_text;
    [SerializeField] TextMeshProUGUI rope_text;
    [SerializeField] TextMeshProUGUI stage_text;
    void Start()
    {
        view = Stage_UI_View.Instance;
        UpdateUI();
    }
    void UpdateUI()
    {
        ThisLevelTime.text = ChangeIntToString(view.GetTime);
        ThisLevelMoney.text = "+" + view.GetStageMoney.ToString();
        TotalTime.text = ChangeIntToString(view.GetTotalTime);
        TotalMoney.text = view.GetMoney.ToString();
        hp_text.text = view.GetHp.ToString();
        bomb_text.text = view.GetBomb.ToString();
        rope_text.text = view.GetRope.ToString();
        stage_text.text = view.GetStage.ToString() + " Completed!";
    }
    string ChangeIntToString(int t)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(t);
        return timeSpan.ToString(@"mm\:ss");
    }
}