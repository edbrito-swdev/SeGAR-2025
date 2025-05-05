using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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

    public void ConnectToServer()
    {
        if (ipInputField != null)
        {
            if (manager != null)
            {
                manager.JoinGame(ipInputField.text);
                Debug.Log("Joined the game: " + ipInputField.text);
            }
            else
            {
                Debug.Log("Manager is null");
            }
        } else
            Debug.Log("Input field is null");
    }
}
