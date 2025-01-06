using System.Text.Json.Serialization;

namespace MindForge.TestRunner.Core;

/// <summary>
/// Represents the configuration settings for the test runner.
/// </summary>
public class RunnerConfig
{
    /// <summary>
    /// Gets or sets the paths to the test files.
    /// </summary>
    [JsonPropertyName("test_paths")]
    public string[] Paths { get; set; }
    /// <summary>
    /// Gets or sets the minimum debug level.
    /// </summary>
    [JsonPropertyName("min_debug_level")]
    public DebugLevel MinDebugLevel { get; set; } = DebugLevel.Default;
}
