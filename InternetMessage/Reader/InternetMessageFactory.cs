using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InternetMessage.Message;

namespace InternetMessage.Reader
{
    public delegate InternetMessageBody TryCreateBody(TextReader textReader, IDictionary<string, ICollection<InternetMessageHeaderField>> headerFields);

    public class InternetMessageFactory
    {
        private readonly Dictionary<string, Type> _headerTypes =
            new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase);

        private readonly IList<TryCreateBody> _bodyTypes = new List<TryCreateBody>();

        public void Register<THeaderField>(string headerFieldName)
            where THeaderField : InternetMessageHeaderField
        {
            RegisterHeaderType(headerFieldName, typeof(THeaderField));
        }

        public void RegisterHeaderType(string headerFieldName, Type headerFieldType)
        {
            _headerTypes[headerFieldName] = headerFieldType;
        }

        public void RegisterBodyType(TryCreateBody bodyPicker)
        {
            _bodyTypes.Add(bodyPicker);
        }

        public InternetMessageHeaderField CreateHeaderField(string headerName, IEnumerable<string> foldedHeaderBody)
        {
            if (_headerTypes.TryGetValue(headerName, out var type))
                return (InternetMessageHeaderField)Activator.CreateInstance(type, headerName, foldedHeaderBody);
            return new InternetMessageRawHeaderField(headerName, foldedHeaderBody);
        }

        public InternetMessageBody CreateBody(TextReader textReader, IDictionary<string, ICollection<InternetMessageHeaderField>> headerFields)
        {
            return _bodyTypes.Select(b => b(textReader, headerFields)).FirstOrDefault(b => b is not null)
                   ?? new InternetMessageBody(textReader);
        }
    }
}
