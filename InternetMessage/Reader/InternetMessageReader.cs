using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InternetMessage.Message;
using InternetMessage.Utility;

namespace InternetMessage.Reader;

public class InternetMessageReader
{
    private readonly TextReader _textReader;
    private readonly InternetMessageFactory _factory;

    private enum State
    {
        Start,
        Headers,
        Body,
        End
    }

    private State _state = State.Start;

    private readonly IDictionary<string, ICollection<InternetMessageHeaderField>> _readHeaderFields
        = new Dictionary<string, ICollection<InternetMessageHeaderField>>(StringComparer.InvariantCultureIgnoreCase);

    public InternetMessageReader(TextReader textReader, InternetMessageFactory factory = null)
    {
        _textReader = textReader;
        _factory = factory ?? InternetMessageFactory.Raw;
    }

    public IEnumerable<InternetMessageHeaderField> ReadHeaders()
    {
        foreach (var internetMessageHeaderField in DoReadHeaders())
        {
            if (!_readHeaderFields.TryGetValue(internetMessageHeaderField.Name, out var headerFields))
                _readHeaderFields[internetMessageHeaderField.Name] = headerFields = new List<InternetMessageHeaderField>();
            headerFields.Add(internetMessageHeaderField);
            yield return internetMessageHeaderField;
        }
    }

    public IDictionary<string, IList<InternetMessageHeaderField>> ReadAllHeaders() => ReadHeaders().ToHeaders();

    private IEnumerable<InternetMessageHeaderField> DoReadHeaders()
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

            if (!line.First().Is(CharacterType.Wsp))
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

    private static InternetMessageHeaderField CreateHeaderField(List<string> currentHeader, InternetMessageFactory factory)
    {
        var (headerName, foldedHeaderBody) = currentHeader.SplitFoldedHeaderField();
        return factory.CreateHeaderField(headerName, foldedHeaderBody);
    }

    public InternetMessageBody ReadBody()
    {
        if (_state == State.End)
            throw new InvalidOperationException("Wrong time to read body");
        if (_state < State.Body)
            _ = ReadHeaders().ToArray();
        _state = State.End;
        return _factory.CreateBody(_textReader, _readHeaderFields);
    }
}