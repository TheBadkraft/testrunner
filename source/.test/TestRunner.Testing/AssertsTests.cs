
namespace TestRunner.Testing;

[TestContainer]
public class AssertsTests
{
    [Test]
    public void IsTrue()
    {
        Assert.IsTrue(true);
    }
    [Test]
    public void IsTrue_Fail()
    {
        Assert.IsTrue(false);
    }

    [Test]
    public void IsFalse()
    {
        Assert.IsFalse(false);
    }
    [Test]
    public void IsFalse_Fail()
    {
        Assert.IsFalse(true);
    }

    [Test]
    public void AreEqual()
    {
        Assert.AreEqual(1, 1);
    }
    [Test]
    public void AreEqual_Fail()
    {
        Assert.AreEqual(1, 2);
    }

    [Test]
    public void AreNotEqual()
    {
        Assert.AreNotEqual(1, 2);
    }

    [Test]
    public void AreSame()
    {
        var obj = new object();
        Assert.AreSame(obj, obj);
    }
    [Test]
    public void AreSame_Fail()
    {
        Assert.AreSame(new object(), new object());
    }

    [Test]
    public void AreNotSame()
    {
        Assert.AreNotSame(new object(), new object());
    }
    [Test]
    public void AreNotSame_Fail()
    {
        var obj = new object();
        Assert.AreNotSame(obj, obj);
    }
}
