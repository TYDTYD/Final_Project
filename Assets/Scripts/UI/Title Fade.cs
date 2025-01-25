using UnityEngine;
using TMPro;
using UniRx.Triggers;
using UniRx;
using UnityEngine.SceneManagement;
using System.Collections;
public class TitleFade : MonoBehaviour
{
    TMP_Text text;
    [SerializeField] GameObject Panel;
    float FadeDuration = 1f;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        StartCoroutine(FadeLoop());
        this.UpdateAsObservable()
            .Select(_ => Input.GetKey(KeyCode.Return))
            .Where(x => x)
            .Subscribe(_ => {
                SceneManager.LoadScene("Lobby");
            });
    }

    IEnumerator FadeLoop()
    {
        while (true)
        {
            // Fade In
            yield return StartCoroutine(Fade(0, 1, FadeDuration));

            // Fade Out
            yield return StartCoroutine(Fade(1, 0, FadeDuration));
        }
    }

    IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            text.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            yield return null;
        }
        text.alpha = endAlpha; // 마지막 값 보정
    }
}
