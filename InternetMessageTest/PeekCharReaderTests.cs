using System.IO;
using InternetMessage.Reader;
using NUnit.Framework;

namespace InternetMessageTest;

[TestFixture]
public class PeekCharReaderTests
{
    [Test]
    public void Read3Test()
    {
        var r = new PeekCharReader(new StringReader("ijk"));
        Assert.That(r.Read(), Is.EqualTo('i'));
        Assert.That(r.Read(), Is.EqualTo('j'));
        Assert.That(r.Read(), Is.EqualTo('k'));
        Assert.That(r.Read(), Is.EqualTo(null));
    }

    [Test]
    public void PeekRead3Test()
    {
        var r = new PeekCharReader(new StringReader("xyz"));
        Assert.That(r.Read(), Is.EqualTo('x'));
        // OK, maybe a little bit excessive here
        Assert.That(r.Peek(), Is.EqualTo('y'));
        Assert.That(r.Peek(), Is.EqualTo('y'));
        Assert.That(r.Peek(), Is.EqualTo('y'));
        Assert.That(r.Peek(), Is.EqualTo('y'));
        Assert.That(r.Read(), Is.EqualTo('y'));
        Assert.That(r.Read(), Is.EqualTo('z'));
        Assert.That(r.Read(), Is.EqualTo(null));
    }
}
