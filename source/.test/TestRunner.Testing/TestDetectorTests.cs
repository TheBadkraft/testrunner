using MindForge.TestRunner.Core;
using MindForge.TestRunner.Logging;

namespace testrunner.testing;

[TestContainer]
public class TestDetectorTests
{
    private readonly RunnerConfig config = new()
    {
        Paths = ["./.test"]
    };

    // public static TestContext TestContext { get; set; }
    internal static TestLogger Logger { get; set; }

    public static TestContext TestContext { get; set; }

    [ContainerInitialize]
    public static void Initialize(TestContext context)
    {
        Logger = new TestLogger();
    }

    [TearDown]
    public void TearDown()
    {
        Logger.Flush();
    }

    [Test]
    public void EnumerateTests_NoTestAssembly_ShouldReturnFalse()
    {
        var mockLogger = new Mock<ILogger>();
        var detector = new TestDetector(mockLogger.Object, config);
        var proj = new ProjectInfo { AssemblyPath = "non_existent.dll" };

        Assert.IsFalse(detector.EnumerateTests(proj));
    }

    [Test]
    public void EnumerateTests_WithTestAssembly_ShouldFindTests()
    {
        // Mock or create a simple assembly with test containers
        var mockLogger = new Mock<ILogger>();
        var detector = new TestDetector(mockLogger.Object, config);
        var proj = new ProjectInfo { AssemblyPath = "./TestAssemblyWithTests.dll" }; // Assume this assembly has test containers

        Assert.IsTrue(detector.EnumerateTests(proj));
        Assert.IsNotNull(proj.TestContainers);
        Assert.IsNotEmpty(proj.TestContainers);
    }

    [Test]
    public void EnumerateTests_WithEmptyAssembly_ShouldReturnFalse()
    {
        var mockLogger = new Mock<ILogger>();
        var detector = new TestDetector(mockLogger.Object);
        var proj = new ProjectInfo { AssemblyPath = "./EmptyAssembly.dll" }; // Assume this assembly has no test containers

        Assert.False(detector.EnumerateTests(proj));
    }
}

internal class TestLogger : ILogger
{
    private StringBuilder logString = new();

    public void Log(DebugLevel debugLevel, string message)
    {
        switch (debugLevel)
        {
            case DebugLevel.Verbose:
                message = LogFormat.Debug(message);

                break;
            case DebugLevel.Default:
                message = LogFormat.Info(message);

                break;
            case DebugLevel.Warning:
                message = LogFormat.Warning(message);

                break;
            case DebugLevel.Error:
                message = LogFormat.Error(message);

                break;
            case DebugLevel.Fatal:
                message = LogFormat.Fatal(message);

                break;
            case DebugLevel.Test:
                message = LogFormat.Test(message);

                break;
        }

        logString.AppendLine(message);
    }

    public void Flush()
    {
        Debug.WriteLine(logString.ToString());
        logString.Clear();
    }
    public void Shutdown()
    {
        Flush();
    }
    public void Dispose()
    {
        Flush();
    }
}