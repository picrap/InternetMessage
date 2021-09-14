using System;

namespace InternetMessage.Encoding
{
    public class Base64Decoder : Decoder
    {
        public override string TransferEncodingName => "base64";
        public override string StringEncodingName => "b";

        public override byte[] Decode(string encodedString)
        {
            return Convert.FromBase64String(encodedString);
        }
    }
}
