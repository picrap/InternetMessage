using System.IO;

namespace InternetMessage.Message
{
    public class InternetMessageBody
    {
        protected readonly TextReader TextReader;

        private string _body;
        public virtual string Body => _body ??= TextReader.ReadToEnd();

        public InternetMessageBody(TextReader textReader)
        {
            TextReader = textReader;
        }
    }
}
