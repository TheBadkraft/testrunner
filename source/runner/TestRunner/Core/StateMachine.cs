using System;

namespace MindForge.TestRunner.Core;

/// <summary>
/// Represents an abstract state machine that manages transitions between states.
/// </summary>
/// <typeparam name="TState">The type of the state enumeration.</typeparam>
public abstract class StateMachine<TState> where TState : struct, IConvertible, IComparable, IFormattable
{
    /// <summary>
    /// Gets or sets the state handler responsible for managing state transitions.
    /// </summary>
    protected StateHandler<TState> StateHandler { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StateMachine{TState}"/> class with the specified initial state and state handler.
    /// </summary>
    /// <param name="stateHandler">The state handler responsible for managing state transitions.</param>
    protected StateMachine(StateHandler<TState> stateHandler)
    {
        //  we're not expecting a null state handler
        StateHandler = stateHandler;
    }

    /// <summary>
    /// Transitions the state machine to a new state.
    /// </summary>
    /// <param name="newState">The new state to transition to.</param>
    /// <param name="message">The message indicating the result of the transition.</param>
    /// <returns>TRUE if the transition was successful; otherwise, FALSE.</returns>
    public bool TransitionTo(TState newState, out string message)
    {
        message = string.Empty;
        if (!Enum.IsDefined(typeof(TState), newState))
        {
            message = $"The value {newState} is not a valid state of the {typeof(TState).Name} enumeration.";
            message = $"[Argument Error] {message}";
            return false;
        }

        if (StateHandler.TransitionTo(newState))
        {
            OnStateChanged(newState);
        }
        else
        {
            message = $"Cannot transition from {StateHandler.GetCurrentState()} to {newState}";
            return false;
        }

        return true;
    }

    /// <summary>
    /// Called when the state of the state machine has changed.
    /// </summary>
    /// <param name="newState"></param>
    protected virtual void OnStateChanged(TState newState)
    {
        // Override this in derived classes for state-specific actions
    }
}
