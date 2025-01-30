using UnityEngine;
using Unity.Cinemachine;
public class Player_LookUp : MonoBehaviour
{
    [SerializeField] CinemachineCamera virtualCamera; // Cinemachine 가상 카메라
    [SerializeField] Transform playerTransform;             // 플레이어 Transform
    [SerializeField] Transform topViewTransform;            // 위쪽을 바라보는 위치 Transform
    Player_Rigidbody player_Rigidbody;

    float keyPressTime = 0f;              // 키가 눌린 시간
    float holdTime = 1f;                   // 카메라가 전환되는 키 누름 시간

    bool isTopView = false;        // 현재 카메라가 위쪽을 보는 상태인지 확인

    private void Start()
    {
        player_Rigidbody = GetComponent<Player_Rigidbody>();
    }

    void Update()
    {
        if (player_Rigidbody.isLadder)
            return;
        // 위 방향키 입력 체크
        if (Input.GetKey(InputHandler.UpKey))
        {
            keyPressTime += Time.deltaTime;

            if (keyPressTime > holdTime && !isTopView)
            {
                SetTopView();
            }
        }
        else
        {
            keyPressTime = 0f;

            // 위쪽 뷰에서 플레이어로 돌아가기
            if (isTopView)
            {
                SetPlayerView();
            }
        }
    }

    // 위쪽을 바라보는 뷰로 전환
    void SetTopView()
    {
        virtualCamera.Follow = topViewTransform;  // 위쪽 Transform을 바라보도록 설정
        isTopView = true;
    }

    // 플레이어를 따라가는 뷰로 전환
    void SetPlayerView()
    {
        virtualCamera.Follow = playerTransform;  // Follow를 플레이어로 설정
        isTopView = false;
    }
}