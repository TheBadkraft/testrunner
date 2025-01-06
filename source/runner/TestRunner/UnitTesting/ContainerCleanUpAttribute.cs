
namespace MindForge.TestRunner.UnitTesting;

/// <summary>
/// Attribute to indicate that a method is responsible for cleaning up the container.
/// </summary>
/// <remarks>
/// This attribute should be placed on a static method that accepts no parameters.
/// </remarks>
[AttributeUsage(AttributeTargets.Method)]
public class ContainerCleanUpAttribute : Attribute
{
    //  cleanup the container, must be placed on a static method accepting no parameters
}
