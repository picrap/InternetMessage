using System.Collections.Generic;
using System.IO;
using System.Linq;
using InternetMessage.Encoding;
using InternetMessage.Reader;

namespace InternetMessage.Message
{
    public class InternetMessageBody
    {
        private static readonly InternetMessageReader[] NoInternetMessageReaders = new InternetMessageReader[0];
        protected readonly TextReader TextReader;
        private readonly string _contentTransferEncoding;

        public InternetMessageBody(TextReader textReader, IDictionary<string, ICollection<InternetMessageHeaderField>> headerFields)
        {
            TextReader = textReader;
            if (headerFields.TryGetValue("content-transfer-encoding", out var transferEncoding))
                _contentTransferEncoding = transferEncoding.First().To<InternetMessageHttpHeaderField>().Tokens.SingleValue().Text;
        }

        public Stream ReadDecodedBody()
        {
            return new MemoryStream(Decoder.TryDecodeTransfer(_contentTransferEncoding, ReadRawBody()));
        }

        public virtual string ReadRawBody() => TextReader.ReadToEnd();

        public virtual InternetMessageBody ReadBody() => this;

        public virtual IEnumerable<InternetMessageReader> ReadParts() => NoInternetMessageReaders;
    }
}
