using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Steamworks;

[System.Serializable]
public class UserData
{
    public ulong id;
    public int high_score;
    public int stage_clear;
    public string record_time;
}

public class DataManagement : MonoBehaviour
{
    string apiUrl = "https://fastapi-cloudrun-951964224435.asia-northeast3.run.app/save";
    public void SaveUser(ulong steamID, int score, int level, string record)
    {
        StartCoroutine(SendDataToServer(steamID, score, level, record));
    }

    IEnumerator SendDataToServer(ulong steamID, int score, int level, string record)
    {
        UserData userData = new UserData
        {
            id = steamID,
            high_score = score,
            stage_clear = level,
            record_time = record
        };

        string jsonData = JsonUtility.ToJson(userData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("User data saved successfully.");
        }
        else
        {
            Debug.LogError($"Error saving user data: {request.error}");
        }
    }
}
