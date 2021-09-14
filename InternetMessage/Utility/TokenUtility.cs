
using System;
using System.Collections.Generic;
using System.Linq;
using InternetMessage.Tokens;

namespace InternetMessage.Utility
{
    public static class TokenUtility
    {
        public static IEnumerable<ICollection<Token>> ChunkBy(this IEnumerable<Token> tokens, TokenType? tokenType = null, string tokenText = null)
        {
            var slice = new List<Token>();
            foreach (var token in tokens)
            {
                if (token.Matches(tokenType, tokenText))
                {
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
