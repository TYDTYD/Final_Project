using UnityEngine;
using Unity.Cinemachine;
public class Player_Tracking : MonoBehaviour
{
    CinemachineCamera playerCamera;
    void Start()
    {
        playerCamera = GetComponent<CinemachineCamera>();
        playerCamera.Follow = GameManager.Instance.GetPlayer.transform;
        GameManager.Instance.GetPlayer.GetComponent<Player>().GetPlayer_Tracking = this;
    }

    public CinemachineCamera GetPlayerCamera => playerCamera;
}
