
namespace MindForge.TestRunner.UnitTesting;

/// <summary>
/// Attribute to indicate that a method should be executed after each test in a test fixture has run.
/// </summary>
/// <remarks>
/// The method marked with this attribute must be an instance method and should not accept any parameters.
/// </remarks>
[AttributeUsage(AttributeTargets.Method)]
public class TearDownAttribute : Attribute
{
    //  cleanup the test, must be placed on an instance method accepting no parameters
}
