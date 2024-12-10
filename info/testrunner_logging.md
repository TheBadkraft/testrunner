
Here's the source code for the various components you've described in your logging framework:

ILoggerStrategy Interface
csharp
public interface ILoggerStrategy
{
    void Write(string message);
    void WriteLine(string message);
    void Log(DebugLevel debugLevel, string message);
}

ChainedStrategy Base Class
csharp
public abstract class ChainedStrategy : ILoggerStrategy
{
    // Delegates for chaining strategies
    private WriteHandler _writeNext = (message) => { };
    private WriteLineHandler _writeLineNext = (message) => { };
    private LogHandler _logNext = (debugLevel, message) => { };

    // Abstract methods for each strategy to implement its specific behavior
    protected abstract void DoWrite(string message);
    protected abstract void DoWriteLine(string message);
    protected abstract void DoLog(DebugLevel debugLevel, string message);

    // Implement ILoggerStrategy methods
    public void Write(string message) { DoWrite(message); _writeNext(message); }
    public void WriteLine(string message) { DoWriteLine(message); _writeLineNext(message); }
    public void Log(DebugLevel debugLevel, string message) { DoLog(debugLevel, message); _logNext(debugLevel, message); }

    // Method to set the next strategy in the chain
    public void SetNextStrategy(ILoggerStrategy nextStrategy)
    {
        if (nextStrategy == null)
        {
            _writeNext = (message) => { };
            _writeLineNext = (message) => { };
            _logNext = (debugLevel, message) => { };
        }
        else
        {
            _writeNext = nextStrategy.Write;
            _writeLineNext = nextStrategy.WriteLine;
            _logNext = nextStrategy.Log;
        }
    }

    // Delegate signatures for clarity and type safety
    public delegate void WriteHandler(string message);
    public delegate void WriteLineHandler(string message);
    public delegate void LogHandler(DebugLevel debugLevel, string message);
}

CompositeLoggerStrategy Class
csharp
public class CompositeLoggerStrategy : ILoggerStrategy
{
    private List<ILoggerStrategy> _strategies = new List<ILoggerStrategy>();
    private ILoggerStrategy _headStrategy;

    public void AddStrategy(ILoggerStrategy strategy) { _strategies.Add(strategy); }
    public void RemoveStrategy(ILoggerStrategy strategy) { _strategies.Remove(strategy); }
    public void ReplaceStrategy(ILoggerStrategy oldStrategy, ILoggerStrategy newStrategy)
    {
        int index = _strategies.IndexOf(oldStrategy);
        if (index != -1) _strategies[index] = newStrategy;
    }

    public void Compose()
    {
        _headStrategy = null;
        ILoggerStrategy current = null;

        foreach (var strategy in _strategies)
        {
            if (_headStrategy == null)
            {
                _headStrategy = strategy;
                current = strategy;
            }
            else
            {
                current.SetNextStrategy(strategy);
                current = strategy;
            }
        }
        current?.SetNextStrategy(null); // Last strategy points to null
    }

    // Implement ILoggerStrategy methods by delegating to the head of the chain
    public void Write(string message) => _headStrategy?.Write(message);
    public void WriteLine(string message) => _headStrategy?.WriteLine(message);
    public void Log(DebugLevel debugLevel, string message) => _headStrategy?.Log(debugLevel, message);
}

DebugLevel Enum (assumed to be used in the Log methods)
csharp
public enum DebugLevel
{
    Debug,
    Info,
    Warning,
    Error,
    Fatal
}

Concrete Strategy Implementations (as examples provided earlier)
csharp
public class FileLoggerStrategy : ChainedStrategy
{
    private readonly string _filePath;

    public FileLoggerStrategy(string filePath)
    {
        _filePath = filePath;
    }

    protected override void DoWrite(string message)
    {
        File.AppendAllText(_filePath, message);
    }

    protected override void DoWriteLine(string message)
    {
        File.AppendAllText(_filePath, message + Environment.NewLine);
    }

    protected override void DoLog(DebugLevel debugLevel, string message)
    {
        string logMessage = $"[{debugLevel}] {message}";
        File.AppendAllText(_filePath, logMessage + Environment.NewLine);
    }
}

public class DebugLoggerStrategy : ChainedStrategy
{
    protected override void DoWrite(string message)
    {
        System.Diagnostics.Debug.Write(message);
    }

    protected override void DoWriteLine(string message)
    {
        System.Diagnostics.Debug.WriteLine(message);
    }

    protected override void DoLog(DebugLevel debugLevel, string message)
    {
        System.Diagnostics.Debug.WriteLine($"[{debugLevel}] {message}");
    }
}

This set of classes and interface provides a flexible framework for implementing various logging strategies and composing them into a chain for sequential execution. Remember to call Compose() on CompositeLoggerStrategy whenever you modify the strategies list to ensure the chain is correctly set up.
