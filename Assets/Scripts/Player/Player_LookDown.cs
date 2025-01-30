using UnityEngine;
using Unity.Cinemachine;
public class Player_LookDown : MonoBehaviour
{
    [SerializeField] CinemachineCamera virtualCamera; 
    [SerializeField] Transform playerTransform;           
    [SerializeField] Transform downViewTransform;
    Player_Anim GetPlayer_Anim;
    Player_Rigidbody player_Rigidbody;
    
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
            SetDownView();
        }
        else if(GetPlayer_Anim.SittingTime < holdTime)
        {
            if (isDownView)
            {
                SetPlayerView();
            }
        }
    }

    // 아래쪽을 바라보는 뷰로 전환
    void SetDownView()
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
