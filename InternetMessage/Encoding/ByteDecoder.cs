
using System;

namespace InternetMessage.Encoding;

public class ByteDecoder : Decoder
{
    public override string[] TransferEncodingNames { get; } = new[] { "7bit", "8bit", "binary" };
    public override string[] StringEncodingNames { get; } = new string[0];

    public override byte[] Decode(string encodedString)
    {
        var bytes = new byte[encodedString.Length];
        for (int i = 0; i < encodedString.Length; i++)
        {
            var c = encodedString[i];
            if (c >= 0xFF)
                throw new FormatException($"Invalid character ({(int)c}) at index {i}");
            bytes[i] = (byte)c;
        }
        return bytes;
    }
}