**_This is a roadmap for an expansive overhaul on logging._**  

### Roadmap  
#### **Composition:**  
`ILoggerStrategy` objects are composed into a chain where each strategy can leverage or modify the behavior of the next one in line. This is a classic example of _composition_ where one object can contain or reference another object to extend its functionality at runtime.  

#### **Decorator:**  
Each `ILoggerStrategy` acts as a _decorator_ since it can wrap another strategy, enhancing or altering its behavior without changing its interface.   

#### **Strategy:**  
The ability to choose different behaviors at runtime based on which _strategies_ are composed into the chain aligns with the Strategy Pattern's intent of defining a family of algorithms, encapsulating each one, and making them interchangeable.  

#### **ILoggerStrategy Interface**  
``` csharp
public interface ILoggerStrategy
{
    void Write(string message);
    void WriteLine(string message);
    void Log(DebugLevel debugLevel, string message);
}
```

#### **ChainedStrategy Base Class**  
``` csharp
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
```

#### **CompositeLoggerStrategy Class**  
``` csharp
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
```

#### **DebugLevel Enum** _(assumed to be used in the Log methods)_  
``` csharp
public enum DebugLevel
{
    Verbose,
    Debug,
    Info,
    Warning,
    Error,
    Fatal,
    Test
}
```

#### **Concrete Strategy Implementations**  
``` csharp
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
```

This set of classes and interface provides a flexible framework for implementing various logging strategies and composing them into a chain for sequential execution. Remember to call Compose() on CompositeLoggerStrategy whenever you modify the strategies list to ensure the chain is correctly set up.
-----  
## Observation  
Now, integrating the Observer Pattern to facilitate centralized logging for objects already using an ILogger interface can be done as follows:

Implementation Steps:
Centralized Logger (Subject):
Create a central LoggerSubject that will act as the subject in the Observer Pattern. This class would manage or be aware of the ILoggerStrategy chain.

csharp
public class LoggerSubject
{
    private readonly List<IObserver<LogMessage>> _observers = new List<IObserver<LogMessage>>();
    private readonly CompositeLoggerStrategy _strategyChain;

    public LoggerSubject(CompositeLoggerStrategy strategyChain)
    {
        _strategyChain = strategyChain;
    }

    public void RegisterObserver(IObserver<LogMessage> observer) => _observers.Add(observer);
    public void RemoveObserver(IObserver<LogMessage> observer) => _observers.Remove(observer);
    
    public void NotifyObservers(LogMessage message)
    {
        foreach (var observer in _observers)
        {
            observer.OnNext(message);
        }
    }

    public void Log(LogMessage message)
    {
        _strategyChain.Log(message.DebugLevel, message.Message);
        NotifyObservers(message); // Notification after strategy chain execution
    }
}
Adapt Existing Loggers to Observers:
Modify or wrap existing ILogger implementations to conform to IObserver<LogMessage>. This way, they can subscribe to the central LoggerSubject to receive all log messages:

csharp
public class LoggerObserver : IObserver<LogMessage>
{
    private ILogger _logger;

    public LoggerObserver(ILogger logger)
    {
        _logger = logger;
    }

    public void OnCompleted() { /* Handle completion if needed */ }
    public void OnError(Exception error) { /* Handle errors if needed */ }
    public void OnNext(LogMessage value)
    {
        _logger.Log(value.DebugLevel, value.Message);
    }
}
Integration with Existing Objects:
Instead of directly using ILogger, existing objects can now interact with the LoggerSubject. When instantiated, they would subscribe their ILogger wrapped as an IObserver to this central logger:

csharp
// Example where an existing object that uses ILogger is updated
public class ExistingObject
{
    private readonly LoggerSubject _loggerSubject;

    public ExistingObject(LoggerSubject loggerSubject)
    {
        _loggerSubject = loggerSubject;
        _loggerSubject.RegisterObserver(new LoggerObserver(this.Logger)); // Assume 'this.Logger' is an existing ILogger field or property
    }

    // Existing methods would now use _loggerSubject instead of ILogger directly
    public void DoSomething()
    {
        _loggerSubject.Log(new LogMessage(DebugLevel.Info, "Doing something"));
    }
}
Manage Strategy Chain:
Ensure that the strategy chain in CompositeLoggerStrategy is composed and managed as needed, perhaps through configuration or dynamically at runtime based on the application's needs.

Benefits:
Decoupling: The existing objects are no longer tightly coupled to a specific ILogger implementation but to a centralized logging system.
Flexibility: You can modify logging behavior by changing the strategy chain without altering the existing objects.
Centralized Control: All logging goes through one point, making it easier to manage, monitor, and potentially redirect logs.

This approach leverages both composition for strategy chaining and observation for event dissemination, enhancing the system's modularity and maintainability.
