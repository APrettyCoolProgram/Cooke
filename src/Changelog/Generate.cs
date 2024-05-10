// 240509.1117

using Cooke.Message;
using Cooke.Session;

namespace Cooke.Changelog
{
    /// <summary>Generates a CHANGLOE.md file for a repository.</summary>
    public static class Generate
    {
        /// <summary>Generates a CHANGELOG.md file.</summary>
        public static void New(CkSession ckSession)
        {
            ToUser.DisplayGenerateChangelogStart();

            DotGit.Export.GitLogToFile(ckSession.GitLogCmd, ckSession.TempPath);

            BuildContent.Body(ckSession.TempPath, ckSession.Months, ckSession.ChangelogStartTag, ckSession.ChangelogEndTag, ckSession.SleepDuration);

            FinalChangelog(ckSession.TempPath, ckSession.ChangelogMdPath, ckSession.RepositoryName, ckSession.IncludeRepositoryNameInChangelog, ckSession.KeepHistory, ckSession.Version);
        }

        /// <summary>Finalizes the CHANGELOG.md file.</summary>
        public static void FinalChangelog(string tempPath, string repoPath, string repoName, bool IncludeRepositoryNameInChangelog, bool keepHistory, string appVer)
        {
            ToUser.DisplayFinalizingChangelogMd();

            var contentBody = File.ReadAllText($@"{tempPath}\changelog.txt");

            var finalContent = BuildContent.ContentHeader(appVer) +
                               BuildContent.ChangelogTitle(repoName, IncludeRepositoryNameInChangelog) +
                               contentBody;

            var dateTime= DateTime.Now.ToString("yyMMdd-HHmmss");

            if (keepHistory)
            {
                ToUser.DisplayWritingHistoricalFile(dateTime);
                File.WriteAllText($@"history\CHANGELOG_{dateTime}.md", finalContent);
            }

            ToUser.DisplayWritingChangelogMd();
            File.WriteAllText($@"{repoPath}\CHANGELOG.md", finalContent);

            ToUser.DisplayChangelogGenerationComplete();
        }
    }
}