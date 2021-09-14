using System;

namespace InternetMessage.Encoding
{
    public class Base64Decoder : Decoder
    {
        public override string Name => "base64";
        public override byte[] Decode(string raw)
        {
            return Convert.FromBase64String(raw);
        }
    }
}
