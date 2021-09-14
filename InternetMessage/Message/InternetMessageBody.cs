using System.Collections.Generic;
using System.IO;
using InternetMessage.Reader;

namespace InternetMessage.Message
{
    public class InternetMessageBody
    {
        private static readonly InternetMessageReader[] NoInternetMessageReaders = new InternetMessageReader[0];
        protected readonly TextReader TextReader;

        public InternetMessageBody(TextReader textReader, IDictionary<string, ICollection<InternetMessageHeaderField>> headerFields)
        {
            TextReader = textReader;
        }

        public virtual string ReadRawBody() => TextReader.ReadToEnd();

        public virtual InternetMessageBody ReadBody() => this;

        public virtual IEnumerable<InternetMessageReader> ReadParts() => NoInternetMessageReaders;
    }
}
