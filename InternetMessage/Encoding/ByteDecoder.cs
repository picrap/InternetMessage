using System.Linq;

namespace InternetMessage.Encoding
{
    public class ByteDecoder : Decoder
    {
        public override string[] TransferEncodingNames { get; } = new[] { "7bit", "8bit", "binary" };
        public override string[] StringEncodingNames { get; } = new string[0];

        public override byte[] Decode(string encodedString)
        {
            return encodedString.Cast<byte>().ToArray();
        }
    }
}
