
namespace MindForge.Domain.Logging;


/// <summary>
/// Represents a subject in the observer pattern for logging.
/// </summary>
public class LogSubject
{
    private readonly List<ILogger> observers = new();

    /// <summary>
    /// [Internal] Gets the observers attached to the subject.
    /// </summary>
    internal IReadOnlyList<ILogger> Observers => observers;

    /// <summary>
    /// Attaches an observer to the subject.
    /// </summary>
    /// <param name="observer">The observer to attach.</param>
    public void Attach(ILogger observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }
    /// <summary>
    /// Clears all attached observers from the subject.
    /// </summary>
    public void Clear()
    {
        observers.Clear();
    }

    /// <summary>
    /// Detaches an observer from the subject.
    /// </summary>
    /// <param name="observer">The observer to detach.</param>
    public void Detach(ILogger observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    /// <summary>
    /// Notifies all attached observers with a log message.
    /// </summary>
    /// <param name="level">The log level of the message.</param>
    /// <param name="message">The log message.</param>
    /// <param name="exception">An optional exception to include in the log.</param>
    public void Notify(DebugLevel level, string message, Exception exception = null)
    {
        foreach (var observer in observers)
        {
            observer.Log(level, $"{message} {(exception != null ? $" :: {exception.Message}" : string.Empty)}");
        }
    }
}