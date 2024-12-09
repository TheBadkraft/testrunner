
namespace MindForge.TestRunner.Core;

/// <summary>
/// Represents a detector for identifying and managing tests.
/// </summary>
/// <param name="logger">The logger used for logging information and errors.</param>
/// <exception cref="NotImplementedException">Thrown to indicate that the method or operation is not implemented.</exception>
public class TestDetector
{
    private const string CSPROJ = "csproj";
    private const string PATTERN = $"*.{CSPROJ}";

    private ILogger Logger { get; init; }
    private RunnerConfig Config { get; init; }

    public TestDetector(ILogger logger, RunnerConfig config)
    {
        Logger = logger;
        Config = config;
    }

    /// <summary>
    /// Discovers tests in the specified projects.
    /// </summary>
    /// <param name="projects">The projects containing the tests to discover.</param>
    /// <returns>TRUE if tests were discovered; otherwise, FALSE.</returns>
    internal bool DiscoverTests(out IEnumerable<ProjectInfo> projects)
    {
        Logger.Log(DebugLevel.Default, "Begin Test Discovery ...");
        var list = new List<ProjectInfo>();
        var iterator = GetProjectInfo();

        while (iterator.MoveNext())
        {
            var proj = iterator.Current;
            if (EnumerateTests(proj))
            {
                list.Add(proj);
            }
        }
        projects = list;

        return projects.Any();
    }

    private IEnumerator<ProjectInfo> GetProjectInfo()
    {
        var directories = Directory.EnumerateDirectories(Config.Paths[0], "*", SearchOption.TopDirectoryOnly);
        foreach (var directory in directories)
        {
            //  there should only be one (if any) csproj file in the directory
            var csprojPath = Directory.GetFiles(directory, PATTERN).FirstOrDefault();
            if (csprojPath != null)
            {
                var projectInfo = GetProjectInfo(csprojPath, directory);
                if (!projectInfo.IsValid())
                {
                    continue;
                }

                yield return projectInfo;
            }
        }
    }
    private ProjectInfo GetProjectInfo(string csprojPath, string projectDirectory)
    {
        try
        {
            var xDoc = XDocument.Load(csprojPath);
            var assemblyName = xDoc.Descendants("AssemblyName").FirstOrDefault()?.Value ?? Path.GetFileNameWithoutExtension(csprojPath);
            var targetFramework = xDoc.Descendants("TargetFramework").FirstOrDefault()?.Value ?? "net8.0"; // Default to net6.0

            var binPath = Path.Combine(projectDirectory, "bin", "Debug", targetFramework);
            var assemblyPath = Directory.GetFiles(binPath, $"{assemblyName}.dll").FirstOrDefault();

            return new ProjectInfo
            {
                AssemblyName = assemblyName ?? string.Empty,
                TargetFramework = targetFramework ?? string.Empty,
                AssemblyPath = assemblyPath ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            // Log the exception or handle it appropriately
            var message = $"Error loading project information for {csprojPath}";
            message = $"{message} {ex.Message}";

            Logger.Log(DebugLevel.Error, message);
            return null;
        }
    }
    private bool EnumerateTests(ProjectInfo proj)
    {
        if (!proj.IsValid())
        {
            //  log invalid project configuration
            return false;
        }

        //  reflect into project assembly and locate TestContainer classes
        var assembly = Assembly.LoadFrom(proj.AssemblyPath);
        var containers = assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<TestContainerAttribute>() != null);

        if (containers.Any())
        {
            Logger.Log(DebugLevel.Default, $"Found {containers.Count()} test containers in {proj.AssemblyName}");

            foreach (var @class in containers)
            {
                Logger.Log(DebugLevel.Default, $"TestContainer: {@class.Name}");
                var containerInfo = new TestContainerInfo(@class);
                proj.AddContainerInfo(containerInfo);

                // Find all test methods with TestAttribute
                var testMethodsInfo = @class.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    .Where(m => m.GetCustomAttribute<TestAttribute>() != null);
                if (testMethodsInfo.Any())
                {
                    containerInfo.AddTests(testMethodsInfo);

                }
            }
        }

        return proj.HasTests();
    }
}
