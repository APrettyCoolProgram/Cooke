// 240509.1117

namespace Cooke.Session
{
    /// <summary>Cooke application settings.</summary>
    /// <remarks>
    ///     - These are all of the settings that Cooke requires
    ///     - There are two types of settings:
    ///         - User-defined settings loaded from the Cook configuration file at runtime
    ///         - Application setting that are created by Cooke at runtime
    ///     - The following settings need to be updated when a new version of Cooke is released:
    ///         - Ver = Major.Minor.Patch
    ///         - DetailedInfo = false
    /// </remarks>
    public class CkSession
    {
        /*
         * These settings are set by Cooke at runtime, and are not user-modifiable.
         */
        /// <summary>The current version of Cooke.</summary>
        public string Version { get; set; }

        /// <summary>The path to the temporary data directory.</summary>
        public string TempPath { get; set; }

        /// <summary>The command used to generate the git log.</summary>
        public string GitLogCmd { get; set; }

        /// <summary>The months of the year.</summary>
        public List<string> Months { get; set; }

        /// <summary>Determines if detailed information is displayed.</summary>
        /// <remarks>
        ///     - This is only used for debugging purposes, and should be set to "false" for releases.
        /// </remarks>
        public bool DetailedInfo { get; set; }

        /*
         * These are the user-definable settings that are loaded from the configuration file at runtime.
         *
         * For more descriptions about these settings, see Configuration.cs.
         */
        public int SleepDuration { get; set; }
        public bool KeepHistory { get; set; }
        public string RepositoryName { get; set; }
        public string RepositoryUrl { get; set; }
        public bool IncludeRepositoryNameInChangelog { get; set; }
        public string ChangelogStartTag { get; set; }
        public string ChangelogEndTag { get; set; }
        public string ChangelogMdPath { get; set; }

        /// <summary>Load all of the settings for a Cooke session.</summary>
        public static CkSession Load()
        {
            Configuration config = Configuration.Load();

            return new CkSession
            {
                Version                          = "0.9.0",
                TempPath                         = "./temp",
                GitLogCmd                        = "git log --pretty=fuller",
                Months                           = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
                DetailedInfo                     = false,
                SleepDuration                    = config.SleepDuration,
                KeepHistory                      = config.KeepHistory,
                RepositoryName                   = config.RepositoryName,
                RepositoryUrl                    = config.RepositoryUrl,
                IncludeRepositoryNameInChangelog = config.IncludeRepositoryNameInChangelog,
                ChangelogStartTag                = config.ChangelogStartTag,
                ChangelogEndTag                  = config.ChangelogEndTag,
                ChangelogMdPath                  = config.ChangelogMdPath
            };
        }
    }
}