// b240319.0940

using System.Text.Json;

namespace Cooke
{
    /// <summary> The AppConfig object contains the configuration settings for the Cooke application.</summary>
    public class AppConfig
    {
        // Repository specific.
        public string RepositoryName { get; set; }
        public string RepositoryURL { get; set; }

        // Common stuff.
        public string GitFolderLocation { get; set; }
        public string TemporaryDataFolder { get; set; }
        public List<string> MonthAbbreviations { get; set; }

        //CHANGELOG.md specific.
        public string ChangelogExportLocation { get; set; }
        public string ChangelogGitCommand { get; set; }
        public string ChangelogStartTag { get; set; }
        public string ChangelogEndTag { get; set; }
        public string ChangelogPublicLocation { get; set; }

        //RELEASE-NOTES.md specific
        public string ReleaseNoteExportLocation { get; set; }

        /// <summary>Create a Cooke-config.json file with default values.</summary>
        public static void CreateDefaultConfigFile()
        {
            Console.WriteLine("Cooke: Creating default configuration file...");

            AppConfig appConfig = new AppConfig
            {
                RepositoryName      = "Cooke",
                RepositoryURL       = "https://github.com/APrettyCoolProgram/Cooke",
                GitFolderLocation   = "../",
                TemporaryDataFolder = "./temp/",
                MonthAbbreviations  = new List<string>
                {
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
                },
                ChangelogExportLocation   = "./changelog/",
                ChangelogGitCommand       = "git log --pretty=fuller",
                ChangelogStartTag         = "[",
                ChangelogEndTag           = "]",
                ChangelogPublicLocation   = "../",
                ReleaseNoteExportLocation = "./releasenotes/",
            };

            var options    = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(appConfig, options);

            File.WriteAllText("./Cooke-config.json", jsonString);
        }

        /// <summary>Load the Cooke-config.json file.</summary>
        /// <returns>The Cooke configuration settings.</returns>
        public static AppConfig LoadConfigFile()
        {
            Console.WriteLine("Cooke: Loading configuration file...");

            string jsonString   = File.ReadAllText(@"./Cooke-config.json");

            return JsonSerializer.Deserialize<AppConfig>(jsonString);
        }
    }
}