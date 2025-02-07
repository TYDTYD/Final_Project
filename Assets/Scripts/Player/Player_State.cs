using UnityEngine;

public class Player_State : MonoBehaviour
{
    Player GetPlayer;

    private void Start()
    {
        GetPlayer = GetComponent<Player>();
    }
    void Update()
    {
        switch (GetPlayer.GetState)
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
                GetPlayer.GetPlayer_Input.enabled = false;
                break;
        }
    }
}
