using UnityEngine;
using Unity.Cinemachine;
using System;
using player;
public class CameraManager : MonoBehaviour
{
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
        if (GameManager.Instance.GetPlayer.TryGetComponent(out Player player))
            GetPlayer = player;
        GetPlayer.GetPlayer_Health.DeathEvent += WaitBook;
    }
    private void OnDisable()
    {
        if(GetPlayer)
            GetPlayer.GetPlayer_Health.DeathEvent -= WaitBook;
    }
    void WaitBook() => Invoke(nameof(GotoBook), 2f);
    void GotoBook() => StartCoroutine(GetMaskVariation.Darker(CameraChange));
    void CameraChange()
    {
        GetDeathCamera.gameObject.SetActive(true);
        GetPlayCamera.gameObject.SetActive(false);
        GetStatisticCamera.gameObject.SetActive(true);
        UI.SetActive(true);
        StartCoroutine(GetMaskVariation.Brighter());
    }
}