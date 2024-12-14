
namespace testrunner.testing;

[TestContainer]
public class TestDetectorTests
{
    private const string TESTPATH = ".test/TestRunner.Testing/bin/Debug/net9.0";
    private readonly RunnerConfig config = new()
    {
        Paths = ["./.test"]
    };

    public static TestContext TestContext { get; set; }

    private static ILogger Logger { get; set; }

    [Test]
    public void EnumerateNoAssembly()
    {
        var detector = new TestDetector(Logger, config);
        var proj = new ProjectInfo { AssemblyPath = "non_existent.dll" };

        Assert.IsFalse(detector.EnumerateTests(proj));
    }

    [Test]
    public void EnumerateWithAssembly()
    {
        var detector = new TestDetector(Logger, config);
        var proj = new ProjectInfo
        {
            AssemblyName = "MindForge.TestRunner.Testing",
            AssemblyPath = $"{TESTPATH}/MindForge.TestRunner.Testing.dll",
            TargetFramework = "net9.0"
        };

        Assert.IsTrue(detector.EnumerateTests(proj));
        Assert.IsNotNull(proj.TestContainers);
        Assert.IsNotEmpty(proj.TestContainers);
    }

    [Test]
    public void EnumerateWithEmptyAssembly()
    {
        var detector = new TestDetector(Logger, config);
        var proj = new ProjectInfo { AssemblyPath = "./EmptyAssembly.dll" }; // Assume this assembly has no test containers

        Assert.IsFalse(detector.EnumerateTests(proj));
    }

    #region Test Configuration
    [ContainerInitialize]
    public static void Initialize(TestContext context)
    {
        Logger = A.Fake<ILogger>();
    }

    [TearDown]
    public void TearDown()
    {

    }

    #endregion
}
