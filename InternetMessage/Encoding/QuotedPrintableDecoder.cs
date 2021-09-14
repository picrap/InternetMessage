using System;

namespace InternetMessage.Encoding
{
    public class QuotedPrintableDecoder : Decoder
    {
        public override string[] TransferEncodingNames { get; } = new[] { "quoted-printable" };
        public override string[] StringEncodingNames { get; } = new[] { "q" };

        public override byte[] Decode(string encodedString)
        {
            // yeah, some day
            throw new NotImplementedException();
        }
    }
}
