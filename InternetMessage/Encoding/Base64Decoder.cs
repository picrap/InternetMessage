using System;

namespace InternetMessage.Encoding;

public class Base64Decoder : Decoder
{
    public override string[] TransferEncodingNames { get; } = new[] { "base64" };
    public override string[] StringEncodingNames { get; } = new[] { "b" };

    public override byte[] Decode(string encodedString)
    {
        return Convert.FromBase64String(encodedString);
    }
}