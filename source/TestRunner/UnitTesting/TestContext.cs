
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
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }

    public ILogger Logger { get; init; }


    /// <summary>
    /// Start unit test
    /// </summary>
    public void StartTest()
    {
        StartTime = DateTime.Now;
    }
    /// <summary>
    /// End unit test
    /// </summary>
    /// <param name="success">Flag indicating whether the test was successful</param>
    /// <param name="errorMessage">Error message if the test failed</param>
    public void EndTest(bool success, string errorMessage = null)
    {
        EndTime = DateTime.Now;
        Success = success;
        ErrorMessage = errorMessage;
    }

    /// <summary>
    /// Notify the test result
    /// </summary>
    internal void ReportResult()
    {
        ContainerInfo.TryGetResultInfo(TestMethodName, out TestCaseResult tcResult);
        TestCaseSubject.NotifyAssertionResult(tcResult);
    }
}
