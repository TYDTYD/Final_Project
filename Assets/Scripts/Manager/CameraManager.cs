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
        {
            GetPlayer = player;
            SubscribeDeathEvent(true);
        }
    }
    void SubscribeDeathEvent(bool subscribe)
    {
        if (GetPlayer == null)
            return;
        if (subscribe)
            GetPlayer.GetPlayer_Health.DeathEvent += WaitBook;
        else
            GetPlayer.GetPlayer_Health.DeathEvent -= WaitBook;
    }
    private void OnDisable() => SubscribeDeathEvent(false);
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