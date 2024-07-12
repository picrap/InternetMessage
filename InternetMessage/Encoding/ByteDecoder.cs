
using System;

namespace InternetMessage.Encoding;

public class ByteDecoder : Decoder
{
    public override string[] TransferEncodingNames { get; } = new[] { "7bit", "8bit", "binary" };
    public override string[] StringEncodingNames { get; } = Array.Empty<string>();

    public override byte[] Decode(string encodedString)
    {
        var bytes = new byte[encodedString.Length];
        for (int i = 0; i < encodedString.Length; i++)
        {
            var c = encodedString[i];
            if (c > 0xFF)
                c = '?';
            bytes[i] = (byte)c;
        }
        return bytes;
    }
}
