using System.IO;
using System.Linq;
using InternetMessage.Reader;
using NUnit.Framework;

namespace InternetMessageTest
{
    [TestFixture]
    public class ReaderTests
    {
        #region public const string MultiPartMessage = @"…"
        public const string MultiPartMessage = @"From: ""Some Dude"" <>
To: some.other@du.de
Subject: =?utf-8?B?V2l0aCBhdHRhY2htZW50cw==?=
Date: Wed, 8 Sep 2021 3:19:09 -0500
MIME-Version: 1.0
Content-Type: multipart/mixed;
	boundary=""Mark=_531883546265941773361""
X-Priority: 3

This is a multi-part message in MIME format.

--Mark=_531883546265941773361
Content-Type: text/plain;
	charset=""utf-8""
Content-Transfer-Encoding: base64

SGVyZSB3ZSBnbw0K

--Mark=_531883546265941773361
Content-Type: application/octet-stream;
	name=""=?utf-8?B?U2ltcGxlLnR4dA==?=""
Content-Transfer-Encoding: base64
Content-Disposition: attachment;
	filename=""=?utf-8?B?U2ltcGxlLnR4dA==?=""

QSBzaW1wbGUgdGV4dCBoZXJl

--Mark=_531883546265941773361--
";
        #endregion

        [Test]
        public void SimpleReadTest()
        {
            using var textReader = new StringReader(MultiPartMessage);
            var internetMessageReader = new InternetMessageReader(textReader);
            var headers = internetMessageReader.ReadHeaders().ToArray();
            var body = internetMessageReader.ReadBody();
            Assert.AreEqual(7,headers.Length);
        }

        [Test]
        [Ignore("not finished yet")]
        public void FullReadTest()
        {
            using var textReader = new StringReader(MultiPartMessage);
            var internetMessageReader = new InternetMessageReader(textReader, InternetMessageFactory.Full);
            var headers = internetMessageReader.ReadHeaders().ToArray();
            var body = internetMessageReader.ReadBody();
            Assert.AreEqual(7, headers.Length);
        }
    }
}