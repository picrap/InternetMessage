using System;
using System.Net.Mime;
using InternetMessage.Encoding;
using InternetMessage.Utility;
using NUnit.Framework;

namespace InternetMessageTest
{
    [TestFixture]
    public class EncodingTest
    {
        [Test]
        public void NotEncodedTest()
        {
            var e = "Not encoded";
            var s = e.Decode();
            Assert.AreEqual(e, s);
        }

        [Test]
        public void Base64EncodedTest()
        {
            var e = "=?utf-8?B?U2ltcGxlLnR4dA==?=";
            var s = e.Decode();
            Assert.AreEqual("Simple.txt", s);
        }

        [Test]
        public void QuotedPrintableEncodedTest()
        {
            var e = "a=40b";
            var qp = new QuotedPrintableDecoder();
            var s = System.Text.Encoding.Default.GetString(qp.Decode(e));
            Assert.AreEqual("a@b", s);
        }

        [Test]
        public void QuotedPrintableCrLfEncodedTest()
        {
            var e = "a=\r\nb";
            var qp = new QuotedPrintableDecoder();
            var s = System.Text.Encoding.Default.GetString(qp.Decode(e));
            Assert.AreEqual("ab", s);
        }

        [Test]
        public void QuotedPrintableCrEncodedTest()
        {
            var e = "a=\rb";
            var qp = new QuotedPrintableDecoder();
            var s = System.Text.Encoding.Default.GetString(qp.Decode(e));
            Assert.AreEqual("ab", s);
        }

        [Test]
        public void QuotedPrintableKeepCrLfEncodedTest()
        {
            var e = "a\r\nb";
            var qp = new QuotedPrintableDecoder();
            var s = System.Text.Encoding.Default.GetString(qp.Decode(e));
            Assert.AreEqual("a\r\nb", s);
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
}
