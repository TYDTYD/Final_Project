using UnityEngine;

public class RightWall : MonoBehaviour
{
    [SerializeField] Player GetPlayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            bool flip = GetPlayer.GetSprite.flipX;
            if (flip)
                GetPlayer.GetPlayer_Input.GetRightMove.GetWallContact = 0f;
            else
                GetPlayer.GetPlayer_Input.GetLeftMove.GetWallContact = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            GetPlayer.GetPlayer_Input.GetRightMove.GetWallContact = 1f;
            GetPlayer.GetPlayer_Input.GetLeftMove.GetWallContact = 1f;
        }
    }
}
