using UnityEngine;
using Steamworks;
using System.Collections;
using System;
using UnityEngine.Networking;

[System.Serializable]
public class SessionTicketData
{
    public string session_ticket;
    public string identity;
}
public class SteamLogin : MonoBehaviour
{
    public readonly static string url = "https://fastapi-cloudrun-951964224435.asia-northeast3.run.app";
    readonly string apiUrl = "https://fastapi-cloudrun-951964224435.asia-northeast3.run.app/steam-auth";
    Callback<GetTicketForWebApiResponse_t> m_AuthTicketForWebApiResponseCallback;
    string m_SessionTicket;
    string identity;
    [SerializeField] GameObject pressEnter;
    [SerializeField] GameObject info;
    [SerializeField] MaskVariation GetMask;

    private void Start()
    {
        StartCoroutine(GetMask.Brighter());
        if (GameManager.Instance.initialized)
        {
            info.SetActive(false);
            pressEnter.SetActive(true);
            return;
        }
        if (SteamManager.Initialized)
        {
            SignInWithSteam();
            GameManager.Instance.initialized = true;
        }
        else
        {
            Debug.Log("Steam is not initialized!");
        }
    }

    void SignInWithSteam()
    {
        m_AuthTicketForWebApiResponseCallback = Callback<GetTicketForWebApiResponse_t>.Create(OnAuthCallback);
        identity = SteamUser.GetSteamID().ToString();
        SteamUser.GetAuthTicketForWebApi(identity);
    }

    void OnAuthCallback(GetTicketForWebApiResponse_t callback)
    {
        m_SessionTicket = BitConverter.ToString(callback.m_rgubTicket).Replace("-", string.Empty);
        m_AuthTicketForWebApiResponseCallback.Dispose();
        m_AuthTicketForWebApiResponseCallback = null;
        Debug.Log("Steam Login success. Session Ticket: " + m_SessionTicket);
        // Call Unity Authentication SDK to sign in or link with Steam, displayed in the following examples, using the same identity string and the m_SessionTicket.
        StartCoroutine(SendSessionTicketToServer(m_SessionTicket));
    }

    IEnumerator SendSessionTicketToServer(string ticket)
    {
        SessionTicketData userData = new SessionTicketData();
        userData.session_ticket = ticket;
        userData.identity = identity;
        string jsonData = JsonUtility.ToJson(userData);
        using (UnityWebRequest www = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json"); // JSON 데이터 전송을 위한 헤더 설정
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
                Debug.LogError("Response Code: " + www.responseCode);
                Debug.LogError("Response: " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Server Response: " + www.downloadHandler.text);
                info.SetActive(false);
                pressEnter.SetActive(true);
            }
        }
    }

    void GetUserInfomation()
    {
        string playerName = SteamFriends.GetPersonaName();
        Debug.Log("Steam Player Name : " + playerName);

        ulong steamID = SteamUser.GetSteamID().m_SteamID;
        Debug.Log("Steam ID: " + steamID);

        EPersonaState playerState = SteamFriends.GetPersonaState();
        Debug.Log("Steam Player State: " + playerState);
    }

    void GetUserFriendList()
    {
        int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
        for (int i = 0; i < friendCount; i++)
        {
            CSteamID friendSteamID = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate);
            string friendName = SteamFriends.GetFriendPersonaName(friendSteamID);
            EPersonaState friendState = SteamFriends.GetFriendPersonaState(friendSteamID);

            Debug.Log($"Friend {i + 1}: {friendName} (State: {friendState})");
        }
    }

    Texture2D GetSteamAvatar()
    {
        CSteamID steamID = SteamUser.GetSteamID();
        int avatarInt = SteamFriends.GetLargeFriendAvatar(steamID); // Large Avatar
        if (avatarInt == -1) return null;

        uint width, height;
        if (SteamUtils.GetImageSize(avatarInt, out width, out height))
        {
            byte[] avatarBuffer = new byte[4 * (int)width * (int)height];
            if (SteamUtils.GetImageRGBA(avatarInt, avatarBuffer, avatarBuffer.Length))
            {
                Texture2D avatarTexture = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false);
                avatarTexture.LoadRawTextureData(avatarBuffer);
                avatarTexture.Apply();
                return avatarTexture;
            }
        }
        return null;
    }
}