

namespace MindForge.TestRunner.Core;

/// <summary>
/// Represents a test executor that runs tests and logs the results.
/// </summary>
public delegate void TestCase();
/// <summary>
/// Represents a method that initializes the test container.
/// </summary>
public delegate void InitializeContainer(TestContext context);
/// <summary>
/// Represents a method that cleans up the test container.
/// </summary>
public delegate void CleanUpContainer();
/// <summary>
/// Represents a method that sets up the test.
/// </summary>
public delegate void TestSetUp();
/// <summary>
/// Represents a method that tears down the test.
/// </summary>
public delegate void TestTearDown();
/// <summary>
/// Represents a method that assigns the test context.
/// </summary>
/// <param name="context"></param>
public delegate void AssignTestContext(TestContext context);

/// <summary>
/// Represents a test executor that runs tests and logs the results.
/// </summary>
/// <param name="logger">The logger used to log test execution details.</param>
/// <exception cref="NotImplementedException">Thrown to indicate that the method is not yet implemented.</exception>
public class TestExecutor
{
    private ILogger Logger { get; init; }

    /// <summary>
    /// Gets the test context for the test executor.
    /// </summary>
    internal TestContext TestContext { get; init; }

    public TestExecutor(ILogger logger)
    {
        Logger = logger;
        TestContext = new TestContext { Logger = logger };
    }

    /// <summary>
    /// Executes the tests for the specified projects.
    /// </summary>
    /// <param name="projects">The projects to execute tests for.</param>
    internal void ExecuteTests(IEnumerable<ProjectInfo> projects, out IEnumerable<TestCaseResult> testResults)
    {
        Logger.Log(DebugLevel.Default, "Begin Test Execution ...");

        //  iterate projects
        foreach (var proj in projects)
        {
            //  iterate test containers
            foreach (var containerInfo in proj.TestContainers)
            {
                TestContext.ContainerInfo = containerInfo; // Update context with current container info

                containerInfo.AssignContextDelegate(TestContext); // Set TestContext if the container supports it

                containerInfo.InitializeContainer(TestContext); // Initialize container setup, passing context if needed

                foreach (var testInfo in containerInfo.Tests)
                {
                    var name = testInfo.Key;
                    var test = testInfo.Value;
                    containerInfo.CurrentTest = name;
                    RunTest(containerInfo, name, test);
                }

                containerInfo.CleanUpContainer(); // Clean up container, passing context if needed
            }
        }

        testResults = projects.SelectMany(p => p.TestContainers.SelectMany(tc => tc.Results));
        Logger.Log(DebugLevel.Default, "... Test Execution Complete");
    }

    private void RunTest(TestContainerInfo containerInfo, string testName, TestCase test)
    {
        if (!containerInfo.TryGetResultInfo(testName, out var testResult))
        {
            Logger.Log(DebugLevel.Warning, $"Test {testName} not found in {containerInfo.Name}.");
            return;
        }

        testResult.StartTest();

        try
        {
            containerInfo.TestSetUp(); // Will instantiate the container if needed
            test(); // Execute the test method
            testResult.EndTest(TestResult.Pass);
            Logger.Log(DebugLevel.Default, $"Test {testName} in {containerInfo.Name} {testResult.Outcome}.");
        }
        catch (Exception ex)
        {
            testResult.EndTest(TestResult.Fail, ex.Message);
            Logger.Log(DebugLevel.Error, $"Test {testName} in {containerInfo.Name} failed: {TestContext.ErrorMessage}");
        }
        finally
        {
            containerInfo.TestTearDown(); // Will instantiate the container if needed
        }
    }
}
