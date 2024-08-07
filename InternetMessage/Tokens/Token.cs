﻿
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using InternetMessage.Utility;

namespace InternetMessage.Tokens;

[DebuggerDisplay("{Text} ({Type})")]
public class Token
{
    private readonly IList<Token> _groupedTokens = new List<Token>();

    private readonly string _rawText;

    public string Text { get; private set; }

    public TokenType Type { get; }

    public IEnumerable<Token> Children => _groupedTokens;

    public Token(string rawText, TokenType type)
    {
        _rawText = rawText;
        Text = rawText.Decode();
        Type = type;
    }

    public Token AddChild(Token token)
    {
        _groupedTokens.Add(token);
        Text = string.Join("", Children.Where(c => c.Type.HasSemantic()).Select(c => c.Text));
        return this;
    }

    public Token AddChildren(IEnumerable<Token> tokens)
    {
        foreach (var token in tokens)
            AddChild(token);
        return this;
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