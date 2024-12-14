using MindForge.TestRunner.Core;
using MindForge.TestRunner.Reporting;

namespace MindForge.TestRunner.Logging;

/// <summary>
/// Decorator for logging messages in JSON format.
/// </summary>
public class JsonLogger : ILogger, IAssertionObserver
{
    private const string LOG_DIR = "test_results";

    private readonly ILoggerStrategy _baseStrategy;
    private readonly MemoryStream memory;
    private readonly Utf8JsonWriter writer;

    public JsonLogger(ILoggerStrategy baseStrategy)
    {
        _baseStrategy = baseStrategy;
        memory = new MemoryStream();
        writer = new Utf8JsonWriter(memory, new JsonWriterOptions { Indented = true });
        writer.WriteStartArray(); // Start of JSON array
    }

    /// <summary>
    /// Write a message to the log (not typically supported in this context)
    /// </summary>
    /// <param name="message">Message to write</param>
    public void Write(string message)
    {
        // Not typically used in this context
    }
    /// <summary>
    /// Write a message to the log with a newline character (not typically supported in this context)
    /// </summary>
    /// <param name="message">Message to write</param>
    public void WriteLine(string message)
    {
        // Similar to Write
    }
    /// <summary>
    /// Log a message with a specified debug level
    /// </summary>
    /// <param name="debugLevel"></param>
    /// <param name="message"></param>
    public void Log(DebugLevel debugLevel, string message)
    {
        _baseStrategy.Log(debugLevel, message);
    }

    public void Flush()
    {
        if (writer.CurrentDepth > 0)
        {
            writer.WriteEndObject(); // Close the last object if we're in one
        }
        writer.WriteEndArray(); // End of JSON array
        writer.Flush();
    }

    public void OnAssertion(TestCaseResult tcResult)
    {
        switch (writer.CurrentDepth)
        {
            case 0:
                //  start the array
                writer.WriteStartArray();

                break;
            case 1:
                //  start the object
                writer.WriteStartObject();

                break;
            default:
                //  assumption: we're not more than 1 level deep ???
                writer.WriteEndObject();
                writer.WriteStartObject(); // New object for each log entry

                break;
        }

        writer.WriteString("timestamp", DateTime.Now.ToString("O"));
        writer.WriteString("method", $"{tcResult.ContainerName}.{tcResult.Name}");
        writer.WriteString("result", $"{tcResult.Outcome}");
        writer.WriteString("duration", $"{tcResult.Duration}");
        writer.WriteString("message", tcResult.Message);
    }

    public void Shutdown()
    {
        Flush();
        SaveLogFile();
    }

    /// <summary>
    /// Saves the log file to the log directory.
    /// </summary>
    private void SaveLogFile()
    {
        var logDir = Path.Combine(Directory.GetCurrentDirectory(), LOG_DIR);
        if (!Directory.Exists(logDir))
        {
            Directory.CreateDirectory(logDir);
        }
        try
        {
            var fileName = FileLogger.GenerateLogFileName("json");
            fileName = Path.Combine(logDir, fileName);

            using var fileStream = new FileStream(fileName, FileMode.Create);
            memory.Position = 0;
            memory.CopyTo(fileStream);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }

        writer.Reset();
    }
    public void Dispose()
    {
        writer.Dispose();
        memory.Dispose();
    }
}