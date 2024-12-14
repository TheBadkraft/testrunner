using System;

namespace TestRunner.Testing;

[TestContainer]
public class TestResultTests
{

    [Test]
    public void AssertPassed()
    {
        Assert.IsTrue(true);
    }

    [Test]
    public void AssertFail()
    {
        //  TestRunner interrupts test case on failed message
        Assert.Fail("TestCase Interrupted");
        Assert.IsTrue(false, "If you see this one, something is wrong.");
    }

    [Test]
    public void NotImplemented()
    {
        Assert.NotImplemented("Test Not Yet Implemented");
    }

    #region TestContainer Initialize & CleanUp

    #endregion
}
