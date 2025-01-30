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

    // �Ʒ����� �ٶ󺸴� ��� ��ȯ
    void SetDownView()
    {
        virtualCamera.Follow = downViewTransform;  
        isDownView = true;
    }

    // �÷��̾ ���󰡴� ��� ��ȯ
    void SetPlayerView()
    {
        virtualCamera.Follow = playerTransform;  // Follow�� �÷��̾�� ����
        isDownView = false;
    }
}
