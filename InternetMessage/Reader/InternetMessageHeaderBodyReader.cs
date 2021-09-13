
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using InternetMessage.Tokens;
using InternetMessage.Utility;

namespace InternetMessage.Reader
{
    public class InternetMessageHeaderBodyReader
    {
        private readonly PeekCharReader _charReader;

        public InternetMessageHeaderBodyReader(TextReader textReader)
        {
            _charReader = new(textReader);
        }

        /// <summary>
        /// First-level parsing, no grouping is made
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Token> ReadRawTokens()
        {
            for (; ; )
            {
                var c = _charReader.Read();
                if (!c.HasValue)
                    yield break;

                if (c.Is(CharacterType.Dquote))
                    yield return new Token(CaptureUntil(c.Value, c.Value), TokenType.QuotedString);
                else if (c == '(')
                    yield return new Token(CaptureUntil(c.Value, ')'), TokenType.Comment);
                else if (c.Is(CharacterType.Wsp | CharacterType.Control))
                    yield return new Token(c.Value.ToString(), TokenType.Whitespace);
                else if (c.Is(CharacterType.Specials))
                    yield return new Token(c.Value.ToString(), TokenType.Special);
                else
                    yield return new Token(CaptureBefore(c.Value, CharacterType.Atext), TokenType.Atom);
            }
        }

        /// <summary>
        /// Second-level parsing:
        /// - atoms are grouped
        /// - whitespaces and comments are ignored
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Token> ReadTokens() => ReadRawTokens().Group();

        private string CaptureBefore(char start, CharacterType @while)
        {
            var s = new StringBuilder();
            s.Append(start);
            for (; ; )
            {
                var c = _charReader.Peek();
                if (!c.HasValue || !c.Is(@while))
                    return s.ToString();
                s.Append(c.Value);
                _charReader.Read(); // pops to next
            }
        }

        private string CaptureUntil(char start, char end)
        {
            var s = new StringBuilder();
            s.Append(start);
            for (; ; )
            {
                var c = _charReader.Read();
                if (!c.HasValue)
                    throw new FormatException($"End character {end} not found");
                s.Append(c.Value);
                if (c == end)
                    return s.ToString();
            }
        }
    }
}
