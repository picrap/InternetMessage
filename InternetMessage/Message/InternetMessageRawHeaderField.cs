using System.Collections.Generic;

namespace InternetMessage.Message
{
    public class InternetMessageRawHeaderField : InternetMessageHeaderField
    {
        public string RawBody { get; }

        public InternetMessageRawHeaderField(string name, IEnumerable<string> foldedRawBody)
            : base(name, foldedRawBody)
        {
            RawBody = string.Join("", FoldedRawBody);
        }
    }
}
