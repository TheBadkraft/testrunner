
namespace MindForge.TestRunner.UnitTesting;

/// <summary>
/// Attribute to mark an instance method as a test method.
/// </summary>
/// <remarks>
/// This attribute should be applied to methods that are intended to be executed as tests.
/// The methods marked with this attribute should not accept any parameters.
/// </remarks>
[AttributeUsage(AttributeTargets.Method)]
public class TestAttribute : Attribute
{
    /// <summary>
    /// Determine whether a test case method is skipped
    /// </summary>
    public bool Skip { get; set; } = false;
}