using TMPro;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class UGSBootstrap : MonoBehaviour
{
    public TMP_Text hostId;
    public TMP_Text clientIdText;
    async void Awake()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Signed in: " + AuthenticationService.Instance.PlayerId);
            hostId.text = NetworkManager.Singleton.LocalClientId.ToString();
            NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += HandleClientDisconnected;
        }
    }

    private void OnDisable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnected;
    }
    
    private void HandleClientConnected(ulong clientId)
    {
        Debug.Log($"Client connected: {clientId}");
        clientIdText.text = clientId.ToString();
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            Debug.Log("This is the local client.");
        }
        else if (NetworkManager.Singleton.IsServer)
        {
            Debug.Log("Another client joined my host/server.");
        }

        ListConnectedClients();
    }

    private void ListConnectedClients()
    {
        foreach (var client in NetworkManager.Singleton.ConnectedClients)
        {
            Debug.Log("Connected Client ID: " + client.Key);
        }
    }

    private void HandleClientDisconnected(ulong clientId)
    {
        Debug.Log($"Client disconnected: {clientId}");
    }    
}