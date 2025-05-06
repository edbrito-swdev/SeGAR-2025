using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class ConnectToMultiplayer : MonoBehaviour
{
    public TMP_InputField ipInputField;
    public MultiplayerManager manager;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void ConnectToServer()
    {
        if (ipInputField != null)
        {
            if (manager != null)
            {
                bool conn = await manager.JoinGame(ipInputField.text);
                if (!conn)
                {
                    Debug.LogError("Failed to connect to server");
                    return;
                }
                Debug.Log("Joined the game: " + ipInputField.text);
                if (NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
                {
                    SceneManager.LoadScene("ARRelative", LoadSceneMode.Single);
                }            
            }
            else
            {
                Debug.Log("Manager is null");
            }
        } else
            Debug.Log("Input field is null");
    }
}
