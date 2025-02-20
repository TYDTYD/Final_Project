using UnityEngine;
using System.Collections;
public class CoroutineTest : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(TestCoroutine(ExampleCoroutine()));
    }
    public IEnumerator ExampleCoroutine()
    {
        Debug.Log("Start Coroutine");
        yield return null; // 한 프레임 대기
        Debug.Log("Middle Coroutine");
        yield return new WaitForSeconds(1f); // 1초 대기
        Debug.Log("End Coroutine");
    }

    public IEnumerator TestCoroutine(IEnumerator coroutine)
    {
        Debug.Log("Before yield");
        yield return coroutine.MoveNext();
        Debug.Log("After yield");
    }
}
