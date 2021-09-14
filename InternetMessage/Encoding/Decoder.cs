
using System;
using System.Collections.Generic;
using System.Linq;

namespace InternetMessage.Encoding
{
    public abstract class Decoder
    {
        private static readonly IDictionary<string, Decoder> KnownTransferDecoders =
            new Decoder[] { new Base64Decoder() }.ToDictionary(d => d.TransferEncodingName, d => d, StringComparer.InvariantCultureIgnoreCase);

        private static readonly IDictionary<string, Decoder> KnownStringDecoders =
            new Decoder[] { new Base64Decoder() }.ToDictionary(d => d.StringEncodingName, d => d, StringComparer.InvariantCultureIgnoreCase);

        public abstract string TransferEncodingName { get; }
        public abstract string StringEncodingName { get; }

        public abstract byte[] Decode(string encodedString);

        public static Decoder FindTransferDecoder(string transferEncodingName)
        {
            KnownTransferDecoders.TryGetValue(transferEncodingName, out var decoder);
            return decoder;
        }

        public static Decoder FindStringDecoder(string stringEncodingName)
        {
            KnownStringDecoders.TryGetValue(stringEncodingName, out var decoder);
            return decoder;
        }

        public static byte[] TryDecodeTransfer(string transferEncodingName, string encodedString) => FindTransferDecoder(transferEncodingName)?.Decode(encodedString);
        
        public static string TryDecodeString(string stringEncodingName, string encodedString, string charset)
        {
            var bytes= FindStringDecoder(stringEncodingName)?.Decode(encodedString);
            var encoding = System.Text.Encoding.GetEncoding(charset);
            return encoding.GetString(bytes);
        }
    }
}
