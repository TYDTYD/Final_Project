using UnityEngine;

public class Player_Ceiling : MonoBehaviour
{
    bool edgeDetact1 = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            edgeDetact1 = false;
            return;
        }
        edgeDetact1 = true;
    }

    public bool GetEdgeDetact1
    {
        get
        {
            return edgeDetact1;
        }
    }
}
