using UnityEngine;

public class Player_Flip : MonoBehaviour
{
    bool edgeDetact2 = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            edgeDetact2 = true;
            return;
        }
        edgeDetact2 = false;
    }

    public bool GetEdgeDetact2
    {
        get
        {
            return edgeDetact2;
        }
    }
}
