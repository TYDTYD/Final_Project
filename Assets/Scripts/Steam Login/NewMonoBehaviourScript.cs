using UnityEngine;
using Steamworks;
public class NewMonoBehaviourScript : MonoBehaviour
{
    /*
    Callback<GetAuthSessionTicketResponse_t> m_AuthTicketResponseCallback;
    HAuthTicket m_AuthTicket;
    string m_SessionTicket;

    void SignInWithSteam()
    {
        // It's not necessary to add event handlers if they are 
        // already hooked up.
        // Callback.Create return value must be assigned to a 
        // member variable to prevent the GC from cleaning it up.
        // Create the callback to receive events when the session ticket
        // is ready to use in the web API.
        // See GetAuthSessionTicket document for details.
        m_AuthTicketResponseCallback = Callback<GetAuthSessionTicketResponse_t>.Create(OnAuthCallback);

        var buffer = new byte[1024];
        m_AuthTicket = SteamUser.GetAuthSessionTicket(buffer, buffer.Length, out var ticketSize);

        Array.Resize(ref buffer, (int)ticketSize);

        // The ticket is not ready yet, wait for OnAuthCallback.
        m_SessionTicket = BitConverter.ToString(buffer).Replace("-", string.Empty);
    }

    void OnAuthCallback(GetAuthSessionTicketResponse_t callback)
    {
        // Call Unity Authentication SDK to sign in or link with Steam.
        Debug.Log("Steam Login success. Session Ticket: " + m_SessionTicket);
    }*/

    void Start()
    {
        if (!SteamManager.Initialized)
        {
            Debug.LogError("Steam 초기화 실패!");
            return;
        }

        string steamUsername = SteamFriends.GetPersonaName();
        Debug.Log("로그인한 유저: " + steamUsername);
    }
}
