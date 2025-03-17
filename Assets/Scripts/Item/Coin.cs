using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Stage_UI_View.Instance.IncreaseMoney(10000);
            gameObject.SetActive(false);
        }
    }
}