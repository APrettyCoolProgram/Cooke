// u251023_code
// u251023_documentation

using cooke.Configuration;
using cooke.Session;

namespace cooke.Generate;
internal class Changelog
{
    internal static void New(CookeSession ckSession)
    {
        CLI.DisplayText.Colored(Blueprint.UserMessage.GenerateChangelogStart());

        ExportGitLog(ckSession.GitLogCmd, $@"{ckSession.TempPath}/gitlog.export");
        BuildContent(ckSession.TempPath, ckSession.Months, ckSession.SleepDuration, ckSession.Config);
        FinalChangelog(ckSession.TempPath, ckSession.Config.GeneratedChangelogPath, ckSession.Config.RepoName, ckSession.Config.DisplayRepoName, ckSession.Config.KeepHistory, ckSession.Version);
    }
    private static void ExportGitLog(string gitLogCmd, string exportPath)
    {
        Git.Export.GitLogToFile(gitLogCmd, exportPath);
    }

    private static void BuildContent(string tempPath, List<string> months, int sleepDuration, CookeConfig ckConfig)
    {
        CLI.DisplayText.Colored(Blueprint.UserMessage.BuildChangelogContent());

        /* OK, so for some reason, the gitlog.txt file is being written too slowly (??), and there needs to be a
           * short pause before continuing.
           *
           * The sleepDuration is set to 1000ms (1 second) by default, but can be adjusted in the config file.
           */
        Thread.Sleep(sleepDuration);

        CLI.DisplayText.Colored(Blueprint.UserMessage.ReadGitLog());

        var version = "Current development";

        var changelogVersionBlocks = new Dictionary<string,List<string>>()
            {
                { version, new List<string>() }
            };

        //var releaseNotes = new Dictionary<string,List<string>>();
        var releaseNotes = "";
        var commitDate      = "";
        var commitDateStamp = "";
        var contentBody     = "";

        using (StreamReader gitlogTxt = new StreamReader($@"{tempPath}\gitlog.export"))
        {
            string logLine;
            string relCatch = "";
            bool relFlag = false;
            string relSub ="";
            string relVer = "undefined";
            bool relStop = false;
            var rnotes ="";

            while ((logLine = gitlogTxt.ReadLine()) != null)
            {
                logLine = logLine.Trim();

                if (logLine.StartsWith("CommitDate"))
                {
                    commitDate = logLine;
                }

                if (logLine.StartsWith(ckConfig.CookeStartTag))
                {
                    if (logLine.StartsWith($"{ckConfig.CookeStartTag}VERSION{ckConfig.CookeEndTag}"))
                    {
                        version = logLine.Replace($"{ckConfig.CookeStartTag}VERSION{ckConfig.CookeEndTag} ", "");

                        if (!changelogVersionBlocks.ContainsKey(version))
                        {
                            changelogVersionBlocks.Add(version,
                            [
                                commitDate
                            ]);
                        }

                        if (relVer == "undefined")
                        {
                            relVer = version;
                        }

                        //relFlag= true;
                        //relSub = logLine;
                    }

                    if (logLine.StartsWith(ckConfig.CookeStartTag) && !logLine.StartsWith($"{ckConfig.CookeStartTag}VERSION{ckConfig.CookeEndTag}"))
                    {
                        changelogVersionBlocks[version].Add($"{logLine}  ");

                    }
                }

                if (logLine.StartsWith("##"))
                {
                    changelogVersionBlocks[version].Add($"{logLine}  ");
                }
            }


            if (relVer != "undefined")
            {
                rnotes = GetRelNotes(relVer, changelogVersionBlocks[relVer], ckConfig.CookeStartTag, ckConfig.CookeEndTag);
            }

            contentBody = ParseVersionBlocks(months, ckConfig.CookeStartTag, ckConfig.CookeEndTag, changelogVersionBlocks, commitDateStamp, contentBody);

            CLI.DisplayText.Colored(Blueprint.UserMessage.WriteChangelogTxt());

            File.WriteAllText($@"{tempPath}\changelog.txt", contentBody);
            //File.WriteAllText($@"{ckConfig.GeneratedChangelogPath}\CHANGELOG.md", contentBody);


            //CLI.DisplayText.Colored(Blueprint.UserMessage.WriteReleaseNotesTxt());

            //File.WriteAllText($@"{tempPath}\release-notes.md", rnotes);
            //File.WriteAllText($@"{ckConfig.GenerateReleaseNotesPath}\RELEASE-NOTES.md", rnotes);
        }
    }
    /// <summary>Parses the version blocks from the git log.</summary>
    public static string ParseVersionBlocks(List<string> months, string startTag, string endTag, Dictionary<string, List<string>> changelogVersionBlocks, string commitDateStamp, string contentBody)
    {
        CLI.DisplayText.Colored(Blueprint.UserMessage.ParseVersionBlocks());

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

    public static string GetRelNotes(string relVer, List<string> verDeets, string startTag, string endTag)
    {
        var relNotes = $"# AppName v{relVer} Release Notes<br/><br/>";

        foreach (var line in verDeets)
        {
            //MessageUser.DisplayDebugMessage(line);

            if (line.StartsWith(startTag))
            {
                var relSub = line.Replace(startTag, "`").Replace(endTag, "`");

                relNotes += $"{relSub}<br/>";
            }

            if (line.StartsWith("##"))
            {
                //CLI.DisplayText.Colored(Blueprint.UserMessage.DebugMessage(line));  //???

                var relNote = line.Replace("##", "").Trim();

                relNotes += $"{relNote}<br/>";
            }
        }

        // If there aren't any changes, add a note to that effect.

        return relNotes;
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
            bodyContent = "> This release does not contain changes.  ";
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
        CLI.DisplayText.Colored(Blueprint.UserMessage.BuildChangelogHeader());

        var dateTime = DateTime.Now.ToString("yy.MM.dd HH:mm");

        return "<!-- " +
                Environment.NewLine +
               $"    Changelog created {dateTime} using Cooke v{appVer}{Environment.NewLine}" +
                Environment.NewLine +
               "    https://github.com/APrettyCoolProgram/Cooke" +
                Environment.NewLine +
               "-->" +
                Environment.NewLine;
    }

    public static void FinalChangelog(string tempPath, string repoPath, string repoName, bool IncludeRepositoryNameInChangelog, bool keepHistory, string appVer)
    {
        CLI.DisplayText.Colored(Blueprint.UserMessage.FinalizeChangelogMd());

        var contentBody = File.ReadAllText($@"{tempPath}\changelog.txt");

        var finalContent = Changelog.ContentHeader(appVer) +
                           Changelog.ChangelogTitle(repoName, IncludeRepositoryNameInChangelog) +
                           contentBody;

        var dateTime= DateTime.Now.ToString("yyMMdd-HHmmss");

        if (keepHistory)
        {
            CLI.DisplayText.Colored(Blueprint.UserMessage.WriteHistoricalFile(dateTime));
            File.WriteAllText($@"history\CHANGELOG_{dateTime}.md", finalContent);
        }

        CLI.DisplayText.Colored(Blueprint.UserMessage.WriteChangelogMd());
        File.WriteAllText($@"{repoPath}\CHANGELOG.md", finalContent);

        CLI.DisplayText.Colored(Blueprint.UserMessage.ChangelogGenerationComplete());
    }


    /// <summary>Builds the title of the changelog.</summary>
    public static string ChangelogTitle(string repoName, bool IncludeRepositoryNameInChangelog) => (IncludeRepositoryNameInChangelog)
        ? $"# {repoName} CHANGELOG" +
          Environment.NewLine
        : "# CHANGELOG" +
          Environment.NewLine;
}