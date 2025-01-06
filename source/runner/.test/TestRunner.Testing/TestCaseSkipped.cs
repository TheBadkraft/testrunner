using System;

namespace TestRunner.Testing;

[TestContainer(Ignore = true)]
public class TestCaseSkipped
{

    [Test(Skip = true)]
    public void SkippedTest()
    {
        Assert.Fail();
    }
}
