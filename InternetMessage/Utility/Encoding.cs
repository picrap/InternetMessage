
using System;
using InternetMessage.Encoding;

namespace InternetMessage.Utility
{
    public static class Encoding
    {
        public static string Decode(this string s)
        {
            if (!s.StartsWith("=?") || !s.EndsWith("?="))
                return s;
            // format is “=?«charset»?«encoding»?«encoded-text»?=”
            var parts = s.Split('?');
            if (parts.Length != 5)
                return s;
            return Decoder.TryDecodeString(parts[2], parts[3], parts[1]);
        }
    }
}
