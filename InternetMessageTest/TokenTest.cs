using System.Linq;
using InternetMessage.Tokens;
using InternetMessage.Utility;
using NUnit.Framework;

namespace InternetMessageTest;

[TestFixture]
public class TokenTest
{
    [Test]
    public void GroupAtomsTest()
    {
        var tt = new[]
        {
                new Token("1", TokenType.Atom),
                new Token(".", TokenType.Atom),
                new Token("2", TokenType.Atom),
                new Token(".", TokenType.Atom),
                new Token("3", TokenType.Atom),
                new Token(".", TokenType.Atom),
                new Token("4", TokenType.Atom),
            };
        var t = tt.Group().ToArray();
        Assert.That(t.Length, Is.EqualTo(1));
        Assert.That(t[0].Type, Is.EqualTo(TokenType.Atom));
        Assert.That(t[0].Text, Is.EqualTo("1.2.3.4"));
    }
}
