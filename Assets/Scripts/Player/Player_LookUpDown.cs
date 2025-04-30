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

        float keyPressTime = 0f;
        float holdTime = 1f;
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

        private void OnEnable() => UpdateManager.Instance.SubscribeUpdate(UpdateMethod);
        private void OnDisable() => UpdateManager.Instance.UnSubscribeUpdate(UpdateMethod);

        void UpdateMethod()
        {
            if (GetPlayer.CurrentState == GetPlayer.GetLadder)
                return;
            // 위 방향키 입력 체크
            if (GetPlayer_Input.GetKeyPress(GetKeyCode.keyCodes[(int)KeySequence.Up]) && GetPlayer.CurrentState == GetPlayer.GetIdle)
            {
                keyPressTime += Time.deltaTime;
                if (keyPressTime > holdTime && !isTopView)
                {
                    SetTopView();
                }
            }
            else if (GetPlayer.CurrentState == GetPlayer.GetSitting)
            {
                keyPressTime += Time.deltaTime;
                if (keyPressTime > holdTime && !isDownView)
                {
                    SetDownView();
                }
            }
            else
            {
                keyPressTime = 0f;
                if (isTopView)
                {
                    SetPlayerView(true);
                }
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