namespace player
{
    using UnityEngine;
    using Unity.Cinemachine;
    public class Player_LookUpDown : MonoBehaviour
    {
        [SerializeField] Transform playerTransform;
        [SerializeField] Transform topViewTransform;
        [SerializeField] Transform downViewTransform;

        Player GetPlayer;
        Player_Input GetPlayer_Input;
        Interact GetKeyCode;
        Player_Rigidbody player_Rigidbody;
        CinemachineCamera virtualCamera;

        float keyPressTime = 0f;              // 키가 눌린 시간
        float holdTime = 1f;                   // 카메라가 전환되는 키 누름 시간
        bool isTopView = false;
        bool isDownView = false;

        private void Start()
        {
            GetPlayer = GetComponent<Player>();
            GetPlayer_Input = GetPlayer.GetPlayer_Input;
            GetKeyCode = GetPlayer_Input.PlayerKey;
            player_Rigidbody = GetPlayer.GetPlayer_Rigidbody;
            virtualCamera = GetPlayer.GetPlayer_Tracking.GetPlayerCamera;
        }

        void Update()
        {
            if (player_Rigidbody.GetLadder)
                return;
            // 위 방향키 입력 체크
            if (GetPlayer_Input.GetKeyPress(GetKeyCode.Up))
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
                if (isTopView)
                {
                    SetPlayerView(true);
                }
            }

            if (GetPlayer.GetSittingTime > holdTime && !isDownView)
            {
                SetDownView();
            }
            else if (GetPlayer.GetSittingTime < holdTime)
            {
                if (isDownView)
                {
                    SetPlayerView(false);
                }
            }
        }

        void SetTopView()
        {
            virtualCamera.Follow = topViewTransform;  // 위쪽 Transform을 바라보도록 설정
            isTopView = true;
        }
        void SetDownView()
        {
            virtualCamera.Follow = downViewTransform;
            isDownView = true;
        }

        // 플레이어를 따라가는 뷰로 전환
        void SetPlayerView(bool Up)
        {
            virtualCamera.Follow = playerTransform;  // Follow를 플레이어로 설정
            if (Up)
                isTopView = false;
            else
                isDownView = false;
        }
    }
}