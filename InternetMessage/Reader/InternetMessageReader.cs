using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InternetMessage.Reader
{
    public class InternetMessageReader
    {
        private readonly TextReader _textReader;

        private enum State
        {
            Start,
            Headers,
            Body,
            End
        }

        private State _state = State.Start;

        public InternetMessageReader(TextReader textReader)
        {
            _textReader = textReader;
        }

        public IEnumerable<InternetMessageHeader> ReadHeaders()
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
                        yield return new InternetMessageHeader(currentHeader);
                    _state = State.Body;
                    yield break;
                }

                if (!line.First().IsWsp())
                {
                    if (currentHeader.Count > 0)
                        yield return new InternetMessageHeader(currentHeader);
                    currentHeader = new List<string> { line };
                }
                else
                {
                    // append to current header (which is folded)
                    currentHeader.Add(line);
                }
            }
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
