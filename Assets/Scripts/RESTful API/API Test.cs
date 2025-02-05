using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
public class APITest : MonoBehaviour
{
    string apiUrl = "http://VM_IP";
    
    IEnumerator GetScore()
    {
        using(UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Score : " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error : " + request.error);
            }
        }
    }
    
    void Start()
    {
        StartCoroutine(GetScore());
    }
}
