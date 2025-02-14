using UnityEngine;

public class Player_Right_Flip : MonoBehaviour
{
    bool edgeDetact = false;
    Player GetPlayer;

    private void Start()
    {
        GetPlayer=GetComponentInParent<Player>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (GetPlayer.GetPlayer_Rigidbody.Grounded)
            return;
        if (collision.gameObject.CompareTag("Ground") && collision.contacts[0].normal.y < 0.7f)
        {
            Debug.Log("º® °¨Áö");
            edgeDetact = true;
            return;
        }
        edgeDetact = false;
    }

    public bool GetEdgeDetact => edgeDetact;
}
