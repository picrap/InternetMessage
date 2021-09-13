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
    }
}
