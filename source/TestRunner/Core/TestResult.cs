
namespace MindForge.TestRunner.Core;

/// <summary>
/// Represents the result of a test execution.
/// </summary>
public enum TestResult
{
    /// <summary>
    /// Indicates that the test has passed.
    /// </summary>
    Pass,

    /// <summary>
    /// Indicates that the test has failed.
    /// </summary>
    Fail,

    /// <summary>
    /// Indicates that the test result is undefined.
    /// </summary>
    Undef
}