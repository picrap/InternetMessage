using System.Collections.Generic;

namespace InternetMessage.Message
{
    public class InternetMessageUnstructuredHeaderField : InternetMessageHeaderField
    {
        public string RawBody { get; }

        public InternetMessageUnstructuredHeaderField(string name, IEnumerable<string> foldedRawBody)
            : base(name, foldedRawBody)
        {
            RawBody = string.Join("", FoldedRawBody);
        }
    }
}
