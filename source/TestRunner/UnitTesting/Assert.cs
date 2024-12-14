using MindForge.TestRunner.Core;

namespace MindForge.TestRunner.UnitTesting;

public class Assert
{
    private static readonly Lazy<Assert> instance = new(() => new());

    internal static Assert Instance => instance.Value;

    /// <summary>
    /// Get or set the <see cref="TestContext"/>
    /// </summary>
    internal TestContext TestContext { get; set; }
    /// <summary>
    /// Get or set the <see cref="Logger"/>
    /// </summary>
    private Logger Logger => (Logger)TestContext.Logger;
    private string CurrentTest => TestContext.ContainerInfo.CurrentTest;

    /// <summary>
    /// Asserts that the condition is true.
    /// </summary>
    /// <param name="condition">The condition to assert.</param>
    /// <param name="message">Optional message to log if the condition is not true.</param>
    public static void IsTrue(bool condition, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (!condition)
        {
            message = message ?? "Condition is not true";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }

    /// <summary>
    /// Asserts that the condition is false.
    /// </summary>
    /// <param name="condition">The condition to assert.</param>
    /// <param name="message">Optional message to log if the condition is not false.</param>
    public static void IsFalse(bool condition, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (condition)
        {
            message = message ?? "Condition is not false";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the value is not null.
    /// </summary>
    /// <param name="value">The value to assert.</param>
    /// <param name="message">Optional message to log if the value is null.</param>
    public static void IsNotNull(object value, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (value == null)
        {
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the collection is empty.
    /// </summary>
    /// <typeparam name="T">Type of the collection.</typeparam>
    /// <param name="collection">The collection to assert.</param>
    /// <param name="message">Optional message to log if the collection is not empty.</param>
    public static void IsEmpty<T>(IEnumerable<T> collection, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (collection?.Any() == true)
        {
            message = message ?? "Collection is not empty";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the collection is not empty.
    /// </summary>
    /// <typeparam name="T">Type of the collection.</typeparam>
    /// <param name="collection">the collection to assert.</param>
    /// <param name="message">Optional message to log if the collection is empty.</param>
    public static void IsNotEmpty<T>(IEnumerable<T> collection, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (collection?.Any() == false)
        {
            message = message ?? "Collection is empty";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the expected value is equal to the actual value.
    /// </summary>
    /// <typeparam name="T">Type of the values.</typeparam>
    /// <param name="expected">The expected value.</param>
    /// <param name="actual">The actual value.</param>
    /// <param name="message">Optional message to log if the values are not equal.</param>
    public static void AreEqual<T>(T expected, T actual, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
        {
            message = message ?? $"Expected: {expected}, Actual: {actual}";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the expected collection is equal to the actual collection.
    /// </summary>
    /// <typeparam name="T">Type of the collections.</typeparam>
    /// <param name="expected">The expected collection.</param>
    /// <param name="actual">The actual collection.</param>
    /// <param name="message">Optional message to log if the collections are not equal.</param>
    public static void AreEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (!expected.SequenceEqual(actual))
        {
            message = message ?? $"Expected: {string.Join(", ", expected)}, Actual: {string.Join(", ", actual)}";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the expected array is equal to the actual array.
    /// </summary>
    /// <typeparam name="T">Type of the collections.</typeparam>
    /// <param name="expected">The expected array.</param>
    /// <param name="actual">The actual array.</param>  
    /// <param name="message">Optional message to log if the arrays are not equal.</param>
    public static void AreEqual<T>(T[] expected, T[] actual, string message = null)
    {

        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (!expected.SequenceEqual(actual))
        {
            message = message ?? $"Expected: {string.Join(", ", expected)}, Actual: {string.Join(", ", actual)}";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public static void Fail(string message = null)
    {
        StartTest(out TestCaseResult tcResult);
        message = message ?? "Fail";
        tcResult.Outcome = TestResult.Fail;
        tcResult.Message = message;
        tcResult.IsInterrupted = true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public static void NotImplemented(string message = null)
    {
        StartTest(out TestCaseResult tcResult);
        message = message ?? "Test not implemented";
        tcResult.Outcome = TestResult.Undef;
        tcResult.Message = message;
        tcResult.IsInterrupted = true;
    }


    private static bool StartTest(out TestCaseResult tcResult)
    {
        if (!Instance.TestContext.ContainerInfo.TryGetResultInfo(Instance.CurrentTest, out tcResult))
        {
            throw new MissingMethodException();
        }

        return !tcResult.IsInterrupted;
    }
}
