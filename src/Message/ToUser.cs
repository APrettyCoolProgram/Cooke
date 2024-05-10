// 240509.1117

using Cooke.Session;

namespace Cooke.Message
{
    /// <summary>Displays various formatted messages to the user via the command line.</summary>
    public static class ToUser
    {
        /* Start
         */
        public static void DisplayStartCooke()
        {
            Du.WithCommandLine.DisplayText.InColor("dy", "b", Catalog.StartCooke());
            Du.WithCommandLine.DisplayText.InColor("b", "dy", Catalog.LoadingConfiguration());
        }

        /* Display configuration information
         */
        public static void DisplayConfiguration(CkSession ckSession)
        {
            if (ckSession.DetailedInfo)
            {
                Du.WithCommandLine.DisplayText.InColor("b", "dy", Catalog.CookeDetailedInfo(ckSession));
            }
            else
            {
                Du.WithCommandLine.DisplayText.InColor("b", "dy", Catalog.CookeInfo(ckSession));
            }
        }

        /* Errors
         */
        public static void DisplayInvalidCommand(string command)
        {
            Du.WithCommandLine.DisplayText.InColor("dr", "w", Catalog.InvalidCommandHeader());
            Du.WithCommandLine.DisplayText.InColor("b", "w", Catalog.InvalidCommand(command));
            Du.WithCommandLine.DisplayText.InColor("b", "w", Catalog.HelpValidCommands());
        }

        /* Help
         */
        public static void DisplayHelp()
        {
            Du.WithCommandLine.DisplayText.InColor("y", "b", Catalog.HelpHeader());
            Du.WithCommandLine.DisplayText.InColor("b", "w", Catalog.HelpValidCommands());
        }

        /* Framework
         */
        public static void DisplayResetCooke()              => Du.WithCommandLine.DisplayText.InColor("a", "b", Catalog.ResetCookeHeader());
        public static void DisplayResetComplete()           => Du.WithCommandLine.DisplayText.InColor("b", "g", Catalog.ResetComplete());
        public static void DisplayDeleteConfigurationFile() => Du.WithCommandLine.DisplayText.InColor("b", "w", Catalog.DeleteConfigurationFile());
        public static void DisplayCreateConfigurationFile() => Du.WithCommandLine.DisplayText.InColor("b", "w", Catalog.CreatingConfigurationFile());
        public static void DisplayDeleteDirectories()       => Du.WithCommandLine.DisplayText.InColor("b", "w", Catalog.DeleteDirectories());
        public static void DisplayVerifyDirectories()       => Du.WithCommandLine.DisplayText.InColor("b", "w", Catalog.VerifyingDirectories());

        /* Changelog
         */
        public static void DisplayGenerateChangelogStart()               => Du.WithCommandLine.DisplayText.InColor("dc", "b", Catalog.GenerateChangelogStart());
        public static void DisplayExportingGitLog()                      => Du.WithCommandLine.DisplayText.InColor("b", "w", Catalog.ExportingGitLog());
        public static void DisplayBuildingChangelogContent()             => Du.WithCommandLine.DisplayText.InColor("b", "w", Catalog.BuildingChangelogContent());
        public static void DisplayReadingGitLogTxt()                     => Du.WithCommandLine.DisplayText.InColor("b", "w", Catalog.ReadingGitlogTxt());
        public static void DisplayParsingVersionBlocks()                 => Du.WithCommandLine.DisplayText.InColor("b", "w", Catalog.ParsingVersionBlocks());
        public static void DisplayWritingChangelogTxt()                  => Du.WithCommandLine.DisplayText.InColor("b", "w", Catalog.WritingChangelogTxt());
        public static void DisplayFinalizingChangelogMd()                => Du.WithCommandLine.DisplayText.InColor("b", "w", Catalog.FinalizingChangelogMd());
        public static void DisplayBuildingChangelogHeader()              => Du.WithCommandLine.DisplayText.InColor("b", "w", Catalog.BuildingChangelogHeader());
        public static void DisplayWritingHistoricalFile(string dateTime) => Du.WithCommandLine.DisplayText.InColor("b", "w", Catalog.WritingHistoricalFile(dateTime));
        public static void DisplayWritingChangelogMd()                   => Du.WithCommandLine.DisplayText.InColor("b", "w", Catalog.WritingChangelogMd());
        public static void DisplayChangelogGenerationComplete()          => Du.WithCommandLine.DisplayText.InColor("b", "g", Catalog.ChangelogGenerationComplete());
    }
}