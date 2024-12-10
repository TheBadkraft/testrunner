using MindForge.TestRunner.Logging;

namespace testrunner.testing;

internal class TestLogger : ILogger
{
    const string LOG_DELIM = "*** ==========  TEST LOG  ========== ***";

    private StringBuilder logString = new();

    public void Log(DebugLevel debugLevel, string message)
    {
        switch (debugLevel)
        {
            case DebugLevel.Verbose:
                message = LogFormat.Debug(message);

                break;
            case DebugLevel.Default:
                message = LogFormat.Info(message);

                break;
            case DebugLevel.Warning:
                message = LogFormat.Warning(message);

                break;
            case DebugLevel.Error:
                message = LogFormat.Error(message);

                break;
            case DebugLevel.Fatal:
                message = LogFormat.Fatal(message);

                break;
            case DebugLevel.Test:
                message = LogFormat.Test(message);

                break;
        }

        // Add a delimiter to separate log messages
        logString.AppendLine(LOG_DELIM);
        logString.AppendLine(message);
        logString.AppendLine(LOG_DELIM);
    }

    public void Flush()
    {
        Debug.WriteLine($"{(logString.Length > 0 ? logString.ToString() : "*** No logs ***")}");
        logString.Clear();
    }
    public void Shutdown()
    {
        Flush();
    }
    public void Dispose()
    {
        Flush();
    }
}