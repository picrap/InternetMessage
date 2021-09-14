
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using InternetMessage.Reader;
using InternetMessage.Tokens;
using InternetMessage.Utility;

namespace InternetMessage.Message
{
    public class InternetMessageHttpHeaderField : InternetMessageHeaderField
    {
        public class KeyValue
        {
            public readonly Token Key, Value;

            public KeyValue(Token key, Token value)
            {
                Key = key;
                Value = value;
            }

            public KeyValue(IEnumerable<Token> keyValues)
            {
                var kva = keyValues.ToArray();
                if (kva.Length >= 1)
                    Key = kva[0];
                if (kva.Length >= 2)
                    Value = kva[1];
            }
        }

        public class KeyValuesList : List<KeyValue>
        {
            public IEnumerable<Token> Get(string key)
            {
                return this.Where(kv => string.Equals(kv.Key.Text, key, StringComparison.InvariantCultureIgnoreCase)).Select(kv => kv.Value);
            }

            public KeyValuesList(IEnumerable<KeyValue> keyValues)
                : base(keyValues)
            { }
        }

        public class OrderedList : List<KeyValuesList>
        {
            public IEnumerable<Token> Get(string key)
            {
                return this.SelectMany(l => l.Get(key));
            }

            public OrderedList(IEnumerable<KeyValuesList> keyValuesLists)
                : base(keyValuesLists)
            { }
        }

        public OrderedList Tokens { get; }

        public InternetMessageHttpHeaderField(string name, IEnumerable<string> foldedRawBody)
            : base(name, foldedRawBody)
        {
            var headerBodyReader = new InternetMessageHeaderBodyReader(new StringReader(string.Join("", FoldedRawBody)));
            Tokens = new OrderedList(headerBodyReader.ReadRawTokens().ChunkBy(tokenText: ",")
                .Select(kvl => new KeyValuesList(kvl.ChunkBy(tokenText: ";")
                    .Select(kvs => new KeyValue(kvs.SelectMany(k => k.Split("=")).ChunkBy(tokenText: "=", maximumChunks: 2).Select(kv => kv.Combine()))))));
        }
    }
}
