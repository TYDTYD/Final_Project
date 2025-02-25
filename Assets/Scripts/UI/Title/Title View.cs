using UnityEngine;
using UnityEngine.UI;
public class TitleView : MonoBehaviour, IView
{
    [SerializeField] Button[] GetButtons;
    [SerializeField] MaskVariation GetMask;
    TitlePresenter Presenter;

    private void Start()
    {
        Presenter = new TitlePresenter(this);
    }

    public void UpdateUI(TitleModel model)
    {
        
    }
    public void SelectUI(TitleModel model)
    {

    }
}
