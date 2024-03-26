// b240326.0914

namespace Cooke
{
    /// <summary>The Changelog object contains the methods to generate a CHANGELOG.md file.</summary>
    public static class Changelog
    {
        /// <summary> Generate the CHANGELOG.md file.</summary>
        /// <param name="appConfig">The object that contains the Cooke configuration settings.</param>
        public static void Generate(AppConfig appConfig)
        {
            Utility.DisplayMsg("Cooke: Generating CHANGELOG.md...");

            BuildRawData(appConfig.TempPath, appConfig.ChangelogGitCmd);

            BuildRawChangelog(appConfig.TempPath, appConfig.Months, appConfig.ChangelogStartTag, appConfig.ChangelogEndTag, appConfig.VerboseLog);

            BuildChangelogMd(appConfig.TempPath, appConfig.ChangelogRawPath, appConfig.ChangelogRepoPath, appConfig.RepoName, appConfig.ChangelogIncludeName, appConfig.ChangelogKeepHistory, appConfig.VerboseLog);

            Utility.DisplayMsg("Cooke: CHANGELOG.md file created.");
        }

        /// <summary>Build the raw data from the git log.</summary>
        /// <param name="tempDir">The temporary data directory.</param>
        /// <param name="gitCmd">The git command.</param>
        private static void BuildRawData(string tempDir, string gitCmd)
        {
            Utility.DisplayMsg("       Building changelog data from git log...");

            Utility.ExeSysCmd("cmd.exe", $"{gitCmd} > {tempDir}changelog.gitlog");
        }

        /// <summary>Build the raw CHANGELOG.md file.</summary>
        /// <param name="tempDir">The temporary data directory.</param>
        /// <param name="months">A list of month abreviations.</param>
        /// <param name="startTag">The string that indicates the beginning of a keyword.</param>
        /// <param name="endTag">The string that indicates the end of a keyword.</param>
        private static void BuildRawChangelog(string tempDir, List<string> months, string startTag, string endTag, bool verboseLog)
        {
            Utility.DisplayMsg("       Building raw changelog content...");

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

            using (StreamReader rawChangelogFile = new StreamReader($"{tempDir}changelog.gitlog"))
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
                        commitDateStamp = FormatCommitDate(versionBlock.Value[0], months);
                    }

                    if (versionBlock.Value.Count > 0)
                    {
                        contentBody += BuildBody(startTag, endTag, commitDateStamp, contentBody, versionBlock);
                    }
                }
            }

            Utility.DisplayMsg("       Writing raw changelog content...", verboseLog);

            File.WriteAllText($"{tempDir}changelog.raw", contentBody);
        }

        /// <summary>Build the CHANGELOG.md file.</summary>
        /// <param name="tempDir">The temporary data directory.</param>
        /// <param name="exportPath">The path for exported data.</param>
        /// <param name="repoPath">The path to the repository.</param>
        /// <param name="repoName">The name of the repository.</param>
        /// <param name="includeName">Determines if the repository name is included in the CHANGELOG.md title.</param>
        /// <param name="keepHistory">Determines if a historical record of CHANGELOG.md is kept.</param>
        /// <param name="verboseLog">Determines if the logging is verbose.</param>
        private static void BuildChangelogMd(string tempDir, string exportPath, string repoPath, string repoName, bool includeName, bool keepHistory, bool verboseLog)
        {
            Utility.DisplayMsg("       Building CHANGELOG.md...");

            var contentBody = File.ReadAllText($"{tempDir}changelog.raw");

            var finalContent = BuildContentHeader() +
                               BuildChangelogTitle(repoName, includeName) +
                               contentBody;

            var dateTime= DateTime.Now.ToString("yyMMdd-HHmmss");

            if (keepHistory)
            {
                Utility.DisplayMsg("       Writing datestamped CHANGELOG.md...", verboseLog);
                File.WriteAllText($"{exportPath}CHANGELOG_{dateTime}.md", finalContent);
            }

            Utility.DisplayMsg($"       Writing CHANGELOG.md to {repoPath}CHANGELOG.md...", verboseLog);
            File.WriteAllText($"{repoPath}CHANGELOG.md", finalContent);
        }

        /// <summary>Build the body of the CHANGELOG.md file.</summary>
        /// <param name="startTag">The string that indicates the beginning of a keyword.</param>
        /// <param name="endTag">The string that indicates the end of a keyword.</param>
        /// <param name="commitStamp">The datestamp of a commit.</param>
        /// <param name="body">The content body.</param>
        /// <param name="verBlock">The collection of version changes.</param>
        /// <returns></returns>
        private static string BuildBody(string startTag, string endTag, string commitStamp, string body, KeyValuePair<string, List<string>> verBlock)
        {
            var bodyHeader = Environment.NewLine +
                             $"## {verBlock.Key}{commitStamp}" +
                             Environment.NewLine +
                             Environment.NewLine;

            List<string> sortedList = new List<string>();

            foreach (var line in verBlock.Value)
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
        /// <param name="commitStamp">The date of the commit.</param>
        /// <param name="months">A list of month abreviations.</param>
        /// <returns>A formated commit date.</returns>
        public static string FormatCommitDate(string commitStamp, List<string> months)
        {
            var commitInfo = commitStamp.Replace("CommitDate: ", "");
            var commitPart = commitInfo.Split(' ');

            var commitMonthAsText    = commitPart[1];
            var commitMonthAsInt     = months.IndexOf(commitMonthAsText)+1;
            var commitMonthFormatted = commitMonthAsInt.ToString("00");
            var commitDateFormatted  = commitPart[2];
            var commitYearFull       = commitPart[4];

            return $" - {commitYearFull}-{commitMonthFormatted}-{commitDateFormatted}";
        }

        /// <summary>Build the header of the CHANGELOG.md file.</summary>
        /// <returns>The CHANGELOG.md header.</returns>
        private static string BuildContentHeader() =>
            "<!-- " +
            Environment.NewLine +
            "    Changelog created using Cooke:" +
            Environment.NewLine +
            "    https://github.com/APrettyCoolProgram/Cooke" +
            Environment.NewLine +
            "-->" +
            Environment.NewLine;

        /// <summary>Build the CHANGELOG title.</summary>
        /// <param name="repoName"></param>
        /// <param name="includeName"></param>
        /// <returns></returns>
        private static string BuildChangelogTitle(string repoName, bool includeName) => (includeName)
            ? $"# {repoName} Changelog" +
              Environment.NewLine
            : "CHANGELOG" +
              Environment.NewLine;

        /// <summary>Build the final content of the CHANGELOG.md file.</summary>
        /// <param name="header">The CHANGELOG.md header.</param>
        /// <param name="body">The generated CHANGELOG.md body</param>
        /// <returns>The final CHANGELOG.md content.</returns>
        private static string BuildFinalContent(string header, string body) =>
            header +
            Environment.NewLine +
            body;
    }
}