
using System;
using System.Collections.Generic;
using System.Linq;

namespace InternetMessage.Encoding
{
    public abstract class Decoder
    {
        private static IDictionary<string, Decoder> KnownDecoders =
            new Decoder[] { new Base64Decoder() }.ToDictionary(d => d.Name, d => d, StringComparer.InvariantCultureIgnoreCase);

        public abstract string Name { get; }

        public abstract byte[] Decode(string raw);

        public static Decoder Find(string name)
        {
            KnownDecoders.TryGetValue(name, out var decoder);
            return decoder;
        }

        public static byte[] TryDecode(string name, string raw) => Find(name)?.Decode(raw);
    }
}
