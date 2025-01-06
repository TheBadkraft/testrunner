
namespace TestRunner.Testing;

[TestContainer(Ignore = false)]
public class AssertsTests
{
    public static TestContext TestContext { get; set; }

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

    //  test assert multiple assertions
    [Test(Skip = true)]
    public void MultipleAssertions()
    {
        /*
            There is value in this, but it isn't a high priority. The expectation 
            is that all the tests will execute regardless of failures.

                 Assert.Multiple(() =>
                {
                    Assert.IsTrue(true);
                    Assert.IsFalse(false);
                    Assert.IsNotNull(null);
                    Assert.AreEqual(1, 1);
                });

        */
        Assert.IsNotEmpty(TestContext.ErrorMessage);
        Debug.WriteLine(TestContext.ErrorMessage);
    }

    [Test]
    public void ExpectedException()
    {
        Assert.ThrowException<Exception>(() => throw new Exception());
    }

    [Test]
    public void ExpectedException_Fail()
    {
        Assert.ThrowException<ArgumentNullException>(() => throw new InvalidOperationException());
    }
}
