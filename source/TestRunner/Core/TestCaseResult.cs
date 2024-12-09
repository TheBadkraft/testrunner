
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
    public string ContainerName { get; set; }
    /// <summary>
    /// Gets or sets the outcome of the test.
    /// </summary>
    public TestResult Outcome { get; set; }
    /// <summary>
    /// Gets or sets the error message if the test failed.
    /// </summary>
    public string ErrorMessage { get; set; }
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

    public void StartTest()
    {
        StartTime = DateTime.Now;
    }

    public void EndTest(TestResult outcome, string errorMessage = null)
    {
        EndTime = DateTime.Now;
        Outcome = outcome;
        ErrorMessage = errorMessage;
    }
}
