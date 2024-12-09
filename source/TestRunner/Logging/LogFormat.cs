
namespace MindForge.TestRunner.Logging;

/// <summary>
/// Provides methods to format log messages with a timestamp and log level.
/// </summary>
public static class LogFormat
{
    private const string DEBUG = "DEBUG";
    private const string INFO = "INFO";
    private const string WARNING = "WARNING";
    private const string ERROR = "ERROR";
    private const string FATAL = "FATAL";
    private const string TEST = "TEST";
    private const int LENGTH = 7;
    private const int ALIGNMENT = -LENGTH;

    /// <summary>
    /// Formats the current timestamp in "HH:mm:ss.fff" format.
    /// </summary>
    /// <returns>A string representing the current timestamp.</returns>
    private static string FormatTimeStamp() => DateTime.Now.ToString("HH:mm:ss.fff");

    /// <summary>
    /// Formats a debug log message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <returns>A formatted log message with a "DEBUG" level.</returns>
    public static string Debug(string message) => $"[{FormatTimeStamp()}] [{DEBUG,ALIGNMENT}]: {message}";
    /// <summary>
    /// Formats an informational log message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <returns>A formatted log message with an "INFO" level.</returns>
    public static string Info(string message) => $"[{FormatTimeStamp()}] [{INFO,ALIGNMENT}]: {message}";
    /// <summary>
    /// Formats a warning log message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <returns>A formatted log message with a "WARNING" level.</returns>
    public static string Warning(string message) => $"[{FormatTimeStamp()}] [{WARNING,ALIGNMENT}]: {message}";
    /// <summary>
    /// Formats an error log message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <returns>A formatted log message with an "ERROR" level.</returns>
    public static string Error(string message) => $"[{FormatTimeStamp()}] [{ERROR,ALIGNMENT}]: {message}";
    /// <summary>
    /// Formats a fatal log message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <returns>A formatted log message with a "FATAL" level.</returns>
    public static string Fatal(string message) => $"[{FormatTimeStamp()}] [{FATAL,ALIGNMENT}]: {message}";
    /// <summary>
    /// Formats a test log message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <returns>A formatted log message with a "TEST" level.</returns>
    public static string Test(string message) => $"[{FormatTimeStamp()}] [{TEST,ALIGNMENT}]: {message}";
}