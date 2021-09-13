
using System.Collections.Generic;
using System.Linq;

namespace InternetMessage.Tokens
{
    public class Token
    {
        private readonly IList<Token> _groupedTokens = new List<Token>();

        public string Text { get; private set; }
        public TokenType Type { get; }

        public IEnumerable<Token> Children => _groupedTokens;

        public Token(string text, TokenType type)
        {
            Text = text;
            Type = type;
        }

        public void AddChild(Token token)
        {
            _groupedTokens.Add(token);
            Text = string.Join("", Children.Where(c => c.Type is TokenType.QuotedString or TokenType.Atom or TokenType.Special).Select(c => c.Text));
        }

        public bool Matches(TokenType? tokenType = null, string tokenText = null)
        {
            if (tokenType.HasValue && tokenType != Type)
                return false;
            if (tokenText is not null && tokenText != Text)
                return false;
            return true;
        }
    }
}
