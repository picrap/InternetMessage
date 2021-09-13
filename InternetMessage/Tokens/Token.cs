
namespace InternetMessage.Tokens
{
    public enum TokenType
    {
        Whitespace,
        QuotedString,
        Comment,
        Atom,
        Special,
    }

    public struct Token
    {
        public string Text { get; }
        public TokenType Type { get; }

        public Token(string text, TokenType type)
        {
            Text = text;
            Type = type;
        }
    }
}
