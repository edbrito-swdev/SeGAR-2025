using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HostMultiplayer : MonoBehaviour
{
    public MultiplayerManager manager;
    public TMP_Text hostText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void HostGame()
    {
        if (manager == null)
            Debug.Log("You need to set the MultiplayerManager");
        else Debug.Log("Hosting the game");
        string joinCode = await manager.HostGame();
        hostText.text = joinCode;
    }
}
