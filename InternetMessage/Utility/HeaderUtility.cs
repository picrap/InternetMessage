
using System;
using System.Collections.Generic;
using System.Linq;

namespace InternetMessage.Utility
{
    public static class HeaderUtility
    {
        public static (string Name, string Body) SplitHeaderField(this string literalHeaderField)
        {
            var colonIndex = literalHeaderField.IndexOf(':');
            if (colonIndex < 0)
                throw new FormatException("Invalid header; first line has to be “name: body”");
            var name = literalHeaderField.Substring(0, colonIndex);
            var body = literalHeaderField.Substring(colonIndex + 1);
            return (name, body);
        }

        public static (string Name, string[] Body) SplitFoldedHeaderField(this IEnumerable<string> foldedHeaderField)
        {
            var foldedRawBody = foldedHeaderField.ToArray();
            var (name, body) = foldedRawBody[0].SplitHeaderField();
            foldedRawBody[0] = body;
            return (name, foldedRawBody);
        }
    }
}
