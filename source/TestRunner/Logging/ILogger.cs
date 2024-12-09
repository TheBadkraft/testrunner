namespace MindForge.TestRunner.Logging
{
    /// <summary>
    /// Defines methods for logging messages.
    /// </summary>
    public interface ILogger : IDisposable
    {
        /// <summary>
        /// Writes a message to the log.
        /// </summary>
        /// <param name="debugLevel">The level of debugging information to log.</param>
        /// <param name="message">The message to log.</param>
        void Log(DebugLevel debugLevel, string message);
        /// <summary>
        /// Shuts down the logger and releases any resources used.
        /// </summary>
        void Shutdown();
    }
}