
using System;

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
            return parts[2] switch
            {
                "B" => DecodeBase64(parts[1], parts[3]),
                "Q" => DecodeQuotedPrintable(parts[1], parts[3]),
                _ => throw new InvalidOperationException()
            };
        }

        private static string DecodeQuotedPrintable(string charset, string encodedText)
        {
            throw new NotImplementedException();
        }

        private static string DecodeBase64(string charset, string encodedText)
        {
            var bytes = Convert.FromBase64String(encodedText);
            var encoding = System.Text.Encoding.GetEncoding(charset);
            return encoding.GetString(bytes);
        }
    }
}
