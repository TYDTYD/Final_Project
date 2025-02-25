using UnityEngine;

public class TitlePresenter
{
    IView GetView;
    TitleModel GetModel;
    public TitlePresenter(IView view)
    {
        GetView = view;
        GetModel = new TitleModel(false);
    }

    public void OnGameStartButtonClicked()
    {
        GetModel.OnClick();
        GetView.UpdateUI(GetModel);
    }
    public void OnSettingButtonClicked()
    {
        GetModel.OnClick();
        GetView.UpdateUI(GetModel);
    }
    public void OnGameExitButtonClicked()
    {
        GetModel.OnClick();
        GetView.UpdateUI(GetModel);
    }

    public void OnGameStartButtonSelected()
    {
        GetView.SelectUI(GetModel);
    }
    public void OnSettingButtonSelected()
    {
        GetView.SelectUI(GetModel);
    }
    public void OnGameExitButtonSelected()
    {
        GetView.SelectUI(GetModel);
    }
}
