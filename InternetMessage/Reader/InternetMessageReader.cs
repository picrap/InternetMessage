using System.Collections.Generic;
using System.IO;

namespace InternetMessage.Reader
{
    public class InternetMessageReader
    {
        private readonly TextReader _textReader;


        private InternetMessageNode _node;
        public InternetMessageNode Node
        {
            get { return _node; }
            private set
            {
                _node = value;
                NodeType = _node?.Type ?? InternetMessageNodeType.End;
            }
        }

        public InternetMessageNodeType NodeType { get; private set; } = InternetMessageNodeType.Start;

        public InternetMessageReader(TextReader textReader)
        {
            _textReader = textReader;
        }

        public bool ReadNext()
        {
            // --- end is already reached
            if (NodeType == InternetMessageNodeType.End)
                return false;

            // --- last time we read the body, so this is goodbye
            if (NodeType == InternetMessageNodeType.Body)
            {
                Node = null;
                NodeType = InternetMessageNodeType.End;
                return false;
            }

            // --- we are currently reading headers, but this may change
            var line = _textReader.ReadLine();
            if (string.IsNullOrEmpty(line)) // (RFC 5322 § 3.5)
            {
                Node = new InternetMessageBody(_textReader.ReadToEnd());
                return true;
            }

            var lines = new List<string> { line };
            for (;;)
            {
            }
        }
    }
}
