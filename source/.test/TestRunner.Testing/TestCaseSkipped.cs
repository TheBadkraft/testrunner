using System;

namespace TestRunner.Testing;

[TestContainer]
public class TestCaseSkipped
{

    [Test(Skip = true)]
    public void SkippedTest()
    {
        Assert.Fail();
    }
}
