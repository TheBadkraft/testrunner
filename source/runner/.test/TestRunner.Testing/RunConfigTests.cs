
namespace testrunner.testing;

[TestContainer(Ignore = true)]
public class RunConfigTests
{
    private static string exp_json = string.Empty;
    private readonly RunnerConfig config = new()
    {
        Paths = ["./.test"],
        MinDebugLevel = DebugLevel.Warning
    };

    [Test]
    public void SerializeConfig()
    {
        var act_json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
        Assert.IsNotEmpty(exp_json);
        Assert.AreEqual(exp_json, act_json);

        Debug.WriteLine(exp_json);
    }

    [Test]
    public void DeserializeConfig()
    {
        var deserialized = JsonSerializer.Deserialize<RunnerConfig>(exp_json);
        Assert.IsNotNull(deserialized);

        Assert.AreEqual(config.Paths, deserialized.Paths);
        Assert.AreEqual(config.MinDebugLevel, deserialized.MinDebugLevel);
    }

    #region TestRunner.Core.RunnerConfig
    [ContainerInitialize]
    public static void Initialize(TestContext context)
    {
        var jsonFile = "exp_config.json";
        exp_json = File.ReadAllText(jsonFile);
    }

    #endregion
}
