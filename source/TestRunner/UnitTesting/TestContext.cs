
using System.ComponentModel;
using MindForge.TestRunner.Core;

namespace MindForge.TestRunner.UnitTesting;

public class TestContext : TestCaseSubject
{
    internal TestContainerInfo ContainerInfo { get; set; }

    public string ContainerName => ContainerInfo?.Name;
    public string TestMethodName => ContainerInfo?.CurrentTest;
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public TimeSpan Duration => EndTime - StartTime;
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }

    public ILogger Logger { get; init; }

    public void StartTest()
    {
        StartTime = DateTime.Now;
    }

    public void EndTest(bool success, string errorMessage = null)
    {
        EndTime = DateTime.Now;
        Success = success;
        ErrorMessage = errorMessage;
    }

    internal void ReportResult()
    {
        ContainerInfo.TryGetResultInfo(TestMethodName, out TestCaseResult tcResult);
        NotifyAssertionResult(tcResult);
    }
}
