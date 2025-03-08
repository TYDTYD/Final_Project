using UnityEngine;
using TMPro;
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
        ThisLevelTime.text = ChangeIntToString(view.View_Model.Time.Value);
        ThisLevelMoney.text = "+" + view.View_Model.Money.Value.ToString();
        TotalTime.text = ChangeIntToString(view.View_Model.TotalTime.Value);
        TotalMoney.text = view.View_Model.Money.Value.ToString();
        hp_text.text = view.View_Model.Health.Value.ToString();
        bomb_text.text = view.View_Model.Bomb.Value.ToString();
        rope_text.text = view.View_Model.Rope.Value.ToString();
        stage_text.text = view.View_Model.Stage.Value.ToString() + " Completed!";
    }
    string ChangeIntToString(int t) => $"{t / 60:D2}:{t % 60:D2}";
}
