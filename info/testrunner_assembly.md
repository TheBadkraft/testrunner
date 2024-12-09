
Namespace: MindForge.TestRunner.Core
Enums
RunnerState
TestOutcome
Attributes
TestContainerAttribute
TestAttribute
SetUpAttribute
TearDownAttribute
ContainerInitializeAttribute
ContainerCleanUpAttribute
Classes
TestContext
Properties for tracking test execution state (like ContainerInfo, StartTime, EndTime, Duration, etc.)
RunnerStateMachine<TState>
Abstract class for managing state transitions with methods like TransitionTo.
RunnerStateHandler<TState>
Abstract class for handling state transitions with methods like CanTransitionTo, OnBeforeTransition, OnAfterTransition.
TestDirector
Inherits from RunnerStateMachine<RunnerState>.
Manages the overall flow of test execution with methods like Run(), DiscoverTests(), ExecuteTests(), AuditResults().
TestDirectorStateHandler
Concrete implementation of RunnerStateHandler<RunnerState> for handling state transitions in TestDirector.
Assert
Static class with assertion methods for unit testing (e.g., AreEqual, IsNull, etc.).
TestCase
Delegate type for test methods.
InitializeContainer
CleanUpContainer
TestSetUp
TestTearDown
AssignTestContext
Delegate types for various lifecycle methods.
TestContainerInfo
Contains information about test containers including lifecycle methods and test results.
Properties: ContainerType, Name, TestMethods, TestResults, etc.
Methods: CreateDelegate, CreateWrappedDelegate.
TestCaseResult
Encapsulates test result information with MethodName, Outcome, ErrorMessage, timing properties, and methods like StartTest, EndTest.

Namespace: MindForge.TestRunner.Execution
Classes
TestDetector
Detects test containers and methods within assemblies.
Methods like EnumerateTests(ProjectInfo).
TestExecutor
Executes the tests found by TestDetector.
Methods like ExecuteTests(IEnumerable<ProjectInfo>), RunTestWithSetupAndTearDown.
TestAuditor
Audits and reports on test results.
Methods like AuditResults(IEnumerable<TestContainerInfo>), GenerateSummaryReport, LogDetailedResults, SaveResultsToFile.

Namespace: MindForge.TestRunner.Config
Classes
RunnerConfig
Represents the configuration for the test runner, particularly paths to test projects.

Namespace: MindForge.TestRunner.Logging
Interfaces
ILogger
Defines the logging interface.
Enums
DebugLevel
Classes
ConsoleLogger (or similar implementation)
Implements ILogger for console output.

Namespace: MindForge.TestRunner.Project
Classes
ProjectInfo
Holds information about a test project, including assembly details and test containers.

Summary:
Core: Contains the fundamental classes and structures for managing tests, states, and results.
Execution: Deals with the mechanics of discovering, running, and auditing tests.
Config: Manages configuration settings for the test runner.
Logging: Provides the logging infrastructure.
Project: Manages project-specific information.

This outline summarizes the components we've developed, providing a structured approach to unit testing within the MindForge.TestRunner namespace. Remember, some of these classes might be split into multiple files or combined based on your coding standards and project structure.
