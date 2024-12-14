
Here's how you can implement JsonLoggingStrategy along with the TestLogContainer and TestResultMessage classes:

TestResultMessage Class
First, define the TestResultMessage class:

csharp
public class TestResultMessage
{
    // json property name: time_stamp
    [JsonPropertyName("time_stamp")]
    public DateTime TimeStamp { get; set; }

    // json property name: log_level
    [JsonPropertyName("log_level")]
    public DebugLevel LogLevel { get; set; }

    // json property name: log_entry
    [JsonPropertyName("log_entry")]
    public string LogEntry { get; set; }
}

TestLogContainer Class
Next, define the TestLogContainer which will hold a collection of TestResultMessage:

csharp
public class TestLogContainer
{
    // json property name: results
    [JsonPropertyName("results")]
    public List<TestResultMessage> Results { get; set; } = new List<TestResultMessage>();
}

JsonLoggingStrategy Class
Now, implement the JsonLoggingStrategy:

csharp
using System.Text.Json;
using System.Text;
using System.IO;

public class JsonLoggingStrategy : ChainedStrategy
{
    private readonly TestLogContainer _logContainer = new TestLogContainer();
    private readonly string _fileName;

    public JsonLoggingStrategy(string fileName)
    {
        _fileName = fileName;
    }

    protected override void DoWrite(string message)
    {
        // Since we're dealing with JSON, we'll accumulate messages in DoLog instead.
    }

    protected override void DoWriteLine(string message)
    {
        // Same as DoWrite, we accumulate messages in DoLog.
    }

    protected override void DoLog(DebugLevel debugLevel, string message)
    {
        if (debugLevel >= MinDebugLevel)
        {
            _logContainer.Results.Add(new TestResultMessage
            {
                TimeStamp = DateTime.Now,
                LogLevel = debugLevel,
                LogEntry = message
            });
        }
        _logNext(debugLevel, message); // Pass to next strategy if exists
    }

    public void SaveLog()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(_logContainer, options);
        File.WriteAllText(_fileName, jsonString);
    }

    public void ClearLog()
    {
        _logContainer.Results.Clear();
    }
}

Explanation:
JsonPropertyName: This attribute from System.Text.Json.Serialization is used to specify the JSON property names for serialization, matching your desired JSON structure.
JsonLoggingStrategy:
We accumulate all log messages in a TestLogContainer instead of writing them immediately. This allows for batch writing to a file, which can be more efficient, especially for JSON which might benefit from being written in one go for better structure and performance.
SaveLog() method serializes the container to JSON and writes it to a file. You can call this method after all logging is done, or at specific checkpoints if you prefer.
ClearLog() clears the current logs, useful for starting fresh for another set of tests or sessions.
Usage: When using JsonLoggingStrategy, make sure to call SaveLog() when you want to write the logs to a file. Here's a brief example:

csharp
var jsonLogger = new JsonLoggingStrategy("test_results.log.json");
var compositeLogger = new CompositeLoggerStrategy();
compositeLogger.AddStrategy(jsonLogger);
compositeLogger.Compose();

// Log messages
compositeLogger.Log(DebugLevel.Info, "Test passed");
compositeLogger.Log(DebugLevel.Error, "Test failed");

// Save logs when done
jsonLogger.SaveLog();

This design allows for a structured, JSON-formatted log of test results, which can be easily parsed or analyzed by other systems or tools.
