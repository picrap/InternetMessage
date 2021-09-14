using System.Collections.Generic;
using System.IO;
using InternetMessage.Reader;

namespace InternetMessage.Message
{
    public class InternetMessageBody
    {
        private static readonly InternetMessageReader[] NoInternetMessageReaders = new InternetMessageReader[0];
        protected readonly TextReader TextReader;

        public InternetMessageBody(TextReader textReader)
        {
            TextReader = textReader;
        }

        public virtual string ReadBody() => TextReader.ReadToEnd();

        public virtual IEnumerable<InternetMessageReader> ReadParts()
        {
            return NoInternetMessageReaders;
        }
    }
}
