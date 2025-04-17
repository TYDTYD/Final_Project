using UnityEngine;
using player;
public class LadderCheck : MonoBehaviour
{
    [SerializeField] Player_Rigidbody Player_Rb;
    bool LadderContacted;
    private void OnEnable() => UpdateManager.Instance.SubscribeUpdate(UpdateMethod);
    private void OnDisable() => UpdateManager.Instance.UnSubscribeUpdate(UpdateMethod);
    private void UpdateMethod()
    {
        if (LadderContacted)
            Player_Rb.GetLadder = true;
        else
            Player_Rb.GetLadder = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            LadderContacted = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            LadderContacted = false;
            Player_Rb.GetClimbing = false;
        }
    }
}
