
using System.IO;

namespace InternetMessage.Reader
{
    public class PeekCharReader
    {
        private readonly TextReader _textReader;

        private char? _current;

        public PeekCharReader(TextReader textReader)
        {
            _textReader = textReader;
        }

        public char? Peek() => _current ??= ReadNext();

        public char? Read() => PopCurrent() ?? ReadNext();

        private char? ReadNext()
        {
            var ic = _textReader.Read();
            if (ic == -1)
                return null;
            return (char)ic;
        }

        private char? PopCurrent()
        {
            if (!_current.HasValue)
                return null;
            var c = _current.Value;
            _current = null;
            return c;
        }
    }
}
