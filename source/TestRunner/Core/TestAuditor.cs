

namespace MindForge.TestRunner.Core;

/// <summary>
/// Represents an auditor for running tests.
/// </summary>
/// <param name="logger">The logger used for logging test audit information.</param>
/// <exception cref="NotImplementedException">Thrown to indicate that the constructor is not yet implemented.</exception>
public class TestAuditor
{
    private ILogger Logger { get; init; }
    private IReadOnlyCollection<TestCaseResult> Results { get; set; }

    public TestAuditor(ILogger logger)
    {
        Logger = logger;
    }

    internal void AuditResults(IEnumerable<TestCaseResult> testResults)
    {
        Logger.Log(DebugLevel.Default, "Begin Auditing Results ...");
        Results = testResults.ToList();
        GenerateSummaryReport();
        LogDetailedResults();
    }

    /// <summary>
    /// Generates a summary report of the test results.
    /// </summary>
    private void GenerateSummaryReport()
    {
        var totalTests = Results.Count;
        var passedTests = Results.Count(r => r.Outcome == TestResult.Pass);
        var failedTests = Results.Count(r => r.Outcome == TestResult.Fail);
        var undefinedTests = Results.Count(r => r.Outcome == TestResult.Undef);

        Logger.Log(DebugLevel.Test, $"Test Summary:");
        Logger.Log(DebugLevel.Test, $"  Total Tests: {totalTests}");
        Logger.Log(DebugLevel.Test, $"  Passed: {passedTests}");
        Logger.Log(DebugLevel.Test, $"  Failed: {failedTests}");
        Logger.Log(DebugLevel.Test, $"  Undefined: {undefinedTests}");
        Logger.Log(DebugLevel.Test, $"  Pass Rate: {(double)passedTests / totalTests:P}");
    }
    /// <summary>
    /// Logs detailed results for each test case.
    /// </summary>
    private void LogDetailedResults()
    {
        Logger.Log(DebugLevel.Test, "Detailed Results:");
        foreach (var result in Results)
        {
            string outcome = result.Outcome switch
            {
                TestResult.Pass => "PASSED",
                TestResult.Fail => "FAILED",
                _ => "UNDEF"
            };
            Logger.Log(DebugLevel.Test, $"  {result.ContainerName}.{result.Name}, Result: {result.Outcome}, Duration: {result.Duration}");
            if (result.Outcome == TestResult.Fail)
            {
                Logger.Log(DebugLevel.Test, $"    {result.ErrorMessage}");
            }
        }
    }

    private void SaveResults()
    {
        //  save results to a file
        var filePath = $"TestResults_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.txt";
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Test Results Log");
            writer.WriteLine($"Generated on: {DateTime.Now}");
            writer.WriteLine();

            foreach (var result in Results)
            {
                string outcome = result.Outcome switch
                {
                    TestResult.Pass => "PASSED",
                    TestResult.Fail => "FAILED",
                    _ => "UNDEF"
                };
                writer.WriteLine($"Container: {result.ContainerName}, Test: {result.Name}, Result: {outcome}, Duration: {result.Duration}");
                if (result.Outcome == TestResult.Fail)
                {
                    writer.WriteLine($"  Error: {result.ErrorMessage}");
                }
                writer.WriteLine();
            }
        }
    }
}