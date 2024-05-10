// 240509.1117S

using Cooke.Message;

namespace Cooke.Session
{
    /// <summary>Cooke configuration settings.</summary>
    /// <remarks>
    ///     - User-defined settings for Cooke
    ///     - Stored in the ./cooke.config file
    ///     - Loaded at runtime.
    /// </remarks>
    public class Configuration
    {
        /// <summary>Duration of sleep timers.</summary>
        /// <remarks>
        ///     - There is a brief pause in Changelog.BuildContent.Body() to allow the gitlog.txt file to be written.
        ///     - The default value is 1000ms (1 second).
        /// </remarks>
        public int SleepDuration { get; set; }

        /// <summary>Determines if a history of generated documents is kept.</summary>
        public bool KeepHistory { get; set; }

        /// <summary>The name of the repository for this instance of Cooke.</summary>
        public string RepositoryName { get; set; }

        /// <summary>The repository URL for this instance of Cooke.</summary>
        public string RepositoryUrl { get; set; }

        /// <summary>Determines if the repository name is included in the changelog.</summary>
        public bool IncludeRepositoryNameInChangelog { get; set; }

        /// <summary>The start tag for changelog items.</summary>
        /// <remarks>
        ///     - The default value is "[", but can be changed to any character or string.
        /// </remarks>
        public string ChangelogStartTag { get; set; }

        /// <summary>The end tag for changelog items.</summary>
        /// <remarks>
        ///     - The default value is "[", but can be changed to any character or string.
        /// </remarks>
        public string ChangelogEndTag { get; set; }

        /// <summary>The path where the generated CHANGELOG.md file will be written.</summary>
        public string ChangelogMdPath { get; set; }

        /// <summary>Build the default configuration settings.</summary>
        public static Configuration BuildDefault()
        {
            return new Configuration
            {
                SleepDuration                    = 1000,
                RepositoryName                   = "user-defined",
                RepositoryUrl                    = "user-defined",
                IncludeRepositoryNameInChangelog = true,
                ChangelogStartTag                = "[",
                ChangelogEndTag                  = "]",
                KeepHistory                      = false,
                ChangelogMdPath                  = "../"
            };
        }

        /// <summary>Load the settings from the configuration file.</summary>
        /// <returns>The Cooke user-defined configuration settings.</returns>
        public static Configuration Load()
        {
            return Du.WithJson.Import.FromFile<Configuration>("./cooke.config");
        }

        /// <summary>Verify that the configuration file exists, and create it if it does not.</summary>
        public static void Verify()
        {
            if (!File.Exists("./cooke.config"))
            {
                Create();
            }
        }

        /// <summary>Reset the configuration file to default settings.</summary>
        public static void Reset()
        {
            if (File.Exists("./cooke.config"))
            {
                ToUser.DisplayDeleteConfigurationFile();
                File.Delete("./cooke.config");
            }

            Create(true);
        }

        /// <summary>Create a new configuration file.</summary>
        /// <param name="displayUserMsg">Determines if the user is notified that the configuration file is being created.</param>
        /// <remarks>
        ///     - The CreateConfigurationFile message is only displayed when the configuration file is reset, not when
        ///       it is initially created.
        /// </remarks>
        private static void Create(bool displayUserMsg = false)
        {
            if (displayUserMsg)
            {
                ToUser.DisplayCreateConfigurationFile();
            }

            Du.WithJson.Export.ToLocalFile(Configuration.BuildDefault(), "./cooke.config");
        }
    }
}