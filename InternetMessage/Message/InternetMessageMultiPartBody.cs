

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

        public override string Body { get; }

        private readonly string _separator;
        private readonly InternetMessageFactory _factory;
        private string _newPart;
        private string _lastPart;
        private State _state = State.Start;
        private static readonly Dictionary<string, ICollection<InternetMessageHeaderField>> NoHeaderFields = new Dictionary<string, ICollection<InternetMessageHeaderField>>();

        public InternetMessageMultiPartBody(TextReader textReader, string separator, InternetMessageFactory factory = null)
            : base(textReader)
        {
            _separator = separator;
            _factory = factory ?? InternetMessageFactory.Raw;
            _newPart = "--" + separator;
            _lastPart = _newPart + "--";
        }

        public InternetMessageBody ReadBody()
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

        public IEnumerable<InternetMessageReader> ReadParts()
        {
            if (_state == State.Start)
                throw new InvalidOperationException("Body has not been read");
            if (_state == State.Parts)
                yield break;
            if (_state == State.End)
                throw new InvalidOperationException("Parts have already been read");

            var partBuilder = new StringBuilder();
            for (; ; )
            {
                var line = TextReader.ReadLine();
                if (line == _lastPart)
                {
                    _state = State.End;
                    yield return new InternetMessageReader(new StringReader(partBuilder.ToString()), _factory);
                    yield break;
                }
                if (line == _newPart)
                {
                    yield return new InternetMessageReader(new StringReader(partBuilder.ToString()), _factory);
                    partBuilder.Clear();
                }

                partBuilder.AppendLine(line);
            }
        }
    }
}
