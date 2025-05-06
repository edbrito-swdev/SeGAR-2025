
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
public class ReceiveImagesFromNetwork : MonoBehaviour
{
    public RawImage displayImage; // UI element to display the camera stream

    void Start()
    {
        NetworkManager.Singleton.CustomMessagingManager.RegisterNamedMessageHandler("CameraFrame",
            (senderClientId, reader) =>
            {
                //reader = reader; // do not use outside `using` block if possible

                if (!reader.TryBeginRead(sizeof(int))) {
                    Debug.LogError("Not enough data to read image length.");
                    return;
                }
                Debug.Log("Able to read image length");
                reader.ReadValueSafe(out int imageLength);
                Debug.Log("Received image length: " + imageLength);
                
                if (imageLength < 0 || imageLength > 5_000_000) { // 5MB sanity check
                    Debug.LogError($"Invalid image length: {imageLength}");
                    return;
                }

                if (!reader.TryBeginRead(imageLength)) {
                    Debug.LogError("Not enough data to read full image.");
                    return;
                }
                
                Debug.Log("Trying to read image...");
                byte[] jpegData = new byte[imageLength];
                reader.ReadBytesSafe(ref jpegData, imageLength);
                Debug.Log("Image read");
                ApplyImage(jpegData);
            });
    }

    void ApplyImage(byte[] data)
    {
        Texture2D tex = new Texture2D(2, 2); // size doesn't matter, will be replaced
        tex.LoadImage(data); // auto-resize
        Debug.Log("Displaying image");
        displayImage.texture = tex;
    }
    
}
