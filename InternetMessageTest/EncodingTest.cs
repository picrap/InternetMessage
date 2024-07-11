using System;
using InternetMessage.Encoding;
using InternetMessage.Utility;
using NUnit.Framework;

namespace InternetMessageTest;

[TestFixture]
public class EncodingTest
{
    [Test]
    public void NotEncodedTest()
    {
        var e = "Not encoded";
        var s = e.Decode();
        Assert.That(s, Is.EqualTo(e));
    }

    [Test]
    public void Base64EncodedTest()
    {
        var e = "=?utf-8?B?U2ltcGxlLnR4dA==?=";
        var s = e.Decode();
        Assert.That(s, Is.EqualTo("Simple.txt"));
    }

    [Test]
    public void QuotedPrintableEncodedTest()
    {
        var e = "a=40b";
        var qp = new QuotedPrintableDecoder();
        var s = System.Text.Encoding.Default.GetString(qp.Decode(e));
        Assert.That(s, Is.EqualTo("a@b"));
    }

    [Test]
    public void QuotedPrintableCrLfEncodedTest()
    {
        var e = "a=\r\nb";
        var qp = new QuotedPrintableDecoder();
        var s = System.Text.Encoding.Default.GetString(qp.Decode(e));
        Assert.That(s, Is.EqualTo("ab"));
    }

    [Test]
    public void QuotedPrintableCrEncodedTest()
    {
        var e = "a=\rb";
        var qp = new QuotedPrintableDecoder();
        var s = System.Text.Encoding.Default.GetString(qp.Decode(e));
        Assert.That(s, Is.EqualTo("ab"));
    }

    [Test]
    public void QuotedPrintableKeepCrLfEncodedTest()
    {
        var e = "a\r\nb";
        var qp = new QuotedPrintableDecoder();
        var s = System.Text.Encoding.Default.GetString(qp.Decode(e));
        Assert.That(s, Is.EqualTo("a\r\nb"));
    }

    [Test]
    [TestCase("a=")]
    [TestCase("a=1")]
    [TestCase("a=0Z")]
    [TestCase("a=X")]
    public void InvalidQuotedPrintableEncodedTest(string s)
    {
        var qp = new QuotedPrintableDecoder();
        Assert.Throws<FormatException>(() => qp.Decode(s));
    }
}
