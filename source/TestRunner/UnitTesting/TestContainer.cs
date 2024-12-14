
namespace MindForge.TestRunner.UnitTesting;


/// <summary>
/// Specifies that a class is a test container for unit tests.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class TestContainerAttribute : Attribute
{
    /// <summary>
    /// Determines whether a test container is ignored
    /// </summary>
    public bool Ignore { get; set; }
}
