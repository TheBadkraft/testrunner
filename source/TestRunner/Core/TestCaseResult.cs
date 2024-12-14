
namespace MindForge.TestRunner.Core;

/// <summary>
/// Represents the result of a test case execution.
/// </summary>
public class TestCaseResult
{
    /// <summary>
    /// Gets or sets the name of the test case.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Gets or sets the parent container name.
    /// </summary>
    public string ContainerName { get; set; }
    /// <summary>
    /// Gets or sets the outcome of the test.
    /// </summary>
    public TestResult Outcome { get; set; }
    /// <summary>
    /// Gets or sets the error message if the test failed.
    /// </summary>
    public string Message { get; set; }
    /// <summary>
    /// Gets test start time.
    /// </summary>
    public DateTime StartTime { get; private set; }
    /// <summary>
    /// Gets test end time.
    /// </summary>
    public DateTime EndTime { get; private set; }
    /// <summary>
    /// Gets the duration of the test.
    /// </summary>
    public TimeSpan Duration => EndTime - StartTime;
    /// <summary>
    /// Determines whether a test case has been interrupted
    /// </summary>
    public bool IsInterrupted { get; internal set; } = false;

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
    public void EndTest()
    {
        EndTime = DateTime.Now;
    }
    /// <summary>
    /// End unit test with unexpected result
    /// </summary>
    /// <param name="result"></param>
    /// <param name="message"></param>
    internal void EndTest(TestResult result, string message)
    {
        EndTest();
        Outcome = result;
        Message = message;
    }
}
