using UnityEngine;

public class PauseUIActive : MonoBehaviour
{
    [SerializeField] GameObject Paused_UI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = (Paused_UI.activeInHierarchy) ? 1f : 0f;
            Paused_UI.SetActive(!Paused_UI.activeInHierarchy);
        }
    }
}