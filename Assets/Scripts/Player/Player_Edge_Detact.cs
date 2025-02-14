using UnityEngine;

public class Player_Edge_Detact : MonoBehaviour
{
    Player_Rigidbody player_Rigidbody;

    private void Start()
    {
        player_Rigidbody = GetComponentInParent<Player_Rigidbody>();
    }

    public bool isEdge = false;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") && player_Rigidbody.Grounded)
            isEdge = true;
        else
            isEdge = false;
    }
}
