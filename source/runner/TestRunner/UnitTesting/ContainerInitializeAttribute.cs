
namespace MindForge.TestRunner.UnitTesting;

/// <summary>
/// Attribute to mark a method for container initialization in unit tests.
/// </summary>
/// <remarks>
/// This attribute should be placed on a static method that accepts a <see cref="TestContext"/> parameter.
/// </remarks>
[AttributeUsage(AttributeTargets.Method)]
public class ContainerInitializeAttribute : Attribute
{
    //  initialize the container, must be placed on a static method accepting a TestContext parameter
}
