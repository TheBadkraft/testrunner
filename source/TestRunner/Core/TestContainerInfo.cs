
namespace MindForge.TestRunner.Core;

public class TestContainerInfo
{
    private object _target = null;
    private List<TestCaseResult> _testResults = new();
    private Dictionary<string, TestCase> _testCases = new();
    //  lazy load the target object
    private object Target => _target ??= Activator.CreateInstance(ContainerType);
    private List<TestCaseResult> TestResults => _testResults;
    private Dictionary<string, TestCase> TestCases => _testCases;

    /// <summary>
    /// Gets the name of the test container.
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// Gets the current test being executed
    /// </summary>
    public string CurrentTest { get; set; }
    /// <summary>
    /// Gets the type of the test container.
    /// </summary>
    public Type ContainerType { get; }
    /// <summary>
    /// Gets the test cases.
    /// </summary>
    public IReadOnlyDictionary<string, TestCase> Tests => TestCases;
    /// <summary>
    /// Gets the test results.
    /// </summary>
    public IReadOnlyList<TestCaseResult> Results => TestResults.AsReadOnly();
    /// <summary>
    /// Gets the <see cref="InitializeContainer"/> delegate.
    /// </summary>
    public InitializeContainer InitializeContainer { get; set; }
    /// <summary>
    /// Gets the <see cref="CleanUpContainer"/> delegate.
    /// </summary>
    public CleanUpContainer CleanUpContainer { get; set; }
    /// <summary>
    /// Gets the <see cref="TestSetUp"/> delegate.
    /// </summary>
    public TestSetUp TestSetUp { get; set; }
    /// <summary>
    /// Gets the <see cref="TestTearDown"/> delegate.
    /// </summary>
    public TestTearDown TestTearDown { get; set; }
    /// <summary>
    /// Gets the <see cref="TestContext"/> property.
    /// </summary>
    public PropertyInfo TestContextProperty { get; set; }
    /// <summary>
    /// Gets the <see cref="AssignTestContext"/> delegate.
    /// </summary>
    public AssignTestContext AssignContextDelegate { get; set; }

    public TestContainerInfo(Type containerType)
    {
        ContainerType = containerType ?? throw new ArgumentNullException(nameof(containerType));
        Name = containerType.Name;

        var initContainer = GetDelegate<InitializeContainer>(containerType, typeof(ContainerInitializeAttribute), BindingFlags.Static | BindingFlags.Public, typeof(TestContext));
        InitializeContainer = initContainer ?? ((tc) => { });

        var cleanUpContainer = GetDelegate<CleanUpContainer>(containerType, typeof(ContainerCleanUpAttribute), BindingFlags.Static | BindingFlags.Public);
        CleanUpContainer = cleanUpContainer ?? (() => { });

        var testSetUp = GetDelegate<TestSetUp>(containerType, typeof(SetUpAttribute), BindingFlags.Instance | BindingFlags.Public);
        TestSetUp = testSetUp ?? (() => { });

        var testTearDown = GetDelegate<TestTearDown>(containerType, typeof(TearDownAttribute), BindingFlags.Instance | BindingFlags.Public);
        TestTearDown = testTearDown ?? (() => { });

        // For the TestContext property, without using an attribute
        var assignContext = GetDelegate<AssignTestContext>(containerType, "TestContext", BindingFlags.Static | BindingFlags.Public);
        AssignContextDelegate = assignContext ?? ((tc) => { });

        TestResults.AddRange(TestCases.Select(tc => new TestCaseResult() { Name = tc.Key, ContainerName = Name }));
    }

    /// <summary>
    /// Add the test cases.
    /// </summary>
    /// <param name="testMethodsInfo">The test methods info.</param>
    internal void AddTests(IEnumerable<MethodInfo> testMethodsInfo)
    {
        foreach (var method in testMethodsInfo)
        {
            // Convert MethodInfo to TestCase delegate
            var testCase = CreateDelegate<TestCase>(ContainerType, m => m == method, BindingFlags.Instance | BindingFlags.Public);
            if (testCase != null)
            {
                TestCases.Add(method.Name, testCase);
            }
        }
    }
    /// <summary>
    /// Get the test result information.
    /// </summary>
    /// <param name="testName">The test name.</param>
    /// <param name="result">The test case result.</param>
    /// <returns>TRUE if the test result information is found; otherwise, FALSE.</returns>
    internal bool TryGetResultInfo(string testName, out TestCaseResult result)
    {
        result = TestResults.FirstOrDefault(r => r.Name == testName);
        return result != null;
    }

    private TDelegate GetDelegate<TDelegate>(Type type, Type attributeType, BindingFlags bindingFlags, params Type[] parameterTypes) where TDelegate : Delegate
    {
        return CreateDelegate<TDelegate>(type, m => m.GetCustomAttribute(attributeType) != null, bindingFlags, parameterTypes);
    }
    private TDelegate GetDelegate<TDelegate>(Type type, string methodName, BindingFlags bindingFlags, params Type[] parameterTypes) where TDelegate : Delegate
    {
        return CreateDelegate<TDelegate>(type, m => m.Name == methodName, bindingFlags, parameterTypes);
    }
    private TDelegate CreateDelegate<TDelegate>(Type type, Func<MethodInfo, bool> methodFilter, BindingFlags bindingFlags, params Type[] parameterTypes) where TDelegate : Delegate
    {
        var method = type.GetMethods(bindingFlags)
            .Where(methodFilter)
            .Where(m => m.GetParameters().Select(p => p.ParameterType).SequenceEqual(parameterTypes))
            .FirstOrDefault();


        if (method != null)
        {
            if (parameterTypes?.Any() == true)
            {
                return CreateWrappedDelegate<TDelegate>(method);
            }
            else
            {
                // Handle TestContext property
                if (method.ReturnType == typeof(TestContext))
                {
                    return CreateWrappedDelegate<TDelegate>(method);
                }
                // For other (TestCase) methods with no parameters
                if (method.GetParameters().Length == 0)
                {
                    return CreateWrappedDelegate<TDelegate>(method);
                }
            }
        }

        return null;
    }
    private TDelegate CreateWrappedDelegate<TDelegate>(MethodInfo method) where TDelegate : Delegate
    {
        if (method.IsStatic)
        {
            return method.CreateDelegate<TDelegate>(null);
        }
        else
        {
            return method.CreateDelegate<TDelegate>(Target);
        }
    }
}
