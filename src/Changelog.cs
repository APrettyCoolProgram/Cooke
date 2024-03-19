// b240319.0947

namespace Cooke
{
    /// <summary>The Changelog object contains the methods to generate a CHANGELOG.md file.</summary>
    public class Changelog
    {
        /// <summary> Generate the CHANGELOG.md file.</summary>
        /// <param name="appConfig">The object that contains the Cooke configuration settings.</param>
        public static void Generate(AppConfig appConfig)
        {
            Console.WriteLine($"Cooke: Generating CHANGELOG.md...");

            BuildRawData(appConfig.TemporaryDataFolder, appConfig.ChangelogGitCommand);

            BuildRawChangelog(appConfig.TemporaryDataFolder, appConfig.ChangelogExportLocation, appConfig.ChangelogPublicLocation, appConfig.MonthAbbreviations, appConfig.ChangelogStartTag, appConfig.ChangelogEndTag);

            Console.WriteLine($"Cooke: CHANGELOG.md file created.");
        }

        /// <summary>Build the raw data from the git log command.</summary>
        private static void BuildRawData(string temporaryDataFolder, string gitCommand)
        {
            Console.WriteLine("       Building raw data from git log...");

            Utility.ExecuteSystemCommand("cmd.exe", $"{gitCommand} > {temporaryDataFolder}changelog-data.raw");
        }

        /// <summary>Build the raw CHANGELOG.md file.</summary>
        /// <param name="temporaryDataFolder">The Cooke temporary folder.</param>
        /// <param name="changelogExportLocation">The location for the CHANGELOG.md datestamped export.</param>
        /// <param name="monthAbbreviations">A list of month abreviations.</param>
        /// <param name="startTag">The string that indicates the beginning of a keyword.</param>
        /// <param name="endTag">The string that indicates the end of a keyword.</param>
        private static void BuildRawChangelog(string temporaryDataFolder, string changelogExportLocation, string changelogPublicLocation, List<string> monthAbbreviations, string startTag, string endTag)
        {
            Console.WriteLine("       Building raw CHANGELOG...");

            var version = "Current development";

            var changelogContents = new Dictionary<string,List<string>>()
            {
                { version, new List<string>() }
            };

            var finalContent    = "";
            var commitDate      = "";
            var commitDateStamp = "";
            var contentBody     = "";

            Thread.Sleep(1000); // This needs to be here to work? Also in Program.cs

            using (StreamReader rawChangelogFile = new StreamReader($"{temporaryDataFolder}changelog-data.raw"))
            {
                string rawLine;

                while ((rawLine = rawChangelogFile.ReadLine()) != null)
                {
                    var workLine = rawLine.Trim();

                    if (workLine.StartsWith("CommitDate"))
                    {
                        commitDate = workLine;
                    }

                    if (workLine.StartsWith(startTag))
                    {
                        if (workLine.StartsWith($"{startTag}VERSION{endTag}"))
                        {
                            version = workLine.Replace($"{startTag}VERSION{endTag} ", "");

                            changelogContents.Add(version, new List<string>
                            {
                                commitDate
                            });

                        }

                        if (workLine.StartsWith(startTag) && !workLine.StartsWith($"{startTag}VERSION{endTag}"))
                        {
                            changelogContents[version].Add($"{workLine}  ");

                        }
                    }
                }

                foreach (var versionBlock in changelogContents)
                {
                    if (versionBlock.Key != "Current development")
                    {
                        commitDateStamp = FormatCommitDate(versionBlock.Value[0], monthAbbreviations);
                    }

                    if (versionBlock.Value.Count > 0)
                    {
                        contentBody += BuildBody(startTag, endTag, commitDateStamp, contentBody, versionBlock);
                    }
                }
            }

            finalContent = BuildContentHeader() +
                           contentBody;

            var dateTime= DateTime.Now.ToString("yyMMdd-HHmmss");

            Console.WriteLine("       Writing datestamped CHANGELOG.md...");
            File.WriteAllText($"{changelogExportLocation}CHANGELOG_{dateTime}.md", finalContent);

            Console.WriteLine($"       Writing CHANGELOG.md to {changelogPublicLocation}CHANGELOG.md...");
            File.WriteAllText($"{changelogPublicLocation}CHANGELOG.md", finalContent);
        }

        /// <summary>Build the body of the CHANGELOG.md file.</summary>
        /// <param name="startTag">The string that indicates the beginning of a keyword.</param>
        /// <param name="endTag">The string that indicates the end of a keyword.</param>
        /// <param name="commitDateStamp">The datestamp of a commit.</param>
        /// <param name="contentBody">The content body.</param>
        /// <param name="versionBlock">The collection of version changes.</param>
        /// <returns></returns>
        private static string BuildBody(string startTag, string endTag, string commitDateStamp, string contentBody, KeyValuePair<string, List<string>> versionBlock)
        {
            var bodyHeader = Environment.NewLine +
                            $"## {versionBlock.Key}{commitDateStamp}" +
                            Environment.NewLine +
                            Environment.NewLine;

            List<string> sortedList = new List<string>();

            foreach (var line in versionBlock.Value)
            {
                if (line.StartsWith(startTag))
                {
                    sortedList.Add(line.Replace(startTag, "`").Replace(endTag, "`"));
                }
            }

            sortedList.Sort();

            var bodyContent = string.Join(Environment.NewLine, sortedList);

            return bodyHeader +
                   bodyContent +
                   Environment.NewLine;
        }

        /// <summary>Format the commit date.</summary>
        /// <param name="commitDate">The date of the commit.</param>
        /// <param name="monthAbbreviations">A list of month abreviations.</param>
        /// <returns>A formated commit date.</returns>
        public static string FormatCommitDate(string commitDate, List<string> monthAbbreviations)
        {
            var commitInfo = commitDate.Replace("CommitDate: ", "");
            var commitPart = commitInfo.Split(' ');

            var commitMonthAsText    = commitPart[1];
            var commitMonthAsInt     = monthAbbreviations.IndexOf(commitMonthAsText)+1;
            var commitMonthFormatted = commitMonthAsInt.ToString("00");
            var commitDateFormatted  = commitPart[2];
            var commitYearFull       = commitPart[4];

            return $" - {commitYearFull}-{commitMonthFormatted}-{commitDateFormatted}";
        }


        /// <summary>Build the header of the CHANGELOG.md file.</summary>
        /// <returns>The CHANGELOG.md header.</returns>
        private static string BuildContentHeader() => "<!-- " +
                                                      Environment.NewLine +
                                                      "    Changelog created using Cooke:" +
                                                      Environment.NewLine +
                                                      "    https://github.com/APrettyCoolProgram/Cooke" +
                                                      Environment.NewLine +
                                                      "-->" +
                                                      Environment.NewLine +
                                                      Environment.NewLine +
                                                      "# CHANGELOG" +
                                                      Environment.NewLine;

        /// <summary>Build the final content of the CHANGELOG.md file.</summary>
        /// <param name="contentHeader">The CHANGELOG.md header.</param>
        /// <param name="contentBody">The generated CHANGELOG.md body</param>
        /// <returns>The final CHANGELOG.md content.</returns>
        private static string BuildFinalContent(string contentHeader, string contentBody) => contentHeader +
                                                                                             Environment.NewLine +
                                                                                             contentBody;
    }
}