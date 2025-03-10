using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public partial class GameManager : MonoBehaviour
{
    Dictionary<int, string> SceneIndex = new Dictionary<int, string>(); 
    float realStartTime = 0f;

    string[] scenesToLoad = { "Title", "Stage 1", "Stage 2", "Stage 3", "Stage 4", "Stage Rest", "Game Over", "Setting", "Statistic" };
    
    void CacheScenes(string[] sceneNames)
    {
        for (int i = 0; i < sceneNames.Length; i++)
        {
            string sceneName = sceneNames[i];
            SceneIndex.Add(i, sceneName);
        }
    }
    public IEnumerator PreloadScene(int index, IEnumerator coroutine)
    {
        startTime = Time.realtimeSinceStartup;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneIndex[index]);
        asyncLoad.allowSceneActivation = false;
        yield return coroutine;
        realStartTime = Time.realtimeSinceStartup;
        yield return new WaitUntil(() => asyncLoad.progress >= 0.9f);
        asyncLoad.allowSceneActivation = true;
    }
    public IEnumerator PreloadScene(string sceneName, IEnumerator coroutine)
    {
        startTime = Time.realtimeSinceStartup;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        yield return coroutine;
        realStartTime = Time.realtimeSinceStartup;
        yield return new WaitUntil(() => asyncLoad.progress >= 0.9f);
        asyncLoad.allowSceneActivation = true;
    }
}