namespace InternetMessage.Tokens
{
    public static class TokenTypeExtensions
    {
        public static bool HasSemantic(this TokenType tokenType) => tokenType != TokenType.Whitespace && tokenType != TokenType.Comment;
    }
}
