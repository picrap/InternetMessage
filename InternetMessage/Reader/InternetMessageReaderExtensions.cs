using System;
using System.Collections.Generic;

namespace InternetMessage.Reader
{
    public static class InternetMessageReaderExtensions
    {
        public static IEnumerable<InternetMessageHeader> ReadHeaders(this InternetMessageReader internetMessageReader)
        {
            if (internetMessageReader.NodeType != InternetMessageNodeType.Start)
                throw new InvalidOperationException("Too late for that: this can only be invoked before any other read");
            for (; ; )
            {
                if (!internetMessageReader.ReadNext())
                    yield break;
                if (internetMessageReader.NodeType != InternetMessageNodeType.Header)
                    yield break;
                yield return (InternetMessageHeader)internetMessageReader.Node;
            }
        }
    }
}