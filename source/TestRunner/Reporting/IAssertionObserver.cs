using MindForge.TestRunner.Core;

namespace MindForge.TestRunner.Reporting;

/// <summary>
/// Observer for assertion results
/// </summary>
public interface IAssertionObserver
{
    /// <summary>
    /// Notify assertion result
    /// </summary>
    /// <param name="tcResult">Test case result</param>
    void OnAssertion(TestCaseResult tcResult);
}
