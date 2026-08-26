// u251023_code
// u251023_documentation

using System.Text.Json;

namespace cooke.Configuration;

/// <summary>Cooke configuration logic.</summary>
internal class CookeConfig
{
    /// <summary>The name of the repository for this instance of Cooke.</summary>
    public string RepoName { get; set; }

    /// <summary>The repository URL for this instance of Cooke.</summary>
    public string RepoUrl { get; set; }

    /// <summary>Determines if the repository name is displayed in the changelog.</summary>
    public bool DisplayRepoName { get; set; }

    /// <summary>Determine if detailed information is displayed.</summary>
    public bool DisplayDetails { get; set; }

    /// <summary>Indicates the start of a change log item.</summary>
    /// <remarks>Can be changed to any character.</remarks>
    /// <value>Default value is "[".</value>
    public string CookeStartTag { get; set; }

    /// <summary>Indicates the end of a change log item.</summary>
    /// <remarks>Can be changed to any character.</remarks>
    /// <value>Default value is "]".</value>
    public string CookeEndTag { get; set; }

    /// <summary>The path where the generated CHANGELOG.md file will be written.</summary>
    public string GeneratedChangelogPath { get; set; }

    /// <summary>The path where the generated RELEASE_NOTES.md file will be written.</summary>
    public string GenerateReleaseNotesPath { get; set; }

    /// <summary>Determines if a history of generated documents is kept.</summary>
    public bool KeepHistory { get; set; }

    /// <summary>
    /// Loads the configuration from the specified file path. If the file does not exist, a new configuration file is
    /// created with default values.
    /// </summary>
    /// <remarks>If the specified file does not exist, a new configuration file is created at the specified
    /// path with default values, and the new configuration is returned.</remarks>
    /// <param name="configPath">The path to the configuration file. Must be a valid file path.</param>
    /// <returns>A <see cref="CookeConfig"/> object representing the loaded configuration. If the file does not exist, the method
    /// returns a configuration with default values.</returns>
    internal static CookeConfig Load(string configPath)
    {
        CLI.DisplayText.Colored(Blueprint.UserMessage.LoadConfig());

        if (!File.Exists(configPath))
        {
            CLI.DisplayText.Colored(Blueprint.UserMessage.CreateConfigFile());

            var ckConfig = BuildNew();

            Save(ckConfig, configPath);
        }

        return JsonSerializer.Deserialize<CookeConfig>(File.ReadAllText(configPath))!;
    }

    /// <summary>
    /// Saves the specified <see cref="CookeConfig"/> object to a file in JSON format.
    /// </summary>
    /// <remarks>The JSON output is formatted with indented spacing for readability. If the specified file
    /// already exists, it will be overwritten.</remarks>
    /// <param name="ckConfig">The configuration object to be serialized and saved.</param>
    /// <param name="configPath">The file path where the configuration will be saved. Must be a valid file path.</param>
    internal static void Save(CookeConfig ckConfig, string configPath)
    {
        JsonSerializerOptions jsonFormat = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var jsonString = JsonSerializer.Serialize(ckConfig, jsonFormat);

        File.WriteAllText(configPath, jsonString);
    }

    /// <summary>
    /// Creates and returns a new instance of the <see cref="CookeConfig"/> class with default configuration values.
    /// </summary>
    /// <remarks>The returned configuration includes default values for repository information, display
    /// options, tag formatting, and file paths. These values can be modified after the instance is created.</remarks>
    /// <returns>A new <see cref="CookeConfig"/> instance initialized with default settings.</returns>
    internal static CookeConfig BuildNew()
    {
        return new CookeConfig
        {
            RepoName                 = "not-defined",
            RepoUrl                  = "not-defined",
            DisplayRepoName          = true,
            DisplayDetails           = false,
            CookeStartTag            = "[",
            CookeEndTag              = "]",
            GeneratedChangelogPath   = "../",
            GenerateReleaseNotesPath = "../",
            KeepHistory              = true
        };
    }
}