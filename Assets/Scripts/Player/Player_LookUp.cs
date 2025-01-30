using UnityEngine;
using Unity.Cinemachine;
public class Player_LookUp : MonoBehaviour
{
    [SerializeField] CinemachineCamera virtualCamera; // Cinemachine ���� ī�޶�
    [SerializeField] Transform playerTransform;             // �÷��̾� Transform
    [SerializeField] Transform topViewTransform;            // ������ �ٶ󺸴� ��ġ Transform
    Player_Rigidbody player_Rigidbody;

    float keyPressTime = 0f;              // Ű�� ���� �ð�
    float holdTime = 1f;                   // ī�޶� ��ȯ�Ǵ� Ű ���� �ð�

    bool isTopView = false;        // ���� ī�޶� ������ ���� �������� Ȯ��

    private void Start()
    {
        player_Rigidbody = GetComponent<Player_Rigidbody>();
    }

    void Update()
    {
        if (player_Rigidbody.isLadder)
            return;
        // �� ����Ű �Է� üũ
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

            // ���� �信�� �÷��̾�� ���ư���
            if (isTopView)
            {
                SetPlayerView();
            }
        }
    }

    // ������ �ٶ󺸴� ��� ��ȯ
    void SetTopView()
    {
        virtualCamera.Follow = topViewTransform;  // ���� Transform�� �ٶ󺸵��� ����
        isTopView = true;
    }

    // �÷��̾ ���󰡴� ��� ��ȯ
    void SetPlayerView()
    {
        virtualCamera.Follow = playerTransform;  // Follow�� �÷��̾�� ����
        isTopView = false;
    }
}