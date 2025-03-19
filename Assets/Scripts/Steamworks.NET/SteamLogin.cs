using UnityEngine;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

public class SteamLogin : MonoBehaviour
{
    string apiUrl = "https://your-fastapi-server.com/steam-auth";
    Callback<GetTicketForWebApiResponse_t> m_AuthTicketForWebApiResponseCallback;
    string m_SessionTicket;
    string identity = "unityauthenticationservice";

    private void Start()
    {
        if (SteamManager.Initialized)
        {
            SignInWithSteam();
        }
        else
        {
            Debug.Log("Steam is not initialized!");
        }
    }

    void SignInWithSteam()
    {
        // It's not necessary to add event handlers if they are 
        // already hooked up.
        // Callback.Create return value must be assigned to a 
        // member variable to prevent the GC from cleaning it up.
        // Create the callback to receive events when the session ticket
        // is ready to use in the web API.
        // See GetAuthSessionTicket document for details.
        m_AuthTicketForWebApiResponseCallback = Callback<GetTicketForWebApiResponse_t>.Create(OnAuthCallback);

        SteamUser.GetAuthTicketForWebApi(identity);
    }

    void OnAuthCallback(GetTicketForWebApiResponse_t callback)
    {
        m_SessionTicket = BitConverter.ToString(callback.m_rgubTicket).Replace("-", string.Empty);
        m_AuthTicketForWebApiResponseCallback.Dispose();
        m_AuthTicketForWebApiResponseCallback = null;
        Debug.Log("Steam Login success. Session Ticket: " + m_SessionTicket);
        // Call Unity Authentication SDK to sign in or link with Steam, displayed in the following examples, using the same identity string and the m_SessionTicket.
        

        
        //GetUserInfomation();
        //GetUserFriendList();
    }

    IEnumerator SendSessionTicketToServer(string ticket)
    {
        WWWForm form = new WWWForm();
        form.AddField("session_ticket", ticket);

        using (UnityWebRequest www = UnityWebRequest.Post(apiUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Debug.Log("Server Response: " + www.downloadHandler.text);
            }
        }
    }

    IEnumerator GetUserData(string steamId)
    {
        string apiUrl = "https://your-fastapi-server.com/get-user-data?steam_id=" + steamId;

        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Debug.Log("User Data: " + www.downloadHandler.text);
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