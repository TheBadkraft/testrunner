
using MindForge.Domain.Logging;

namespace Codificer.Testing;

[TestContainer]
public class LogSubjectTests
{
    private static ILogger Logger { get; set; }
    private static LogSubject LogSubject { get; set; }

    [Test]
    public void Attach_AddsObserverToList()
    {
        // Arrange

        // Act
        LogSubject.Attach(Logger);

        // Assert
        Assert.Contains(LogSubject.Observers, (x) => x == Logger);
    }

    [Test(Skip = true)]
    public void Detach_RemovesObserverFromList()
    {
        // Arrange
        LogSubject.Attach(Logger);

        // Act
        LogSubject.Detach(Logger);

        // Assert
        Assert.DoesNotContain(LogSubject.Observers, (x) => x == Logger);
    }

    [Test(Skip = true)]
    public void Notify_CallsLogOnAllObservers()
    {
        // Arrange
        LogSubject.Attach(Logger);

        // Act
        LogSubject.Notify(DebugLevel.Default, "Test");

        // Assert
        A.CallTo(() => Logger.Log(DebugLevel.Default, "Test")).MustHaveHappened();
    }

    #region Container Initialize & CleanUp
    [ContainerInitialize]
    public static void Initialize(TestContext testContext)
    {
        Logger = A.Fake<ILogger>();
        LogSubject = new LogSubject();
    }

    [ContainerCleanUp]
    public static void CleanUp()
    {
        LogSubject.Clear();
    }
    #endregion
}
