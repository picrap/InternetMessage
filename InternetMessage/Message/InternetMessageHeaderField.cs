using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using InternetMessage.Reader;

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

        public TInternetMessageHeaderField To<TInternetMessageHeaderField>()
            where TInternetMessageHeaderField : InternetMessageHeaderField
        {
            if (this is TInternetMessageHeaderField internetMessageHeaderField)
                return internetMessageHeaderField;
            return InternetMessageFactory.CreateInternetMessageHeaderField<TInternetMessageHeaderField>(Name, FoldedRawBody);
        }
    }
}
