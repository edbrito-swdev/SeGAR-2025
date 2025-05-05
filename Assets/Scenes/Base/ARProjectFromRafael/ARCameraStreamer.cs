using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class ARCameraStreamer : NetworkBehaviour
{
    public Camera arCamera;
    public RenderTexture renderTexture;
    public float captureInterval = 0.2f; // 5 fps

    private Texture2D captureTexture;
    private float timer;

    void Start()
    {
        if (!IsOwner) return;

        captureTexture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        arCamera.targetTexture = renderTexture;
    }

    void Update()
    {
        if (!IsOwner) return;

        timer += Time.deltaTime;
        if (timer >= captureInterval)
        {
            timer = 0f;
            StartCoroutine(CaptureAndSendFrame());
        }
    }

    IEnumerator CaptureAndSendFrame()
    {
        yield return new WaitForEndOfFrame();

        RenderTexture.active = renderTexture;
        captureTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        captureTexture.Apply();

        byte[] jpegData = captureTexture.EncodeToJPG(50);
        SendFrameClientRpc(jpegData);
    }

    [ClientRpc]
    void SendFrameClientRpc(byte[] imageBytes)
    {
        // Only non-owner clients should receive
        if (IsOwner) return;

        //ARStreamReceiver.Instance?.OnImageReceived(imageBytes);
    }
}
