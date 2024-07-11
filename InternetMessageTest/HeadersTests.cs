
using System.IO;
using System.Linq;
using InternetMessage.Message;
using InternetMessage.Reader;
using InternetMessage.Tokens;
using NUnit.Framework;

namespace InternetMessageTest;

[TestFixture]
public class HeadersTests
{
    [Test]
    public void ReadAtomTest()
    {
        var r = new InternetMessageHeaderBodyReader(new StringReader("abc"));
        var t = r.ReadRawTokens().ToArray();
        Assert.That(t.Length, Is.EqualTo(1));
        Assert.That(t[0].Type, Is.EqualTo(TokenType.Atom));
        Assert.That(t[0].Text, Is.EqualTo("abc"));
    }
    [Test]
    public void ReadAtomsAndSpecialsTest()
    {
        var r = new InternetMessageHeaderBodyReader(new StringReader("10.69.10.170"));
        var t = r.ReadRawTokens().ToArray();
        Assert.That(t.Length, Is.EqualTo(7));
        Assert.That(t[0].Type, Is.EqualTo(TokenType.Atom));
        Assert.That(t[0].Text, Is.EqualTo("10"));
        Assert.That(t[1].Type, Is.EqualTo(TokenType.Special));
        Assert.That(t[1].Text, Is.EqualTo("."));
        Assert.That(t[2].Type, Is.EqualTo(TokenType.Atom));
        Assert.That(t[2].Text, Is.EqualTo("69"));
        Assert.That(t[3].Type, Is.EqualTo(TokenType.Special));
        Assert.That(t[3].Text, Is.EqualTo("."));
        Assert.That(t[4].Type, Is.EqualTo(TokenType.Atom));
        Assert.That(t[4].Text, Is.EqualTo("10"));
        Assert.That(t[5].Type, Is.EqualTo(TokenType.Special));
        Assert.That(t[5].Text, Is.EqualTo("."));
        Assert.That(t[6].Type, Is.EqualTo(TokenType.Atom));
        Assert.That(t[6].Text, Is.EqualTo("170"));
    }

    [Test]
    public void ReadQuotedStringTest()
    {
        var r = new InternetMessageHeaderBodyReader(new StringReader("a \"this is a quoted string\" bc"));
        var t = r.ReadRawTokens().Single(t => t.Type == TokenType.QuotedString);
        Assert.That(t.Text, Is.EqualTo("this is a quoted string"));
    }

    [Test]
    public void ReadCommentTest()
    {
        var r = new InternetMessageHeaderBodyReader(new StringReader("a this is a (small comment) bc"));
        var t = r.ReadRawTokens().Single(t => t.Type == TokenType.Comment);
        Assert.That(t.Text, Is.EqualTo("(small comment)"));
    }

    [Test]
    public void ReadNestedCommentTest()
    {
        var r = new InternetMessageHeaderBodyReader(new StringReader("a this is a ((very) small comment) bc"));
        var t = r.ReadRawTokens().Single(t => t.Type == TokenType.Comment);
        Assert.That(t.Text, Is.EqualTo("((very) small comment)"));
    }

    public const string ContentDispositionName = @"Content-Disposition";
    public string[] ContentDispositionBody = new[]
    {
            @"attachment;",
            @"	filename=""=?utf-8?B?U2ltcGxlLnR4dA==?="""
        };

    [Test]
    public void EncodedStringTest()
    {
        var h = new InternetMessageHttpHeaderField(ContentDispositionName, ContentDispositionBody);
        var f = h.Tokens.Get("filename").Single().Text;
        Assert.That(f, Is.EqualTo("Simple.txt"));
    }
}

