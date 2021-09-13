using System.Linq;
using System.Security.Cryptography.X509Certificates;
using InternetMessage.Tokens;
using InternetMessage.Utility;
using NUnit.Framework;

namespace InternetMessageTest
{
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
            Assert.AreEqual(1, t.Length);
            Assert.AreEqual(TokenType.Atom,t[0].Type);
            Assert.AreEqual("1.2.3.4",t[0].Text);
        }
    }
}