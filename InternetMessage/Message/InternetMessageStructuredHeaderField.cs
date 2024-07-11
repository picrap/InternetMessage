using System.Collections.Generic;
using System.IO;
using System.Linq;
using InternetMessage.Reader;
using InternetMessage.Tokens;

namespace InternetMessage.Message;

public class InternetMessageStructuredHeaderField : InternetMessageHeaderField
{
    public IEnumerable<Token> Tokens { get; }

    public InternetMessageStructuredHeaderField(string name, IEnumerable<string> foldedRawBody)
        : base(name, foldedRawBody)
    {
        var headerBodyReader = new InternetMessageHeaderBodyReader(new StringReader(string.Join("", FoldedRawBody)));
        Tokens = headerBodyReader.ReadTokens().ToArray();
    }
}