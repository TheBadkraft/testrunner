
using MindForge.TestRunner.Core;

namespace MindForge.TestRunner.UnitTesting;

public class TestContext
{
    private TestCaseSubject TestCaseSubject => TestDirector.TestCaseSubject;

    /// <summary>
    /// Get or set the current <see cref="TestContainerInfo"/>
    /// </summary>
    internal TestContainerInfo ContainerInfo { get; set; }

    public string ContainerName => ContainerInfo?.Name;
    public string TestMethodName => ContainerInfo?.CurrentTest;
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public TimeSpan Duration => EndTime - StartTime;
    public TestResult TestResult => GetTestResult();

    public string ErrorMessage { get; set; }

    public ILogger Logger { get; init; }

    /// <summary>
    /// Notify the test result
    /// </summary>
    internal void ReportResult()
    {
        ContainerInfo.TryGetResultInfo(TestMethodName, out TestCaseResult tcResult);
        TestCaseSubject.NotifyAssertionResult(tcResult);
    }

    private TestResult GetTestResult()
    {
        if (ContainerInfo.TryGetResultInfo(TestMethodName, out TestCaseResult tcResult))
        {
            return tcResult.Outcome;
        }

        return TestResult.NotRun;
    }
}
