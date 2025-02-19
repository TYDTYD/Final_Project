using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public partial class GameManager : MonoBehaviour
{
    Dictionary<string, AsyncOperation> LoadCache = new Dictionary<string, AsyncOperation>();
    Dictionary<int, string> SceneIndex = new Dictionary<int, string>();

    string[] scenesToLoad = { "Title", "Stage 1", "Stage 2", "Stage 3", "Stage 4", "Stage Rest", "Game Over", "Setting", "Statistic" };
    
    void CacheScenes(string[] sceneNames)
    {
        for (int i = 0; i < sceneNames.Length; i++)
        {
            string sceneName = sceneNames[i];
            SceneIndex.Add(i, sceneName);
            LoadCache.Add(sceneName, null);
        }
    }

    public IEnumerator PreloadScene(int index)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneIndex[index]);
        LoadCache[SceneIndex[index]] = asyncLoad;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public IEnumerator PreloadScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        LoadCache[sceneName] = asyncLoad;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}