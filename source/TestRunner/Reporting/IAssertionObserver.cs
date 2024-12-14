using MindForge.TestRunner.Core;

namespace MindForge.TestRunner.Reporting;

public interface IAssertionObserver
{
    void OnAssertion(TestCaseResult tcResult);
}
