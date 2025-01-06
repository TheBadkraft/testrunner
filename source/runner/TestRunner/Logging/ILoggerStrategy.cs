
namespace MindForge.TestRunner.Logging;

/// <summary>
/// Defines a strategy for logging operations.
/// Implementations of this interface should provide specific logging mechanisms.
/// </summary>
public interface ILoggerStrategy
{
    /// <summary>
    /// Write a message to the log.
    /// </summary>
    /// <param name="message">Message to write</param>
    void Write(string message);
    /// <summary>
    /// Writes a message to the log.
    /// </summary>
    /// <param name="debugLevel">The level of debugging information to log.</param>
    /// <param name="message">The message to log.</param>
    void Log(DebugLevel debugLevel, string message);
}
