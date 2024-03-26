// b240326.0959

using System.Text.Json;

namespace Cooke
{
    /// <summary> The AppConfig object contains the configuration settings for the Cooke application.</summary>
    public class AppConfig
    {
        // Cooke specific.

        /// <summary>The version of Cooke</summary>
        public string AppVer { get; set; }

        /// <summary>Determines if logging is verbose.</summary>
        public bool VerboseLog { get; set; }

        // Repository specific.

        /// <summary>Name of the repository Cooke will generate a CHANGLOG.md for.</summary>
        public string RepoName { get; set; }

        /// <summary>URL of the repository Cooke will generate a CHANGLOG.md for.</summary>
        public string RepoURL { get; set; }

        // Common stuff.

        /// <summary>Path to .git/</summary>
        public string DotGitPath { get; set; }

        /// <summary>Temporary data directory.</summary>
        public string TempPath { get; set; }

        /// <summary>List of months.</summary>
        public List<string> Months { get; set; }

        //CHANGELOG.md specific.

        /// <summary>Determines if the Repository name is included in the CHANGELOG.md title.</summary>
        public bool ChangelogIncludeName { get; set; }

        /// <summary>Path to raw Changelog data.</summary>
        public string ChangelogRawPath { get; set; }

        /// <summary>The git command used.</summary>
        public string ChangelogGitCmd { get; set; }

        /// <summary>The character that indicates the start of a changelog tag.</summary>
        public string ChangelogStartTag { get; set; }

        /// <summary>The character that indicates the end of a changelog tag.</summary>
        public string ChangelogEndTag { get; set; }

        /// <summary>Determines if historical changelogs are saved.</summary>
        public bool ChangelogKeepHistory { get; set; }

        /// <summary>Path to the repository CHANGELOG.md.</summary>
        public string ChangelogRepoPath { get; set; }

        //RELEASE-NOTES.md specific

        /// <summary>Path to the Release Note raw data.</summary>
        public string ReleaseNoteExportLocation { get; set; }

        /// <summary>Verifies that the configuration file exists, and creates one if it does not.</summary>
        /// <param name="configPath">Path to the configuration file.</param>
        public static void Verify(string configPath)
        {
            if (!File.Exists(configPath))
            {
                Create(configPath);
            }
        }

        /// <summary>Create a new default configuration file.</summary>
        /// <param name="configPath">Path to the configuration file.</param>
        private static void Create(string configPath)
        {
            Utility.DisplayMsg(Catalog.Msg.FirstExecution());

            BuildContent(configPath);

            Utility.ExitApp("Cooke: Process complete!");
        }

        /// <summary>Create the default configuration values.</summary>
        private static void BuildContent(string configPath)
        {
            Utility.DisplayMsg("Cooke: Building default configuration file...");

            AppConfig appConfig = new AppConfig
            {
                AppVer          = "0.1.1",
                VerboseLog      = false,
                RepoName        = "YOUR-REPOSITORY-NAME",
                RepoURL         = "YOUR-REPOSITORY-URL",
                DotGitPath      = "../",
                TempPath        = "./temp/",
                Months =
                [
                    "Jan",
                    "Feb",
                    "Mar",
                    "Apr",
                    "May",
                    "Jun",
                    "Jul",
                    "Aug",
                    "Sep",
                    "Oct",
                    "Nov",
                    "Dec"
                ],
                ChangelogIncludeName      = true,
                ChangelogRawPath          = "./changelog/",
                ChangelogGitCmd           = "git log --pretty=fuller",
                ChangelogStartTag         = "[",
                ChangelogEndTag           = "]",
                ChangelogRepoPath         = "../",
                ReleaseNoteExportLocation = "./releasenotes/",
            };

            Save(appConfig, configPath);
        }

        /// <summary>Save the configuration data to a file.</summary>
        /// <param name="appConfig">The configuration data object.</param>
        /// <param name="configPath">The path to the configuration file.</param>
        public static void Save(AppConfig appConfig, string configPath)
        {
            JsonSerializerOptions jsonFormat = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string configData = JsonSerializer.Serialize(appConfig, jsonFormat);

            Utility.DisplayMsg("Cooke: Writing configuration file to disk...", appConfig.VerboseLog);

            File.WriteAllText(configPath, configData);
        }

        /// <summary> Load the configuration file.</summary>
        /// <param name="configPath">Path to the configuration file.</param>
        /// <returns>The configuration settings.</returns>
        public static AppConfig Load(string configPath)
        {
            Utility.DisplayMsg("Cooke: Loading configuration file...");

            string jsonString = File.ReadAllText(configPath);

            return JsonSerializer.Deserialize<AppConfig>(jsonString);
        }
    }
}