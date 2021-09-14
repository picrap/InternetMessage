

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using InternetMessage.Reader;

namespace InternetMessage.Message
{
    public class InternetMessageMultiPartBody : InternetMessageBody
    {
        private enum State
        {
            Start,
            Body,
            Parts,
            End
        }

        private readonly string _separator;
        private readonly InternetMessageFactory _factory;
        private readonly string _newPart;
        private readonly string _lastPart;
        private State _state = State.Start;
        private static readonly Dictionary<string, ICollection<InternetMessageHeaderField>> NoHeaderFields = new Dictionary<string, ICollection<InternetMessageHeaderField>>();

        public InternetMessageMultiPartBody(TextReader textReader, IDictionary<string, ICollection<InternetMessageHeaderField>> headerFields, string separator, InternetMessageFactory factory = null)
            : base(textReader, headerFields)
        {
            _separator = separator;
            _factory = factory ?? InternetMessageFactory.Raw;
            _newPart = "--" + separator;
            _lastPart = _newPart + "--";
        }

        public override string ReadRawBody()
        {
            if (_state != State.Start)
                throw new InvalidOperationException("Body has already been read");
            _state = State.End;
            return TextReader.ReadToEnd();
        }

        public override InternetMessageBody ReadBody()
        {
            if (_state != State.Start)
                throw new InvalidOperationException("Body has already been read");
            var partBuilder = new StringBuilder();
            for (; ; )
            {
                var line = TextReader.ReadLine();
                if (line == _newPart)
                {
                    _state = State.Body;
                    return _factory.CreateBody(new StringReader(partBuilder.ToString()), NoHeaderFields);
                }
                if (line == _lastPart)
                {
                    _state = State.Parts;
                    return _factory.CreateBody(new StringReader(partBuilder.ToString()), NoHeaderFields);
                }

                partBuilder.AppendLine(line);
            }
        }

        public override IEnumerable<InternetMessageReader> ReadParts()
        {
            if (_state == State.Start)
                ReadBody();
            if (_state == State.End)
                throw new InvalidOperationException("Parts have already been read");

            var partBuilder = new StringBuilder();
            string lastLine = null;
            for (; ; )
            {
                var line = TextReader.ReadLine();
                if (line == _lastPart)
                {
                    _state = State.End;
                    if (!string.IsNullOrEmpty(lastLine))
                        partBuilder.AppendLine(lastLine);
                    yield return new InternetMessageReader(new StringReader(partBuilder.ToString()), _factory);
                    yield break;
                }
                if (line == _newPart)
                {
                    if (!string.IsNullOrEmpty(lastLine))
                        partBuilder.AppendLine(lastLine);
                    lastLine = null;
                    yield return new InternetMessageReader(new StringReader(partBuilder.ToString()), _factory);
                    partBuilder.Clear();
                    continue;
                }

                if (lastLine is not null)
                    partBuilder.AppendLine(lastLine);
                lastLine = line;
            }
        }
    }
}
