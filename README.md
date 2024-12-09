Because MSTest is a giant fail ... and seems there is some breakage in VSCode.  
I just needed a little excuse to write my own.

#### **Namespace:** `MindForge.TestRunner.Core`  
``` c#
namespace MindForge.TestRunner.Core
{
    // Enum for Runner States
    public enum RunnerState
    {
        Idle,
        Ready,
        Discovery,
        Running,
        Auditing,
        Complete
    }

    // Abstract State Machine
    public abstract class RunnerStateMachine<TState> where TState : struct, IConvertible, IComparable, IFormattable
    {
        protected RunnerStateMachine(TState initialState, RunnerStateHandler<TState> handler);
        public void TransitionTo(TState newState);
        protected virtual void OnStateEntered(TState newState);
        public TState GetCurrentState();
    }

    // Abstract State Handler
    public abstract class RunnerStateHandler<TState> where TState : struct, IConvertible, IComparable, IFormattable
    {
        protected RunnerStateHandler(ILogger logger);
        public void SetParentStateMachine(RunnerStateMachine<TState> stateMachine);
        public void SetCurrentState(TState newState);
        public TState GetCurrentState();
        public abstract bool CanTransitionTo(TState currentState, TState newState);
        public virtual void OnBeforeTransition(TState oldState, TState newState);
        public virtual void OnAfterTransition(TState newState);
    }

    // Test Director
    public class TestDirector : RunnerStateMachine<RunnerState>, IDisposable
    {
        public TestDirector(ILogger logger);
        public bool IsDone { get; }
        public void ChangeState(RunnerState newState);
        public void Run();
        public void DiscoverTests();
        public void ExecuteTests();
        public void AuditResults();
        public void Dispose();
    }

    {
        public class RunnerConfig
        {
            public string[] Paths { get; set; }
        }
    }

    public class TestDetector
    {
        public TestDetector(ILogger logger);
        public IEnumerable<Type> DiscoverTests();
    }

    public class TestExecutor
    {
        public TestExecutor(ILogger logger);
        public List<TestResult> ExecuteTests(Type testContainer);
    }

    public class TestAuditor
    {
        public TestAuditor(ILogger logger);
        public void AuditResults(IEnumerable<Type> containers);
    }
    // Test Director State Handler
    public class TestDirectorStateHandler : RunnerStateHandler<RunnerState>
    {
        public TestDirectorStateHandler(ILogger logger, TestDetector detector, TestExecutor executor, TestAuditor auditor);
        public override bool CanTransitionTo(RunnerState currentState, RunnerState newState);
        public override void OnAfterTransition(RunnerState newState);
    }
```

#### **Namespace:** `MindForge.TestRunner.UnitTesting`  
``` c#
namespace MindForge.TestRunner.UnitTesting
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TestContainerAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class TestAttribute : Attribute
    {
        public string Description { get; set; }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class SetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class TearDownAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class ContainerInitializeAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class ContainerCleanUpAttribute : Attribute { }

    // Assertion Class
    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual);
        public static void AreEqual<T>(T expected, T actual, string message);
        public static void AreNotEqual<T>(T notExpected, T actual);
        public static void AreNotEqual<T>(T notExpected, T actual, string message);
        public static void AreSame(object expected, object actual);
        public static void AreSame(object expected, object actual, string message);
        public static void AreNotSame(object notExpected, object actual);
        public static void AreNotSame(object notExpected, object actual, string message);
        public static void IsTrue(bool condition);
        public static void IsTrue(bool condition, string message);
        public static void IsFalse(bool condition);
        public static void IsFalse(bool condition, string message);
        public static void IsNull(object value);
        public static void IsNull(object value, string message);
        public static void IsNotNull(object value);
        public static void IsNotNull(object value, string message);
        public static void IsInstanceOfType(object value, Type expectedType);
        public static void IsInstanceOfType(object value, Type expectedType, string message);
        public static void IsNotInstanceOfType(object value, Type notExpectedType);
        public static void IsNotInstanceOfType(object value, Type notExpectedType, string message);
        public static void Fail();
        public static void Fail(string message);
        public static void Throws<TException>(Action action) where TException : Exception;
        public static void Throws<TException>(Action action, string message) where TException : Exception;
        public static void DoesNotThrow(Action action);
        public static void DoesNotThrow(Action action, string message);
        public static void NotImplemented(string message);

        // Collection Assertions
        public static void AreEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual);
        public static void AreEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, string message);
        public static void AreNotEqual<T>(IEnumerable<T> notExpected, IEnumerable<T> actual);
        public static void AreNotEqual<T>(IEnumerable<T> notExpected, IEnumerable<T> actual, string message);
        public static void AreEquivalent<T>(IEnumerable<T> expected, IEnumerable<T> actual);
        public static void AreEquivalent<T>(IEnumerable<T> expected, IEnumerable<T> actual, string message);
        public static void AreNotEquivalent<T>(IEnumerable<T> notExpected, IEnumerable<T> actual);
        public static void AreNotEquivalent<T>(IEnumerable<T> notExpected, IEnumerable<T> actual, string message);
        public static void IsEmpty(IEnumerable collection);
        public static void IsEmpty(IEnumerable collection, string message);
        public static void IsNotEmpty(IEnumerable collection);
        public static void IsNotEmpty(IEnumerable collection, string message);
        public static void Contains<T>(IEnumerable<T> collection, T item);
        public static void Contains<T>(IEnumerable<T> collection, T item, string message);
        public static void DoesNotContain<T>(IEnumerable<T> collection, T item);
        public static void DoesNotContain<T>(IEnumerable<T> collection, T item, string message);
        public static void AllItemsAreInstancesOf<T>(IEnumerable collection, Type expectedType);
        public static void AllItemsAreInstancesOf<T>(IEnumerable collection, Type expectedType, string message);
        public static void AllItemsAreUnique<T>(IEnumerable<T> collection);
        public static void AllItemsAreUnique<T>(IEnumerable<T> collection, string message);
    }
}
}
```

#### **Namespace:** `MindForge.TestRunner.Logging`  
``` c#
namespace MindForge.TestRunner.Logging
{
    public interface ILogger
    {
        void Log(DebugLevel level, string message);
    }

    public enum DebugLevel
    {
        Default,
        Info,
        Warning,
        Error
    }
}
```
