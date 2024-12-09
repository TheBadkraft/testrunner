namespace MindForge.TestRunner.UnitTesting;

public class Assert
{
    private static readonly Lazy<Assert> instance = new(() => new());

    internal static Assert Instance => instance.Value;

    internal Logger Logger { get; set; }

    public static void IsTrue(bool condition)
    {
        if (!condition)
        {
            var message = "Condition is not true";
            Instance.Logger.Log(DebugLevel.Test, message);
        }
    }
    public static void IsTrue(Func<bool> condition)
    {
        if (!condition())
        {
            var message = "Condition is not true";
            Instance.Logger.Log(DebugLevel.Test, message);
        }
    }

    /*
        public static void AreEqual<T>(T expected, T actual);
        public static void AreEqual<T>(T expected, T actual, string message);
        public static void AreNotEqual<T>(T notExpected, T actual);
        public static void AreNotEqual<T>(T notExpected, T actual, string message);
        public static void AreSame(object expected, object actual);
        public static void AreSame(object expected, object actual, string message);
        public static void AreNotSame(object notExpected, object actual);
        public static void AreNotSame(object notExpected, object actual, string message);
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
    */
}
