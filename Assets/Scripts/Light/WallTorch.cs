using UnityEngine;

public class WallTorch : MonoBehaviour
{
    [SerializeField] GameObject Light;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Light"))
        {
            if (collision.TryGetComponent(out TorchLight _))
            {
                Light.SetActive(true);
            }
        }
    }
}