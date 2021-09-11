using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace InternetMessage.Reader
{
    [DebuggerDisplay("{Name}: {Body[0]}")]
    public class InternetMessageHeader 
    {
        public string Name { get; }

        public ICollection<string> Body { get; }

        public InternetMessageHeader(IEnumerable<string> foldedHeader)
        {
            var body = foldedHeader.ToArray();
            var colonIndex = body[0].IndexOf(':');
            if (colonIndex < 0)
                throw new FormatException("Invalid header; first line has to be “name: body”");
            Name = body[0].Substring(0, colonIndex);
            body[0] = body[0].Substring(colonIndex + 1);
            Body = body;
        }
    }
}
