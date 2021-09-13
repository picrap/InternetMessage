using System.IO;
using InternetMessage.Reader;
using NUnit.Framework;

namespace InternetMessageTest
{
    [TestFixture]
    public class PeekCharReaderTests
    {
        [Test]
        public void Read3Test()
        {
            var r = new PeekCharReader(new StringReader("ijk"));
            Assert.AreEqual('i', r.Read());
            Assert.AreEqual('j', r.Read());
            Assert.AreEqual('k', r.Read());
            Assert.AreEqual(null, r.Read());
        }

        [Test]
        public void PeekRead3Test()
        {
            var r = new PeekCharReader(new StringReader("xyz"));
            Assert.AreEqual('x', r.Read());
            // OK, maybe a little bit excessive here
            Assert.AreEqual('y', r.Peek());
            Assert.AreEqual('y', r.Peek());
            Assert.AreEqual('y', r.Peek());
            Assert.AreEqual('y', r.Peek());
            Assert.AreEqual('y', r.Read());
            Assert.AreEqual('z', r.Read());
            Assert.AreEqual(null, r.Read());
        }
    }
}