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
    /// Get the <see cref="Logger"/>
    /// </summary>
    private FileLogger Logger => (FileLogger)TestContext.Logger;
    /// <summary>
    /// Get the current test name.
    /// </summary>
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

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

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

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

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
            message = message ?? "Object is null";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the value is null.
    /// </summary>
    /// <param name="value">The value to assert.</param>
    /// <param name="message">Optional message to log if the value is not null.</param>
    public static void IsNull(object value, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (value != null)
        {
            message = message ?? "Object is not null";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

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

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the expected value is not equal to the actual value.
    /// </summary>
    /// <typeparam name="T">Type of the values.</typeparam>
    /// <param name="expected">The expected value.</param>
    /// <param name="actual">The actual value.</param>
    /// <param name="message">Optional message to log if the values are equal.</param>
    public static void AreNotEqual<T>(T expected, T actual, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (EqualityComparer<T>.Default.Equals(expected, actual))
        {
            message = message ?? $"Expected: {expected}, Actual: {actual}";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the expected value is the same as the actual value.
    /// </summary>
    /// <typeparam name="T">Type of the values.</typeparam>
    /// <param name="expected">The expected value.</param>
    /// <param name="actual">The actual value.</param>
    /// <param name="message">Optional message to log if the values are not the same.</param>
    public static void AreSame<T>(T expected, T actual, string message = null) where T : class
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (!ReferenceEquals(expected, actual))
        {
            message = message ?? $"Expected: {expected}, Actual: {actual}";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the expected value is not the same as the actual value.
    /// </summary>
    /// <typeparam name="T">Type of the values.</typeparam>
    /// <param name="expected">The expected value.</param>
    /// <param name="actual">The actual value.</param>
    /// <param name="message">Optional message to log if the values are not the same.</param>
    public static void AreNotSame<T>(T expected, T actual, string message = null) where T : class
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (ReferenceEquals(expected, actual))
        {
            message = message ?? $"Expected: {expected}, Actual: {actual}";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the value is null.
    /// </summary>
    /// <typeparam name="T">Type of the values.</typeparam>
    /// <param name="value">The value to assert.</param>
    /// <param name="message">Optional message to log if the values are not the same.</param>
    public static void IsNull<T>(T value, string message = null) where T : class
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (value != null)
        {
            message = message ?? "Value is not null";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the value is null.
    /// </summary>
    /// <typeparam name="T">Type of the values.</typeparam>
    /// <param name="value">The value to assert.</param>
    /// <param name="message">Optional message to log if the values are not the same.</param>
    public static void IsNotNull<T>(T value, string message = null) where T : class
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (value == null)
        {
            message = message ?? "Value is null";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }

    /// <summary>
    /// Asserts that the action will throw an expected exception.
    /// </summary>
    /// <typeparam name="T">The exception type expected.</typeparam>
    /// <param name="action">The action to execute</param>
    /// <param name="message">Optional message to log if the exception is not thrown.</param>
    public static void ThrowException<T>(Action action, string message = null) where T : Exception
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        try
        {
            action();
        }
        catch (T)
        {
            tcResult.Outcome = TestResult.Pass;
            return;
        }
        catch (Exception ex)
        {
            message = message ?? $"Expected exception of type {typeof(T).Name}, but caught exception of type {ex.GetType().Name}";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        message = message ?? $"Expected exception of type {typeof(T).Name}, but no exception was thrown";
        tcResult.Outcome = TestResult.Fail;
        tcResult.Message = message;
        tcResult.IsInterrupted = true;

        Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";
    }

    #region Collection Asserts
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

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

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

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the collection contains the expected value.
    /// </summary>
    /// <typeparam name="T">Type of the collection.</typeparam>
    /// <param name="collection">The collection to assert.</param>
    /// <param name="func">The function to determine if the collection contains the expected value.</param>
    /// <param name="message">Optional message to log if the collection does not contain the expected value.</param>
    public static void Contains<T>(IEnumerable<T> collection, Func<T, bool> func, string message = null)
    {
        //  Assert.Contains(LogSubject.Observers, x => x == Logger)
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (!collection.Any(func))
        {
            message = message ?? "Collection does not contain the expected value";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the array contains the expected value.
    /// </summary>
    /// <typeparam name="T">Type of the array.</typeparam>
    /// <param name="array">The array to assert.</param>
    /// <param name="func">The function to determine if the array contains the expected value.</param>
    /// <param name="message">Optional message to log if the array does not contain the expected value.</param>
    public static void Contains<T>(T[] array, Func<T, bool> func, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (!array.Any(func))
        {
            message = message ?? "Array does not contain the expected value";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the collection does not contain the expected value.
    /// </summary>
    /// <typeparam name="T">Type of the collection.</typeparam>
    /// <param name="collection">The collection to assert.</param>
    /// <param name="func">The function to determine if the collection contains the expected value.</param>
    /// <param name="message">Optional message to log if the collection does not contain the expected value.</param>
    public static void DoesNotContain<T>(IEnumerable<T> collection, Func<T, bool> func, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (collection.Any(func))
        {
            message = message ?? "Collection contains the expected value";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the array does not contain the expected value.
    /// </summary>
    /// <typeparam name="T">Type of the array.</typeparam>
    /// <param name="array">The array to assert.</param>
    /// <param name="func">The function to determine if the array contains the expected value.</param>
    /// <param name="message">Optional message to log if the array does not contain the expected value.</param>
    public static void DoesNotContain<T>(T[] array, Func<T, bool> func, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (array.Any(func))
        {
            message = message ?? "Array contains the expected value";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the collection contains all the expected values.
    /// </summary>
    /// <typeparam name="T">Type of the collection.</typeparam>
    /// <param name="collection">The collection to assert.</param>
    /// <param name="expected">The expected values.</param>
    /// <param name="message">Optional message to log if the collection does not contain all the expected values.</param>
    public static void ContainsAll<T>(IEnumerable<T> collection, IEnumerable<T> expected, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (!expected.All(x => collection.Contains(x)))
        {
            message = message ?? $"Expected: {string.Join(", ", expected)}, Actual: {string.Join(", ", collection)}";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the array contains all the expected values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T">Type of the collection.</typeparam>
    /// <param name="array">The collection to assert.</param>
    /// <param name="expected">The expected values.</param>
    /// <param name="message">Optional message to log if the collection does not contain all the expected values.</param>
    public static void ContainsAll<T>(T[] array, IEnumerable<T> expected, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (!expected.All(x => array.Contains(x)))
        {
            message = message ?? $"Expected: {string.Join(", ", expected)}, Actual: {string.Join(", ", array)}";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the collection does not contain all the expected values.
    /// </summary>
    /// <typeparam name="T">Type of the collection.</typeparam>
    /// <param name="collection">The collection to assert.</param>
    /// <param name="expected">The expected values.</param>
    /// <param name="message">Optional message to log if the collection contains all the expected values.</param>
    public static void DoesNotContainAll<T>(IEnumerable<T> collection, IEnumerable<T> expected, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (expected.All(x => collection.Contains(x)))
        {
            message = message ?? $"Expected: {string.Join(", ", expected)}, Actual: {string.Join(", ", collection)}";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the array does not contain all the expected values.
    /// </summary>
    /// <typeparam name="T">Type of the collection.</typeparam>
    /// <param name="array">The collection to assert.</param>
    /// <param name="expected">The expected values.</param>
    /// <param name="message">Optional message to log if the collection contains all the expected values.</param>
    public static void DoesNotContainAll<T>(T[] array, IEnumerable<T> expected, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (expected.All(x => array.Contains(x)))
        {
            message = message ?? $"Expected: {string.Join(", ", expected)}, Actual: {string.Join(", ", array)}";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

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

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

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

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the collection contains any of the expected values.
    /// </summary>
    /// <typeparam name="T">Type of the collection.</typeparam>
    /// <param name="collection">The collection to assert.</param>
    /// <param name="expected">The expected values.</param>
    /// <param name="message">Optional message to log if the collection does not contain any of the expected values.</param>
    public static void ContainsAny<T>(IEnumerable<T> collection, IEnumerable<T> expected, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (!expected.Any(x => collection.Contains(x)))
        {
            message = message ?? $"Expected: {string.Join(", ", expected)}, Actual: {string.Join(", ", collection)}";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the array contains any of the expected values.
    /// </summary>
    /// <typeparam name="T">Type of the collection.</typeparam>
    /// <param name="array">The collection to assert.</param>
    /// <param name="expected">The expected values.</param>
    /// <param name="message">Optional message to log if the collection does not contain any of the expected values.</param>
    public static void ContainsAny<T>(T[] array, IEnumerable<T> expected, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (!expected.Any(x => array.Contains(x)))
        {
            message = message ?? $"Expected: {string.Join(", ", expected)}, Actual: {string.Join(", ", array)}";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the collection does not contain any of the expected values.
    /// </summary>
    /// <typeparam name="T">Type of the collection.</typeparam>
    /// <param name="collection">The collection to assert.</param>
    /// <param name="expected">The expected values.</param>
    /// <param name="message">Optional message to log if the collection does not contain any of the expected values.</param>
    public static void DoesNotContainAny<T>(IEnumerable<T> collection, IEnumerable<T> expected, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (expected.Any(x => collection.Contains(x)))
        {
            message = message ?? $"Expected: {string.Join(", ", expected)}, Actual: {string.Join(", ", collection)}";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    /// <summary>
    /// Asserts that the array does not contain any of the expected values.
    /// </summary>
    /// <typeparam name="T">Type of the collection.</typeparam>
    /// <param name="collection">The collection to assert.</param>
    /// <param name="expected">The expected values.</param>
    /// <param name="message">Optional message to log if the collection does not contain any of the expected values.</param>
    public static void DoesNotContainAny<T>(T[] array, IEnumerable<T> expected, string message = null)
    {
        if (!StartTest(out TestCaseResult tcResult))
        {
            return;
        }
        if (expected.Any(x => array.Contains(x)))
        {
            message = message ?? $"Expected: {string.Join(", ", expected)}, Actual: {string.Join(", ", array)}";
            tcResult.Outcome = TestResult.Fail;
            tcResult.Message = message;
            tcResult.IsInterrupted = true;

            Instance.TestContext.ErrorMessage = $"Test terminated: {tcResult}";

            return;
        }

        tcResult.Outcome = TestResult.Pass;
    }
    #endregion Collection Asserts

    #region Miscellaneous Asserts
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
    #endregion Miscellaneous Asserts

    private static bool StartTest(out TestCaseResult tcResult)
    {
        if (!Instance.TestContext.ContainerInfo.TryGetResultInfo(Instance.CurrentTest, out tcResult))
        {
            throw new MissingMethodException();
        }

        Instance.TestContext.ErrorMessage = string.Empty;

        return !tcResult.IsInterrupted;
    }

}
