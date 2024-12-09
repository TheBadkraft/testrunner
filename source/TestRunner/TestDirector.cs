
using System.Text.Json;
using MindForge.TestRunner.Core;

/// <summary>
/// The <c>TestDirector</c> class orchestrates the test execution process by managing the state machine
/// and coordinating the test detection, execution, and auditing phases.
/// </summary>
public class TestDirector : StateMachine<RunnerState>, IDisposable
{
    private const string JSON_CONFIG = "runner-config.json";
    private readonly TestDetector detector;
    private readonly TestExecutor executor;
    private readonly TestAuditor auditor;
    private IEnumerable<ProjectInfo> projects;
    private RunnerConfig config;
    private IEnumerable<TestCaseResult> TestResults;

    private RunnerStateHandler Handler => (RunnerStateHandler)StateHandler;
    /// <summary>
    /// Gets the logger instance for logging activities.
    /// </summary>
    private ILogger Logger { get; init; }

    /// <summary>
    /// Indicates whether the test director is ready to begin detection,
    /// execution, and auditing test containers, tests, and results.
    /// </summary>
    public bool IsReady => StateHandler.GetCurrentState() == RunnerState.Ready;
    /// <summary>
    /// Indicates whether the test run is complete.
    /// </summary>
    public bool IsDone => StateHandler.GetCurrentState() == RunnerState.Exit;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestDirector"/> class.
    /// </summary>
    /// <param name="logger">The logger instance for logging activities.</param>
    public TestDirector(ILogger logger) : base(new RunnerStateHandler(logger))
    {
        Logger = logger;

        if (!LoadConfiguration())
        {
            return;
        }
        Assert.Instance.Logger = logger as Logger;

        detector = new TestDetector(Logger, config);
        executor = new TestExecutor(Logger);
        auditor = new TestAuditor(Logger);

        ChangeState(RunnerState.Ready);
    }
    /// <summary>
    /// Changes the current state of the test director to the specified new state.
    /// </summary>
    /// <param name="newState">The new state to transition to.</param>
    public void ChangeState(RunnerState newState)
    {
        if (!TransitionTo(newState, out string message))
        {
            Logger.Log(DebugLevel.Error, message);
        }
    }
    /// <summary>
    /// Starts the test execution process by processing the state machine.
    /// </summary>
    public void Run()
    {
        //  process the state machine
        if (!IsReady)
        {
            //  log error status ... possibly fatal if we don't know why
            Logger.Log(DebugLevel.Error, "TestRunner.Director is not ready to run.");
            return;
        }
        while (!IsDone)
        {
            // Instead of manually handling state transitions here, we let the state machine and handler manage it
            if (!TransitionTo(Handler.NextState(), out string message))
            {
                Logger.Log(DebugLevel.Error, message);
                break;
            }
        }
    }

    protected override void OnStateChanged(RunnerState newState)
    {
        switch (newState)
        {
            case RunnerState.Ready:
                Logger.Log(DebugLevel.Default, "TestRunner.Director is ready to run.");

                break;
            case RunnerState.Discovery:
                DiscoverTests();

                break;
            case RunnerState.Running:
                ExecutTests();

                break;
            case RunnerState.Complete:
                AuditResults();

                break;
            case RunnerState.Exit:
                //  save current log to ./test_logs/*.log
                Logger.Shutdown();

                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Loads the configuration from the JSON file.
    /// </summary>
    private bool LoadConfiguration()
    {
        try
        {
            //  load JSON configuration
            string json = File.ReadAllText(JSON_CONFIG);
            config = JsonSerializer.Deserialize<RunnerConfig>(json);
            if (config == null || !(config.Paths?.Any()).Value)
            {
                var message = "Invalid configuration file.";
                Logger.Log(DebugLevel.Error, message);
                return false;
            }

        }
        catch (Exception ex)
        {
            var message = "Error loading configuration file.";
            message = $"{message} {ex.Message}";

            Logger.Log(DebugLevel.Error, message);
            return false;
        }

        return true;
    }
    /// <summary>
    /// Discovers the tests and populates the containers.
    /// </summary>
    private void DiscoverTests()
    {
        //  test discovery: pupulate containers
        if (!detector.DiscoverTests(out projects))
        {
            //  log error status ... possibly fatal if we don't know why
            Logger.Log(DebugLevel.Error, "Test discovery failed.");
            if (!projects.Any())
            {
                ChangeState(RunnerState.Exit);
            }

            //  we have some test projects to run
        }
    }
    /// <summary>
    /// Executes the tests and catalogs the test results.
    /// </summary>
    private void ExecutTests()
    {
        //  test execution: catalog test results
        executor.ExecuteTests(projects, out TestResults);
        //  log each test result
    }
    /// <summary>
    /// Audits the test results and generates a results log.
    /// </summary>
    private void AuditResults()
    {
        //  audit results: generate results log
        auditor.AuditResults(TestResults);
    }
    /// <summary>
    /// Releases all resources used by the <see cref="TestDirector"/> class.
    /// </summary>
    public void Dispose()
    {
        //  release resources ... (???)
    }
}
