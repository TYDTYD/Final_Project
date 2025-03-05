using UnityEngine;
using Unity.Cinemachine;
using player;
public class CameraManager : MonoBehaviour
{
    GameManager gameManager;
    
    [SerializeField] MaskAnim GetMask;
    [SerializeField] MaskVariation GetMaskVariation;
    [SerializeField] CinemachineCamera GetStatisticCamera;
    [SerializeField] CinemachineCamera GetPlayCamera;
    [SerializeField] Death_Cam GetDeathCamera;
    Player GetPlayer;
    
    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.StageLoad += OnStageStart;

        if (GetPlayer == null)
            OnStageStart();
    }

    private void OnDestroy()
    {
        if (GetPlayer != null)
            GetPlayer.GetPlayer_Health.DeathEvent -= WaitBook;
    }

    void OnStageStart()
    {
        if (gameManager.GetPlayer.TryGetComponent(out Player player))
            GetPlayer = player;

        if (GetPlayer)
            GetPlayer.GetPlayer_Health.DeathEvent += WaitBook;
    }

    void WaitBook() => Invoke(nameof(GotoBook), 2f);
    void GotoBook() => StartCoroutine(GetMaskVariation.Darker(CameraChange));
    void CameraChange()
    {
        GetDeathCamera.gameObject.SetActive(true);
        GetPlayCamera.gameObject.SetActive(false);
        GetStatisticCamera.gameObject.SetActive(true);
        StartCoroutine(GetMaskVariation.Brighter());
    }
}
