using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MindForge.TestRunner.Testing")]

// See https://aka.ms/new-console-template for more information

var logFilePath = Path.Combine(Environment.CurrentDirectory, "test-runner.log");
using var logger = new Logger(logFilePath);

Console.WriteLine("Welcome to MindForge.TestRunner!");
using var testDirector = new TestDirector(logger);
testDirector.Run();
