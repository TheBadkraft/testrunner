using MindForge.TestRunner.Reporting;

namespace MindForge.TestRunner.Core;

public class TestCaseSubject
{
    private readonly List<IAssertionObserver> _observers = new List<IAssertionObserver>();

    public void Subscribe(IAssertionObserver observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void Unsubscribe(IAssertionObserver observer)
    {
        _observers.Remove(observer);
    }

    protected void NotifyAssertionResult(TestCaseResult tcResult)
    {
        foreach (var observer in _observers)
        {
            observer.OnAssertion(tcResult);
        }
    }
}
