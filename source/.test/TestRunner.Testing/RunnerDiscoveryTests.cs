using System.Text;
using MindForge.TestRunner.Core;
using MindForge.TestRunner.Logging;

namespace testrunner.testing;

[TestContainer]
public class RunnerDiscoveryTests
{
    private readonly RunnerConfig config = new()
    {
        Paths = ["./.test"]
    };

    // public static TestContext TestContext { get; set; }
    public static ILogger Logger { get; set; } = new TestLogger();

    [Test]
    public void DiscoverTests()
    {
        // Arrange
        var detector = new TestDetector(Logger, new RunnerConfig() { Paths = ["./.test"] });

        // Act
        var retOk = detector.DiscoverTests(out var projects);

        // Assert
        Assert.IsTrue(retOk);
    }
}

internal class TestLogger : ILogger
{
    private StringBuilder logString = new();

    public void Log(DebugLevel debugLevel, string message)
    {
        switch (debugLevel)
        {
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
            default:
                break;
        }

        logString.AppendLine(message);
    }

    public void Shutdown()
    {
        Debug.WriteLine(logString.ToString());
    }

    public void Dispose()
    {

    }
}