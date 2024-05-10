// 240509.1117

using Cooke.Session;

namespace Cooke.Message
{
    /// <summary>Generates the messages that are displayed to the user.</summary>
    public static class Catalog
    {
        /* Start
         */
        public static string StartCooke() =>
            $"============================================================{Environment.NewLine}" +
            $"                            Cooke                           {Environment.NewLine}" +
            $"============================================================{Environment.NewLine}";

        public static string LoadingConfiguration() =>
            $"Loading configuration...{Environment.NewLine}";

        /* Display configuration information
         */
        public static string CookeInfo(CkSession ckSession) =>
            $"Version: {ckSession.Version}{Environment.NewLine}" +
            $"Repository: {ckSession.RepositoryName}{Environment.NewLine}" +
            $"git command: \"{ckSession.GitLogCmd}\"{Environment.NewLine}";

        public static string CookeDetailedInfo(CkSession ckSession) =>
            $"Version: {ckSession.Version}{Environment.NewLine}" +
            $"Temp Path: {ckSession.TempPath}{Environment.NewLine}" +
            $"GitCmd Path: {ckSession.GitLogCmd}{Environment.NewLine}" +
            $"Detailed Info: {ckSession.DetailedInfo}{Environment.NewLine}" +
            $"Sleep Duration: {ckSession.SleepDuration}{Environment.NewLine}" +
            $"Repository Name: {ckSession.RepositoryName}{Environment.NewLine}" +
            $"Repository URL: {ckSession.RepositoryUrl}{Environment.NewLine}" +
            $"Changelog Repo Path: {ckSession.ChangelogMdPath}{Environment.NewLine}" +
            $"Include Changelog Name: {ckSession.IncludeRepositoryNameInChangelog}{Environment.NewLine}" +
            $"Start Tag: {ckSession.ChangelogStartTag}{Environment.NewLine}" +
            $"End Tag: {ckSession.ChangelogEndTag}{Environment.NewLine}" +
            $"Keep History: {ckSession.KeepHistory}{Environment.NewLine}";

        /* Errors
         */
        public static string InvalidCommandHeader()         => $"                      INVALID COMMAND                        {Environment.NewLine}";
        public static string InvalidCommand(string command) => $"The \"{command}\" command is invalid.{Environment.NewLine}";

        /* Help
         */
        public static string HelpValidCommands() =>
            $"Valid commands{Environment.NewLine}" +
             "--------------" +
            Environment.NewLine +
            $"          none  Generate all documentation.{Environment.NewLine}" +
            $"   \"changelog\"  Generate a CHANGELOG.md file.{Environment.NewLine}" +
            $"\"releasenotes\"  Generate a RELEASENOTES.md file.{Environment.NewLine}" +
            $"       \"reset\"  Reset the Cooke environment.{Environment.NewLine}" +
            Environment.NewLine +
            $"Examples:{Environment.NewLine}" +
            Environment.NewLine +
            $"  \"cooke\"            Generate all documentation{Environment.NewLine}" +
            $"  \"cooke changelog\"  Generate a CHANGELOG.md file.{Environment.NewLine}" +
            Environment.NewLine;

        /* Framework
         */
        public static string ResetCookeHeader()          => $"                      RESETTING COOKE                       {Environment.NewLine}";
        public static string ResetComplete()             => "Cooke has been reset!";
        public static string DeleteConfigurationFile()   => "Deleting configuration file...";
        public static string CreatingConfigurationFile() => "Creating configuration file...";
        public static string DeleteDirectories()         => "Deleting directories...";
        public static string VerifyingDirectories()      => "Verifying directories...";

        /* Changelog
         */
        public static string GenerateChangelogStart()               => $"                GENERATING CHANGELOG.MD FILE                {Environment.NewLine}";
        public static string ExportingGitLog()                      => "Exporting git log to gitlog.txt...";
        public static string ReadingGitlogTxt()                     => "Reading gitlog.txt...";
        public static string BuildingChangelogContent()             => "Building changelog content...";
        public static string ParsingVersionBlocks()                 => "Parsing version blocks...";
        public static string WritingChangelogTxt()                  => "Writing changelog.txt...";
        public static string FinalizingChangelogMd()                => "Finalizing CHANGELOG.md...";
        public static string BuildingChangelogHeader()              => "Building changelog header...";
        public static string WritingHistoricalFile(string dateTime) => $"Writing historical file: CHANGELOG_{dateTime}.md...";
        public static string WritingChangelogMd()                   => "Writing CHANGELOG.md...";
        public static string ChangelogGenerationComplete()          => "CHANGELOG.md generation complete!";
        public static string HelpHeader()                           => $"                         COOKE HELP                         {Environment.NewLine}";
    }
}