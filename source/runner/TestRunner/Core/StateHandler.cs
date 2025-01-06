using MindForge.TestRunner.Logging;

namespace MindForge.TestRunner.Core;

/// <summary>
/// Abstract base class for handling state transitions in a state machine.
/// </summary>
/// <typeparam name="TState">The type of the state, which must be a struct and implement IConvertible, IComparable, and IFormattable.</typeparam>
public abstract class StateHandler<TState> where TState : struct, IConvertible, IComparable, IFormattable
{
    /// <summary>
    /// Logger instance for logging state transitions.
    /// </summary>
    protected ILogger Logger;

    /// <summary>
    /// Gets the parent state machine associated with this handler.
    /// </summary>
    public StateMachine<TState> Owner { get; private set; }
    /// <summary>
    /// Gets the current state of the state machine.
    /// </summary>
    protected TState CurrentState { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StateHandler{TState}"/> class.
    /// </summary>
    /// <param name="logger">The logger instance to use for logging.</param>
    protected StateHandler(TState beginningState, ILogger logger)
    {
        CurrentState = beginningState;
        //  we're not expecting a null logger
        Logger = logger;
    }

    /// <summary>
    /// Determines whether a transition from the current state to a new state is allowed.
    /// </summary>
    /// <param name="newState">The new state to transition to.</param>
    /// <returns>True if the transition is allowed; otherwise, false.</returns>
    public abstract bool CanTransitionTo(TState newState);

    /// <summary>
    /// Called before a state transition occurs.
    /// </summary>
    /// <param name="newState">The state being transitioned to.</param>
    public virtual void OnBeforeTransition(TState newState) { }

    /// <summary>
    /// Called after a state transition occurs.
    /// </summary>
    /// <param name="newState">The state that has been transitioned to.</param>
    public virtual void OnAfterTransition(TState newState)
    {
        Logger?.Log(DebugLevel.Default, $"State transitioned to {newState}");
    }

    /// <summary>
    /// Sets the parent state machine for this handler.
    /// </summary>
    /// <param name="stateMachine">The state machine to set as the parent.</param>
    public void SetParentStateMachine(StateMachine<TState> stateMachine)
    {
        //  we're not expecting a null state machine
        Owner = stateMachine;
    }
    /// <summary>
    /// Sets the current state of the state machine.
    /// </summary>
    /// <param name="newState">The new state to set as the current state.</param>
    public void SetCurrentState(TState newState)
    {
        CurrentState = newState;
    }
    /// <summary>
    /// Gets the current state of the state machine.
    /// </summary>
    /// <returns>The current state of the state machine.</returns>
    public TState GetCurrentState()
    {
        return CurrentState;
    }

    /// <summary>
    /// Transitions the state machine to a new state.
    /// </summary>
    /// <param name="newState">The new state to transition to.</param>
    /// <returns>TRUE if the transition was successful; otherwise, FALSE.</returns>
    public virtual bool TransitionTo(TState newState)
    {
        if (!CanTransitionTo(newState))
        {
            return false;
        }

        OnBeforeTransition(newState);
        SetCurrentState(newState);
        OnAfterTransition(newState);

        return true;
    }
}
