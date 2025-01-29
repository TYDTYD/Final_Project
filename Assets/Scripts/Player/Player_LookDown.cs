using UnityEngine;
using Unity.Cinemachine;
public class Player_LookDown : MonoBehaviour
{
    public CinemachineCamera virtualCamera; 
    public Transform playerTransform;           
    public Transform downViewTransform;
    Player_Anim GetPlayer_Anim;
    Player_Rigidbody player_Rigidbody;

    float keyPressTime = 0f;              
    float holdTime = 1f;      
    bool isDownView = false;        

    private void Start()
    {
        player_Rigidbody = GetComponent<Player_Rigidbody>();
        GetPlayer_Anim = GetComponent<Player_Anim>();
    }

    void Update()
    {
        if (player_Rigidbody.isLadder)
            return;

        if(GetPlayer_Anim.SittingTime > holdTime && !isDownView)
        {
            SetTopView();
        }
        else
        {
            keyPressTime = 0f;

            if (isDownView)
            {
                SetPlayerView();
            }
        }
    }

    // 위쪽을 바라보는 뷰로 전환
    void SetTopView()
    {
        virtualCamera.Follow = downViewTransform;  
        isDownView = true;
    }

    // 플레이어를 따라가는 뷰로 전환
    void SetPlayerView()
    {
        virtualCamera.Follow = playerTransform;  // Follow를 플레이어로 설정
        isDownView = false;
    }
}
