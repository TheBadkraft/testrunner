``` c#
namespace MindForge.TestRunner.Core
{
    public static class Assert
    {
        // Equality and Inequality
        public static void AreEqual<T>(T expected, T actual);
        public static void AreEqual<T>(T expected, T actual, string message);
        public static void AreNotEqual<T>(T notExpected, T actual);
        public static void AreNotEqual<T>(T notExpected, T actual, string message);

        // Reference Equality
        public static void AreSame(object expected, object actual);
        public static void AreSame(object expected, object actual, string message);
        public static void AreNotSame(object notExpected, object actual);
        public static void AreNotSame(object notExpected, object actual, string message);

        // Boolean Assertions
        public static void IsTrue(bool condition);
        public static void IsTrue(bool condition, string message);
        public static void IsFalse(bool condition);
        public static void IsFalse(bool condition, string message);

        // Null Checks
        public static void IsNull(object value);
        public static void IsNull(object value, string message);
        public static void IsNotNull(object value);
        public static void IsNotNull(object value, string message);

        // Type Checks
        public static void IsInstanceOfType(object value, Type expectedType);
        public static void IsInstanceOfType(object value, Type expectedType, string message);
        public static void IsNotInstanceOfType(object value, Type notExpectedType);
        public static void IsNotInstanceOfType(object value, Type notExpectedType, string message);

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
        public static void AllItemsAreInstancesOf(IEnumerable collection, Type expectedType);
        public static void AllItemsAreInstancesOf(IEnumerable collection, Type expectedType, string message);
        public static void AllItemsAreUnique<T>(IEnumerable<T> collection);
        public static void AllItemsAreUnique<T>(IEnumerable<T> collection, string message);

        // Exception Assertions
        public static void Throws<TException>(Action action) where TException : Exception;
        public static void Throws<TException>(Action action, string message) where TException : Exception;
        public static void DoesNotThrow(Action action);
        public static void DoesNotThrow(Action action, string message);

        // Fail Test
        public static void Fail();
        public static void Fail(string message);
    }
}
```
