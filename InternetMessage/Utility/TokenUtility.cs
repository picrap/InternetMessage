
using System;
using System.Collections.Generic;
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
        }

        public static IEnumerable<Token> Group(this IEnumerable<Token> tokens)
        {
            Token currentToken = null;
            foreach (var token in tokens)
            {
                if (!CanAddChild(currentToken, token))
                {
                    if (currentToken is not null)
                        yield return currentToken;
                    currentToken = new Token(token.Text, token.Type);
                }
                currentToken.AddChild(token);
            }

            if (currentToken is not null)
                yield return currentToken;
        }

        private static bool CanAddChild(Token token, Token child)
        {
            if (token is null)
                return false;
            return child.Type switch
            {
                TokenType.Whitespace or TokenType.Comment => true,
                TokenType.QuotedString => false,
                TokenType.Atom => token.Type == TokenType.Atom || token.Type == TokenType.Special && token.Text == ".",
                TokenType.Special => false,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
