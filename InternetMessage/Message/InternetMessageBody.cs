using System.IO;

namespace InternetMessage.Message
{
    public class InternetMessageBody
    {
        private readonly TextReader _textReader;

        private string _body;
        public string Body => _body ??= _textReader.ReadToEnd();

        public InternetMessageBody(TextReader textReader)
        {
            _textReader = textReader;
        }
    }
}
