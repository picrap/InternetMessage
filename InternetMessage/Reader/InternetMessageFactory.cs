using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InternetMessage.Message;
using InternetMessage.Utility;

namespace InternetMessage.Reader
{
    public delegate InternetMessageBody TryCreateBody(TextReader textReader, IDictionary<string, ICollection<InternetMessageHeaderField>> headerFields);

    public class InternetMessageFactory
    {
        private readonly Dictionary<string, Type> _headerTypes = new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase);

        private readonly IList<TryCreateBody> _bodyTypes = new List<TryCreateBody>();

        public static InternetMessageFactory Raw = new();
        public static InternetMessageFactory Full = CreateFull();

        private static InternetMessageFactory CreateFull()
        {
            InternetMessageFactory internetMessageFactory = new();
            internetMessageFactory.RegisterBodyType(delegate (TextReader textReader, IDictionary<string, ICollection<InternetMessageHeaderField>> headerFields)
            {
                var separator = headerFields.GetMultipartBoundary();
                if (separator is null)
                    return null; // pass
                return new InternetMessageMultiPartBody(textReader, headerFields, separator, internetMessageFactory);
            });
            return internetMessageFactory;
        }

        public void RegisterHeaderType<THeaderField>(string headerFieldName)
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
                return CreateInternetMessageHeaderField(headerName, foldedHeaderBody, type);
            return new InternetMessageUnstructuredHeaderField(headerName, foldedHeaderBody);
        }

        public static TInternetMessageHeaderField CreateInternetMessageHeaderField<TInternetMessageHeaderField>(string headerName, IEnumerable<string> foldedHeaderBody)
        where TInternetMessageHeaderField : InternetMessageHeaderField
        {
            return (TInternetMessageHeaderField)CreateInternetMessageHeaderField(headerName, foldedHeaderBody, typeof(TInternetMessageHeaderField));
        }

        public static InternetMessageHeaderField CreateInternetMessageHeaderField(string headerName, IEnumerable<string> foldedHeaderBody, Type type)
        {
            return (InternetMessageHeaderField)Activator.CreateInstance(type, new object[] { headerName, foldedHeaderBody });
        }

        public InternetMessageBody CreateBody(TextReader textReader, IDictionary<string, ICollection<InternetMessageHeaderField>> headerFields)
        {
            return _bodyTypes.Select(b => b(textReader, headerFields)).FirstOrDefault(b => b is not null)
                   ?? new InternetMessageBody(textReader, headerFields);
        }
    }
}
