using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameServer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            SceneManager.LoadScene("ARPatient", LoadSceneMode.Single);
        }          
    }
}
