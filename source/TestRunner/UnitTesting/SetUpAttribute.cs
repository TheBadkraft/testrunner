
namespace MindForge.TestRunner.UnitTesting;

/// <summary>
/// Attribute to mark a method that should be run before each test in a test class.
/// </summary>
/// <remarks>
/// The method marked with this attribute must be an instance method and should not accept any parameters.
/// </remarks>
[AttributeUsage(AttributeTargets.Method)]
public class SetUpAttribute : Attribute
{
    //  initialize the test, must be placed on an instance method accepting no parameters
}
