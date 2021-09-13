using System;
using System.Collections.Generic;
using InternetMessage.Message;

namespace InternetMessage.Reader
{
    public class InternetMessageHeaderFactory
    {
        private Dictionary<string, Type> _types = new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase);

        public void Register<THeaderField>(string headerFieldName)
            where THeaderField : InternetMessageHeaderField
        {
            Register(headerFieldName,typeof(THeaderField));
        }

        public void Register(string headerFieldName, Type headerFieldType)
        {
            _types[headerFieldName] = headerFieldType;
        }

        public InternetMessageHeaderField CreateHeaderField(string headerName, IEnumerable<string> foldedHeaderBody)
        {
            if (_types.TryGetValue(headerName, out var type))
                return (InternetMessageHeaderField)Activator.CreateInstance(type, headerName, foldedHeaderBody);
            return new InternetMessageRawHeaderField(headerName, foldedHeaderBody);
        }
    }
}
