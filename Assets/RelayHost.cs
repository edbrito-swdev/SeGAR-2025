using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Services.Authentication;
using Unity.Services.Core;


public class RelayHost : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
//     public async void StartHost()
//     {
//         await UnityServices.InitializeAsync();
//         await AuthenticationService.Instance.SignInAnonymouslyAsync();
//     
//         Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);
//         string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
//     
//         Debug.Log("Join Code: " + joinCode);
//     
//         var relayServerData = new RelayServerData(allocation, "dtls");
//         NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
//         NetworkManager.Singleton.StartHost();
//     }
}