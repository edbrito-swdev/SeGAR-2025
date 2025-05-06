using System;
using Unity.Collections;
using Unity.Netcode;

[Serializable]
public struct ImageMessage
{
    public byte[] jpegData;

    public FastBufferWriter Serialize()
    {
        var writer = new FastBufferWriter(jpegData.Length + sizeof(int), Allocator.Temp);
        writer.WriteValueSafe(jpegData.Length);
        writer.WriteBytesSafe(jpegData);
        return writer;
    }

    public static ImageMessage Deserialize(FastBufferReader reader)
    {
        reader.ReadValueSafe(out int length);
        byte[] data = new byte[length];
        reader.ReadBytesSafe(ref data, length);
        return new ImageMessage { jpegData = data };
    }
}