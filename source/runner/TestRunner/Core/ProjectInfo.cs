


using System.ComponentModel;

namespace MindForge.TestRunner.Core;

/// <summary>
/// Represents information about a project.
/// </summary>
public class ProjectInfo
{
    private readonly List<TestContainerInfo> testContainers = new();

    /// <summary>
    /// Gets or sets the name of the assembly.
    /// </summary>
    public string AssemblyName { get; set; }
    /// <summary>
    /// Gets or sets the path to the assembly.
    /// </summary>
    public string AssemblyPath { get; set; }
    /// <summary>
    /// Gets or sets the target framework of the project.
    /// </summary>
    public string TargetFramework { get; set; }
    /// <summary>
    /// Gets the test containers as READ ONLY.
    /// </summary>
    public IReadOnlyList<TestContainerInfo> TestContainers => testContainers.AsReadOnly();

    /// <summary>
    /// Add the container info.
    /// </summary>
    /// <param name="types">Test container info.</param>
    internal void AddContainerInfo(TestContainerInfo containerInfo)
    {
        testContainers.Add(containerInfo);
    }

    /// <summary>
    /// Determines whether the project has tests.
    /// </summary>
    /// <returns>TRUE if the project has tests; otherwise, FALSE.</returns>
    internal bool HasTests() => TestContainers.Any(c => c.Tests.Any());
    /// <summary>
    /// Determines whether the project info is valid.
    /// </summary>
    /// <returns>TRUE if the project info is valid; otherwise, FALSE.</returns>
    internal bool IsValid() => !string.IsNullOrEmpty(AssemblyName) &&
                               !string.IsNullOrEmpty(AssemblyPath) &&
                               !string.IsNullOrEmpty(TargetFramework);
}
