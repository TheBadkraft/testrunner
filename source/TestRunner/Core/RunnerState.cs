namespace MindForge.TestRunner.Core;

/// <summary>
/// Represents the various states that a test runner can be in.
/// </summary>
public enum RunnerState
{
    /// <summary>
    /// The test runner is idle and not performing any actions.
    /// </summary>
    Idle,
    /// <summary>
    /// The test runner is ready to begin the test execution process.
    /// </summary>
    Ready,
    /// <summary>
    /// The test runner is discovering tests.
    /// </summary>
    Discovery,
    /// <summary>
    /// The test runner is currently running tests.
    /// </summary>
    Running,
    /// <summary>
    /// The test runner is auditing the test results.
    /// </summary>
    Auditing,
    /// <summary>
    /// The test runner has completed all actions.
    /// </summary>
    Complete,
    /// <summary>
    /// The test runner has encountered an error.
    /// </summary>
    Error,
    /// <summary>
    /// The test runner is exiting.
    /// </summary>
    /// <remarks>
    /// This state is used to indicate that the test runner is shutting down.
    /// If the test runner encounters an error, it will transition to the Error state 
    /// for graceful shutdown.
    /// </remarks>
    Exit
}
