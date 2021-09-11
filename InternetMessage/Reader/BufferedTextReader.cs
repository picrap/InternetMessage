using System.IO;

namespace InternetMessage.Reader
{
    /// <summary>
    /// A text reader, with the ability to peek a line
    /// </summary>
    public class BufferedTextReader
    {
        private readonly TextReader _textReader;
        
        private string _currentLine;

        public BufferedTextReader(TextReader textReader)
        {
            _textReader = textReader;
        }

        public string ReadLine()
        {
            if (_currentLine is null)
                return _textReader.ReadLine();
            return PopCurrentLine();
        }

        private string PopCurrentLine()
        {
            var currentLine = _currentLine;
            _currentLine = null;
            return currentLine;
        }

        public string PeekLine()
        {
            _currentLine ??= _textReader.ReadLine();
            return _currentLine;
        }

        public string ReadToEnd()
        {
            if (_currentLine is null)
                return _textReader.ReadToEnd();
            return PopCurrentLine() + _textReader.ReadToEnd();
        }
    }
}
