
using System;
using System.Collections.Generic;
using System.Linq;
using InternetMessage.Tokens;

namespace InternetMessage.Utility
{
    public static class TokenUtility
    {
        public static IEnumerable<ICollection<Token>> ChunkBy(this IEnumerable<Token> tokens, TokenType? tokenType = null, string tokenText = null, int maximumChunks = int.MaxValue)
        {
            var slice = new List<Token>();
            var chunksCount = 1;
            foreach (var token in tokens)
            {
                if (token.Matches(tokenType, tokenText) && chunksCount < maximumChunks)
                {
                    chunksCount++;
                    if (slice.Count > 0)
                        yield return slice;
                    slice = new();
                }
                else
                    slice.Add(token);
            }
            if (slice.Count > 0)
                yield return slice;
        }

        public static IEnumerable<Token> Group(this IEnumerable<Token> tokens)
        {
            Token currentToken = null;
            var whiteSpaces = new List<Token>();
            foreach (var token in tokens)
            {
                if (whiteSpaces is not null && !token.Type.HasSemantic())
                {
                    whiteSpaces.Add(token);
                    continue;
                }
                if (!CanAddChild(currentToken, token))
                {
                    if (currentToken is not null)
                        yield return currentToken;
                    currentToken = new Token(token.Text, token.Type);
                    if (whiteSpaces is not null)
                    {
                        currentToken.AddChildren(whiteSpaces);
                        whiteSpaces = null;
                    }
                }
                currentToken.AddChild(token);
            }

            if (currentToken is not null)
                yield return currentToken;
            else if (whiteSpaces is not null)
                yield return new Token("", TokenType.Comment).AddChildren(whiteSpaces);
        }

        public static Token Combine(this IEnumerable<Token> tokens)
        {
            var allTokens = tokens.ToArray();
            return allTokens.Length switch
            {
                0 => null,
                1 => allTokens[0],
                _ => new Token("", TokenType.Atom).AddChildren(allTokens)
            };
        }

        public static IEnumerable<Token> Split(this Token token, string separator)
        {
            if (!token.Type.HasSemantic())
            {
                yield return token;
                yield break;
            }

            var parts = token.Text.Split(new[] { separator }, StringSplitOptions.None);
            if (parts.Length == 1)
            {
                yield return token;
                yield break;
            }

            for (var partIndex = 0; partIndex < parts.Length; partIndex++)
            {
                var part = parts[partIndex];
                yield return new Token(part, token.Type);
                if (partIndex < parts.Length - 1)
                    yield return new Token(separator, token.Type);
            }
        }

        private static bool CanAddChild(Token token, Token child)
        {
            if (token is null)
                return false;
            return child.Type switch
            {
                TokenType.Whitespace or TokenType.Comment => true,
                TokenType.Atom or TokenType.QuotedString => token.Type == TokenType.Atom || token.Type == TokenType.QuotedString || token.Type == TokenType.Special && token.Text == ".",
                TokenType.Special => false,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
