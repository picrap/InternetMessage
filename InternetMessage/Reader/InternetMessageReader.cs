using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InternetMessage.Message;
using InternetMessage.Utility;

namespace InternetMessage.Reader
{
    public class InternetMessageReader
    {
        private readonly TextReader _textReader;
        private readonly InternetMessageHeaderFactory _factory;

        private enum State
        {
            Start,
            Headers,
            Body,
            End
        }

        private State _state = State.Start;

        public InternetMessageReader(TextReader textReader, InternetMessageHeaderFactory factory = null)
        {
            _textReader = textReader;
            _factory = factory;
        }

        public IEnumerable<InternetMessageHeaderField> ReadHeaders()
        {
            if (_state >= State.Body)
                throw new InvalidOperationException("Headers have already been read");
            _state = State.Headers;
            var currentHeader = new List<string>();
            for (; ; )
            {
                var line = _textReader.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    if (currentHeader.Count > 0)
                        yield return CreateHeaderField(currentHeader, _factory);
                    _state = State.Body;
                    yield break;
                }

                if (!line.First().IsWsp())
                {
                    if (currentHeader.Count > 0)
                        yield return CreateHeaderField(currentHeader, _factory);
                    currentHeader = new List<string> { line };
                }
                else
                {
                    // append to current header (which is folded)
                    currentHeader.Add(line);
                }
            }
        }

        private static InternetMessageHeaderField CreateHeaderField(List<string> currentHeader, InternetMessageHeaderFactory factory)
        {
            var (headerName, foldedHeaderBody) = currentHeader.SplitFoldedHeaderField();
            if (factory is null)
                return new InternetMessageRawHeaderField(headerName,foldedHeaderBody);
            return factory.CreateHeaderField(headerName, foldedHeaderBody);
        }

        public InternetMessageBody ReadBody()
        {
            if (_state != State.Body)
                throw new InvalidOperationException("Wrong time to read body");
            _state = State.End;
            return new InternetMessageBody(_textReader.ReadToEnd());
        }
    }
}
