using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace InternetMessage.Message
{
    [DebuggerDisplay("{Name}: {Body[0]}")]
    public abstract class InternetMessageHeaderField
    {
        public string Name { get; }

        public ICollection<string> FoldedRawBody { get; }

        public InternetMessageHeaderField(string name, IEnumerable<string> foldedRawBody)
        {
            Name = name;
            FoldedRawBody = foldedRawBody.ToArray();
        }
    }
}
