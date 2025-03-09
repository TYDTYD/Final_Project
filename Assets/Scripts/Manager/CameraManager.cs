using UnityEngine;
using Unity.Cinemachine;
using System;
using player;
public class CameraManager : MonoBehaviour
{
    GameManager gameManager;
    Player GetPlayer;
    Action UIActive;

    [SerializeField] MaskAnim GetMask;
    [SerializeField] MaskVariation GetMaskVariation;
    [SerializeField] CinemachineCamera GetStatisticCamera;
    [SerializeField] CinemachineCamera GetPlayCamera;
    [SerializeField] Death_Cam GetDeathCamera;
    [SerializeField] GameObject UI;
    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.StageLoad += OnStageStart;
        UIActive += UISetActive;
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
    void UISetActive() => UI.SetActive(true);
    void WaitBook() => Invoke(nameof(GotoBook), 2f);
    void GotoBook() => StartCoroutine(GetMaskVariation.Darker(CameraChange));
    void CameraChange()
    {
        GetDeathCamera.gameObject.SetActive(true);
        GetPlayCamera.gameObject.SetActive(false);
        GetStatisticCamera.gameObject.SetActive(true);
        UIActive();
        StartCoroutine(GetMaskVariation.Brighter());
    }
}