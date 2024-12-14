using MindForge.TestRunner.Reporting;

namespace MindForge.TestRunner.Core;

/// <summary>
/// Observer for assertion results
/// </summary>
public class TestCaseSubject
{
    private readonly List<IAssertionObserver> _observers = new List<IAssertionObserver>();

    /// <summary>
    /// Subscribe to assertion results
    /// </summary>
    /// <param name="observer">Observer to subscribe</param>
    /// <returns>Observer</returns>
    public IAssertionObserver Subscribe(IAssertionObserver observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }

        return observer;
    }
    /// <summary>
    /// Unsubscribe from assertion results
    /// </summary>
    /// <param name="observer">Observer to unsubscribe</param>
    public void Unsubscribe(IAssertionObserver observer)
    {
        _observers.Remove(observer);
    }

    /// <summary>
    /// Notify assertion result
    /// </summary>
    /// <param name="tcResult">Test case result</param>
    public virtual void NotifyAssertionResult(TestCaseResult tcResult)
    {
        foreach (var observer in _observers)
        {
            observer.OnAssertion(tcResult);
        }
    }
}
