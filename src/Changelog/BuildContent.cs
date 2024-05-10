// 240509.1117

using Cooke.Message;

namespace Cooke.Changelog
{
    /// <summary>Builds the content of the changelog.</summary>
    public static class BuildContent
    {
        /// <summary>Builds the body of the changelog.</summary>
        public static void Body(string tempPath, List<string> months, string startTag, string endTag, int sleepDuration)
        {
            ToUser.DisplayBuildingChangelogContent();

            /* OK, so for some reason, the gitlog.txt file is being written too slowly (??), and there needs to be a
             * short pause before continuing.
             *
             * The sleepDuration is set to 1000ms (1 second) by default, but can be adjusted in the config file.
             */
            Thread.Sleep(sleepDuration);

            ToUser.DisplayReadingGitLogTxt();

            var version = "Current development";

            var changelogVersionBlocks = new Dictionary<string,List<string>>()
            {
                { version, new List<string>() }
            };

            var commitDate      = "";
            var commitDateStamp = "";
            var contentBody     = "";

            using (StreamReader gitlogTxt = new StreamReader($@"{tempPath}\gitlog.txt"))
            {
                string logLine;

                while ((logLine = gitlogTxt.ReadLine()) != null)
                {
                    logLine = logLine.Trim();

                    if (logLine.StartsWith("CommitDate"))
                    {
                        commitDate = logLine;
                    }

                    if (logLine.StartsWith(startTag))
                    {
                        if (logLine.StartsWith($"{startTag}VERSION{endTag}"))
                        {
                            version = logLine.Replace($"{startTag}VERSION{endTag} ", "");

                            if(!changelogVersionBlocks.ContainsKey(version))
                            {
                                changelogVersionBlocks.Add(version,
                                [
                                    commitDate
                                ]);
                            }

                        }

                        if (logLine.StartsWith(startTag) && !logLine.StartsWith($"{startTag}VERSION{endTag}"))
                        {
                            changelogVersionBlocks[version].Add($"{logLine}  ");
                        }
                    }
                }

                contentBody = ParseVersionBlocks(months, startTag, endTag, changelogVersionBlocks, commitDateStamp, contentBody);

                ToUser.DisplayWritingChangelogTxt();
                File.WriteAllText($@"{tempPath}\changelog.txt", contentBody);
            }
        }

        /// <summary>Parses the version blocks from the git log.</summary>
        public static string ParseVersionBlocks(List<string> months, string startTag, string endTag, Dictionary<string, List<string>> changelogVersionBlocks, string commitDateStamp, string contentBody)
        {
            ToUser.DisplayParsingVersionBlocks();

            foreach (var versionBlock in changelogVersionBlocks)
            {
                if (versionBlock.Key != "Current development")
                {
                    commitDateStamp = FormattedCommitDate(versionBlock.Value[0], months);
                }

                if (versionBlock.Value.Count > 0)
                {
                    contentBody += BodyFinal(startTag, endTag, commitDateStamp, contentBody, versionBlock);
                }
            }

            return contentBody;
        }

        /// <summary>Formats the commit date.</summary>
        public static string FormattedCommitDate(string commitStamp, List<string> months)
        {
            var commitInfo           = commitStamp.Replace("CommitDate: ", "");
            var commitPart           = commitInfo.Split(' ');
            var commitMonthAsText    = commitPart[1];
            var commitMonthAsInt     = months.IndexOf(commitMonthAsText)+1;
            var commitMonthFormatted = commitMonthAsInt.ToString("00");
            var commitDateFormatted  = commitPart[2];
            var commitYearFull       = commitPart[4];

            return $" - {commitYearFull}-{commitMonthFormatted}-{commitDateFormatted}";
        }

        /// <summary>Builds the body of the changelog.</summary>
        public static string BodyFinal(string startTag, string endTag, string commitStamp, string body, KeyValuePair<string, List<string>> versionBlock)
        {
            var bodyHeader = Environment.NewLine +
                             $"## {versionBlock.Key}{commitStamp}" +
                             Environment.NewLine +
                             Environment.NewLine;

            List<string> sortedList = [];

            string versionBlockInfo = "";

            foreach (var line in versionBlock.Value)
            {
                if (line.StartsWith(startTag))
                {
                    if (line.StartsWith($"{startTag}INFO{endTag}"))
                    {
                        versionBlockInfo = line.Replace($"{startTag}INFO{endTag}", ">");
                    }
                    else
                    {
                        sortedList.Add(line.Replace(startTag, "`").Replace(endTag, "`"));
                    }
                }
            }

            string bodyContent;

            if (sortedList.Count == 0)
            {
                bodyContent = ">This release does not contain changes.  ";
            }
            else
            {
                sortedList.Sort();

                if (versionBlockInfo != "")
                {
                    sortedList.Insert(0, $"{versionBlockInfo}{Environment.NewLine}");
                }

                bodyContent = string.Join(Environment.NewLine, sortedList);
            }

            return bodyHeader +
                   bodyContent +
                   Environment.NewLine;
        }

        /// <summary>Builds the header of the changelog.</summary>
        public static string ContentHeader(string appVer)
        {
            ToUser.DisplayBuildingChangelogHeader();

            var dateTime = DateTime.Now.ToString("yy.MM.dd HH:mm");

            return "<!-- " +
                    Environment.NewLine +
                   $"    Changelog created {dateTime} using Cooke v{appVer}:" +
                    Environment.NewLine +
                   "    https://github.com/APrettyCoolProgram/Cooke" +
                    Environment.NewLine +
                   "-->" +
                    Environment.NewLine;
        }

        /// <summary>Builds the title of the changelog.</summary>
        public static string ChangelogTitle(string repoName, bool IncludeRepositoryNameInChangelog) => (IncludeRepositoryNameInChangelog)
            ? $"# {repoName} CHANGELOG" +
              Environment.NewLine
            : "# CHANGELOG" +
              Environment.NewLine;
    }
}