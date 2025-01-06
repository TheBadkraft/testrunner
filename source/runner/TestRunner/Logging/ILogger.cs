namespace MindForge.TestRunner.Logging
{
    /// <summary>
    /// Defines methods for logging messages.
    /// </summary>
    public interface ILogger : ILoggerStrategy, IDisposable
    {
        /// <summary>
        /// Shuts down the logger and releases any resources used.
        /// </summary>
        void Shutdown();
    }
}