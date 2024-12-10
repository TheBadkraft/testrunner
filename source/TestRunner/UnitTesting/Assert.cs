namespace MindForge.TestRunner.UnitTesting;

public class Assert
{
    private static readonly Lazy<Assert> instance = new(() => new());

    internal static Assert Instance => instance.Value;

    internal Logger Logger { get; set; }

    /// <summary>
    /// Asserts that the condition is true.
    /// </summary>
    /// <param name="condition">The condition to assert.</param>
    /// <param name="message">Optional message to log if the condition is not true.</param>
    public static void IsTrue(bool condition, string message = null)
    {
        if (!condition)
        {
            message = message ?? "Condition is not true";
            Instance.Logger.Log(DebugLevel.Test, message);
        }
    }
    /// <summary>
    /// Asserts that the condition is false.
    /// </summary>
    /// <param name="condition">The condition to assert.</param>
    /// <param name="message">Optional message to log if the condition is not false.</param>
    public static void IsFalse(bool condition, string message = null)
    {
        if (condition)
        {
            message = message ?? "Condition is not false";
            Instance.Logger.Log(DebugLevel.Test, message);
        }
    }
    /// <summary>
    /// Asserts that the value is not null.
    /// </summary>
    /// <param name="value">The value to assert.</param>
    /// <param name="message">Optional message to log if the value is null.</param>
    public static void IsNotNull(object value, string message = null)
    {
        if (value == null)
        {
            message = message ?? "Value is null";
            Instance.Logger.Log(DebugLevel.Test, message);
        }
    }
    /// <summary>
    /// Asserts that the collection is empty.
    /// </summary>
    /// <typeparam name="T">Type of the collection.</typeparam>
    /// <param name="collection">The collection to assert.</param>
    /// <param name="message">Optional message to log if the collection is not empty.</param>
    public static void IsEmpty<T>(IEnumerable<T> collection, string message = null)
    {
        if (collection?.Any() == true)
        {
            message = message ?? "Collection is not empty";
            Instance.Logger.Log(DebugLevel.Test, message);
        }
    }
    /// <summary>
    /// Asserts that the collection is not empty.
    /// </summary>
    /// <typeparam name="T">Type of the collection.</typeparam>
    /// <param name="collection">the collection to assert.</param>
    /// <param name="message">Optional message to log if the collection is empty.</param>
    public static void IsNotEmpty<T>(IEnumerable<T> collection, string message = null)
    {
        if (collection?.Any() == false)
        {
            message = message ?? "Collection is empty";
            Instance.Logger.Log(DebugLevel.Test, message);
        }
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
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
        {
            message = message ?? $"Expected: {expected}, Actual: {actual}";
            Instance.Logger.Log(DebugLevel.Test, message);
        }
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
        if (!expected.SequenceEqual(actual))
        {
            message = message ?? $"Expected: {string.Join(", ", expected)}, Actual: {string.Join(", ", actual)}";
            Instance.Logger.Log(DebugLevel.Test, message);
        }
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
        if (!expected.SequenceEqual(actual))
        {
            message = message ?? $"Expected: {string.Join(", ", expected)}, Actual: {string.Join(", ", actual)}";
            Instance.Logger.Log(DebugLevel.Test, message);
        }
    }
}
