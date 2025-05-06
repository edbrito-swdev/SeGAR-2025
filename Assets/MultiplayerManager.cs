using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    void Start()
    {

    }

    public string gameCode;
    
    public async Task<string> HostGame()
    {
        // 1. Create Relay allocation
        Allocation alloc = await RelayService.Instance.CreateAllocationAsync(4);
        string joinCode = await RelayService.Instance.GetJoinCodeAsync(alloc.AllocationId);
        Debug.Log("Relay Join Code: " + joinCode);
        gameCode = joinCode;
        
        // 2. Start Relay server
        var utp = NetworkManager.Singleton.GetComponent<UnityTransport>();
        utp.SetHostRelayData(alloc.RelayServer.IpV4, (ushort)alloc.RelayServer.Port, alloc.AllocationIdBytes, alloc.Key, alloc.ConnectionData);

        // 3. Start NGO host
        NetworkManager.Singleton.StartHost();

        // 4. Create lobby
        await LobbyService.Instance.CreateLobbyAsync("MyLobby", 4, new CreateLobbyOptions
        {
            IsPrivate = false,
            Data = new System.Collections.Generic.Dictionary<string, DataObject>
            {
                { "joinCode", new DataObject(DataObject.VisibilityOptions.Public, joinCode) }
            }
        });
        return gameCode;
    }

    public async Task<bool> JoinGame(string joinCode)
    {
        // 1. Join Relay using join code
        JoinAllocation joinAlloc = await RelayService.Instance.JoinAllocationAsync(joinCode);

        var utp = NetworkManager.Singleton.GetComponent<UnityTransport>();
        utp.SetClientRelayData(joinAlloc.RelayServer.IpV4, (ushort)joinAlloc.RelayServer.Port, joinAlloc.AllocationIdBytes, joinAlloc.Key, joinAlloc.ConnectionData, joinAlloc.HostConnectionData);

        // 2. Start NGO client
        return NetworkManager.Singleton.StartClient();
    }
}