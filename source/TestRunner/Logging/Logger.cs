
namespace MindForge.TestRunner.Logging;

/// <summary>
/// General purpose logger for logging messages to the console 
/// standard output, error output, debug trace listener, and a 
/// log file.
/// </summary>
public class Logger : TraceListener, ILogger
{
    private const string LOG_DIR = "test_logs";

    private readonly TextWriter stdOut = Console.Out;
    private readonly TextWriter stdErr = Console.Error;
    private TextWriter writer;

    public string LogFile { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Logger"/> class.
    /// </summary>
    public Logger(string logFilePath)
    {
        var stream = new StreamWriter(LogFile = logFilePath, false);
        // stream.BaseStream.SetLength(0);
        stream.AutoFlush = true;
        writer = stream;

        Console.SetOut(writer);
        Console.SetError(writer);

        Trace.Listeners.Add(this);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="message"></param>
    public override void Write(string message) => writer.Write(message);
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="message"></param>
    public override void WriteLine(string message) => writer.WriteLine(message);
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="debugLevel"></param>
    /// <param name="message"></param>
    public void Log(DebugLevel debugLevel, string message)
    {
        switch (debugLevel)
        {
            case DebugLevel.Verbose:
                WriteLine(LogFormat.Debug(message));

                break;
            case DebugLevel.Default:
                WriteLine(LogFormat.Info(message));

                break;
            case DebugLevel.Warning:
                WriteLine(LogFormat.Warning(message));

                break;
            case DebugLevel.Error:
                WriteLine(LogFormat.Error(message));

                break;
            case DebugLevel.Fatal:
                WriteLine(LogFormat.Fatal(message));

                break;
            case DebugLevel.Test:
                WriteLine(LogFormat.Test(message));

                break;
        }
    }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Shutdown()
    {
        SaveLogFile();
    }

    /// <summary>
    /// Saves the log file to the log directory.
    /// </summary>
    private void SaveLogFile()
    {
        writer.Flush();
        writer.Close();

        var logDir = Path.Combine(Directory.GetCurrentDirectory(), LOG_DIR);
        if (!Directory.Exists(logDir))
        {
            Directory.CreateDirectory(logDir);
        }
        try
        {
            var fileName = GenerateLogFileName();
            File.Move(LogFile, Path.Combine(logDir, fileName));
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }

    }
    /// <summary>
    /// Generates a random log file name.
    /// </summary>
    /// <returns>A random log file name (*.log).</returns>
    private string GenerateLogFileName()
    {
        var chars = "Aa80Bb9C1cDd2EeF3fGg4HhJ5_iK6jLk7MmN8nPo9QpR1qSr2TsU3tVu4WvX5wYx6Zy_7z";
        var random = new Random();
        var fileName = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        return $"{fileName}.log";
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    protected override void Dispose(bool disposing)
    {
        Console.SetOut(stdOut);
        Console.SetError(stdErr);

        Trace.Listeners.Remove(this);

        writer.Dispose();
        writer = null;

        GC.SuppressFinalize(this);

        base.Dispose(disposing);
    }
}
