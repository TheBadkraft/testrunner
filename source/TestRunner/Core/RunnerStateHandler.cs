
using MindForge.TestRunner.Core;

/// <summary>
/// Handles the state transitions for the test director in the test runner.
/// </summary>
/// <remarks>
/// This class is responsible for determining if a transition between states is allowed.
/// </remarks>
/// <param name="logger">The logger instance used for logging information.</param>
internal class RunnerStateHandler : StateHandler<RunnerState>
{
    public RunnerStateHandler(ILogger logger) : base(RunnerState.Idle, logger)
    {
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override bool CanTransitionTo(RunnerState newState)
    {
        switch (CurrentState)
        {
            case RunnerState.Idle when newState == RunnerState.Ready:
                return true;
            case RunnerState.Ready when newState == RunnerState.Discovery:
                return true;
            case RunnerState.Discovery when newState == RunnerState.Running:
                return true;
            case RunnerState.Running when newState == RunnerState.Auditing:
                return true;
            case RunnerState.Auditing when newState == RunnerState.Complete:
                return true;
            case RunnerState.Complete when newState == RunnerState.Exit:
                return true;
            default:
                return false;
        }
    }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void OnAfterTransition(RunnerState newState)
    {
        switch (newState)
        {
            case RunnerState.Discovery:


                break;
            case RunnerState.Running:


                break;
            case RunnerState.Auditing:


                break;
                // ... other cases
        }
    }
    /// <summary>
    /// Gets the next state in the state machine.
    /// </summary>
    /// <returns>The next state in the state machine.</returns>
    public RunnerState NextState()
    {
        // Logic to determine the next state based on the current state
        // This could be simplified if the state handler manages all transitions
        switch (GetCurrentState())
        {
            case RunnerState.Idle: return RunnerState.Ready;
            case RunnerState.Ready: return RunnerState.Discovery;
            case RunnerState.Discovery: return RunnerState.Running;
            case RunnerState.Running: return RunnerState.Auditing;
            case RunnerState.Auditing: return RunnerState.Complete;
            case RunnerState.Complete: return RunnerState.Exit;
            default: return RunnerState.Error;
        }
    }
}