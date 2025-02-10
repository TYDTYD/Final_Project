using UnityEngine;
using Unity.Cinemachine;
public class Player_State : MonoBehaviour
{
    Player GetPlayer;
    [SerializeField] MaskAnim GetMaskAnim;
    [SerializeField] MaskVariation GetMaskVariation;
    [SerializeField] CinemachineCamera GetStatisticCamera;
    [SerializeField] Camera GetDeathCam;
    [SerializeField] CinemachineCamera GetPlayCamera;
    private void Start()
    {
        GetPlayer = GetComponent<Player>();
        GetPlayer.GetPlayer_Health.DeathEvent += WaitBook;
    }

    void WaitBook() 
    {
        Invoke("GotoBook", 2f);
    }

    void GotoBook()
    {
        GetMaskVariation.Darker(CameraChange);
    }
    void CameraChange()
    {
        GetDeathCam.gameObject.SetActive(true);
        GetPlayCamera.gameObject.SetActive(false);
        GetStatisticCamera.gameObject.SetActive(true);
        GetMaskVariation.Brighter(null);
    }
    void Update()
    {
        switch (GetPlayer.CurrentState)
        {
            case Player.State.Idle_State:
                break;
            case Player.State.Jump_State:
                break;
            case Player.State.Ladder_State:
                break;
            case Player.State.Damage_State:
                break;
            case Player.State.Attack_State:
                break;
            case Player.State.Move_State:
                break;
            case Player.State.Fall_State:
                break;
            case Player.State.Land_State:
                break;
            case Player.State.LadderStop_State:
                break;
            case Player.State.EdgeDetact_State:
                break;
            case Player.State.SittingStart_State:
                break;
            case Player.State.Sitting_State:
                break;
            case Player.State.SittingMove_State:
                break;
            case Player.State.Edge_State:
                break;
            case Player.State.Death_State:
                break;
        }
    }

    private void OnDestroy()
    {
        if (GetPlayer != null)
            GetPlayer.GetPlayer_Health.DeathEvent -= WaitBook;
    }
}