
using System.IO;
using System.Linq;
using InternetMessage.Reader;
using InternetMessage.Tokens;
using NUnit.Framework;

namespace InternetMessageTest
{
    [TestFixture]
    public class HeadersTests
    {
        [Test]
        public void ReadAtomTest()
        {
            var r = new InternetMessageHeaderBodyReader(new StringReader("abc"));
            var t = r.ReadRawTokens().ToArray();
            Assert.AreEqual(1,t.Length);
            Assert.AreEqual(TokenType.Atom,t[0].Type);
            Assert.AreEqual("abc",t[0].Text);
        }
        [Test]
        public void ReadAtomsAndSpecialsTest()
        {
            var r = new InternetMessageHeaderBodyReader(new StringReader("10.69.10.170"));
            var t = r.ReadRawTokens().ToArray();
            Assert.AreEqual(7,t.Length);
            Assert.AreEqual(TokenType.Atom,t[0].Type);
            Assert.AreEqual("10",t[0].Text);
            Assert.AreEqual(TokenType.Special,t[1].Type);
            Assert.AreEqual(".",t[1].Text);
            Assert.AreEqual(TokenType.Atom,t[2].Type);
            Assert.AreEqual("69",t[2].Text);
            Assert.AreEqual(TokenType.Special,t[3].Type);
            Assert.AreEqual(".",t[3].Text);
            Assert.AreEqual(TokenType.Atom,t[4].Type);
            Assert.AreEqual("10",t[4].Text);
            Assert.AreEqual(TokenType.Special,t[5].Type);
            Assert.AreEqual(".",t[5].Text);
            Assert.AreEqual(TokenType.Atom,t[6].Type);
            Assert.AreEqual("170",t[6].Text);
        }
    }
}