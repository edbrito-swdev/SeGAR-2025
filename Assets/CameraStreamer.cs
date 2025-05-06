using System.Linq;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class CameraStreamer : MonoBehaviour
{
    public RenderTexture renderTexture;
    public float sendRate = 0.1f; // send every 0.1 seconds (10 FPS)
    public int quality = 10;
    
    private Texture2D tempTexture;
    private float timer;

    void Start()
    {
        tempTexture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
    }

    void Update()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            Debug.Log("This is the server side.");
            timer += Time.deltaTime;
            if (timer >= sendRate)
            {
                timer = 0f;
                SendFrame();
            }            
        }
        else
        {
            Debug.LogWarning("This is not the server");
        }
    }

    void SendFrame()
    {
        RenderTexture.active = renderTexture;
        tempTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tempTexture.Apply();
        RenderTexture.active = null;

        byte[] imageData = tempTexture.EncodeToJPG(quality); // Compress to JPEG

        // Send using NGO Custom Message
        FastBufferWriter writer = new FastBufferWriter(imageData.Length+sizeof(int), Allocator.Temp);
        var idToSend = NetworkManager.Singleton.ConnectedClientsList.Last().ClientId;
        int totalLength = imageData.Length + sizeof(int);
        if (writer.TryBeginWrite(imageData.Length + sizeof(int)))
        {
            writer.WriteValueSafe(imageData.Length);
            writer.WriteBytesSafe(imageData);
            Debug.Log("Number of bytes: " + imageData.Length);
            NetworkManager.Singleton.CustomMessagingManager.SendNamedMessage(
                "CameraFrame",
                idToSend,
                writer,
                NetworkDelivery.ReliableFragmentedSequenced
            );
        }
        else
        {
            Debug.LogWarning("Not enough space in buffer to write JPEG frame. Size: " + totalLength);
        }

    }
}