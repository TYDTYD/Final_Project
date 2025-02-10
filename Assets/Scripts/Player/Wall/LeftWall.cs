using UnityEngine;

public class LeftWall : MonoBehaviour
{
    [SerializeField] Player GetPlayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Debug.Log("°¨Áö");
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
            bool flip = GetPlayer.GetSprite.flipX;
            if (flip)
                GetPlayer.GetPlayer_Input.GetRightMove.GetWallContact = 1f;
            else
                GetPlayer.GetPlayer_Input.GetLeftMove.GetWallContact = 1f;
        }
    }
}
