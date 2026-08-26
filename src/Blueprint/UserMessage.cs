// u251023_code
// u251023_documentation

namespace cooke.Blueprint;

/// <summary>User message blueprints.</summary>
internal class UserMessage
{
    internal static string StartCooke() =>
        $"============================================================{Environment.NewLine}" +
        $"                           Cooke                            {Environment.NewLine}" +
        $"============================================================{Environment.NewLine}";

    internal static string StopCooke() =>
       """

       EXITING COOKE...
       """;

    internal static string InitializeSession() =>
       """
       INITIALIZING SESSION...
       """;

    internal static string LoadConfig() =>
        """
        LOADING CONFIGURATION...
        """;

    internal static string CreateConfigFile() =>
        """
        CONFIGURATION FILE NOT FOUND! CREATING...
        """;

    internal static string GenerateChangelogStart() =>
        """
        GENERATING CHANGELOG FILE...
        """;
    internal static string ExportGitLog() =>
        """
          Exporting git log...
        """;

    internal static string ReadGitLog() =>
        """
          Reading git log...
        """;

    internal static string BuildChangelogContent() =>
        """
          Building changelog content...
        """;
    internal static string ParseVersionBlocks() =>
        """
          Parsing version blocks...
        """;

    internal static string WriteChangelogTxt() =>
        """
          Writing "changelog.txt"...
        """;

    internal static string FinalizeChangelogMd() =>
        """
          Finalizing "CHANGELOG.md"...
        """;

    internal static string BuildChangelogHeader() =>
        """
          Building changelog header...
        """;

    internal static string WriteHistoricalFile(string dateTime) =>
        $"  Writing historical file: \"CHANGELOG_{dateTime}.md\"...";

    internal static string WriteChangelogMd() =>
        """
          Writing "CHANGELOG.md"...
        """;

    internal static string ChangelogGenerationComplete() =>
        """
        CHANGELOG GENERATION COMPLETE!
        """;

    internal static string Info(Session.CookeSession ckSession) =>
        Environment.NewLine +
        $"         Cooke information{Environment.NewLine}" +
        $"--------------------------{Environment.NewLine}" +
        $"                   Version: {ckSession.Version}{Environment.NewLine}" +
        $"                 Arguments: {string.Join(" ", ckSession.Args)}{Environment.NewLine}" +
        $"               Git commamd: {ckSession.GitLogCmd}{Environment.NewLine}" +
        $"           Repository name: {ckSession.Config.RepoName}\"{Environment.NewLine}" +
        $"            Repository URL: {ckSession.Config.RepoUrl}\"{Environment.NewLine}" +
        $"   Display repository name: {ckSession.Config.DisplayRepoName}{Environment.NewLine}" +
        $"         CHANGELOG.md path: {ckSession.Config.GeneratedChangelogPath}\"{Environment.NewLine}" +
        $"           Cooke start tag: {ckSession.Config.CookeStartTag}\"{Environment.NewLine}" +
        $"             Cooke end tag: {ckSession.Config.CookeEndTag}\"{Environment.NewLine}" +
        $"     RELEASE_NOTES.md path: {ckSession.Config.GenerateReleaseNotesPath}\"{Environment.NewLine}" +
        $"  Release note comment tag: {ckSession.Config.CookeStartTag}\"{Environment.NewLine}" +
        $"            Sleep duration: {ckSession.SleepDuration}{Environment.NewLine}" +
        $"              Keep history: {ckSession.Config.KeepHistory}";
}