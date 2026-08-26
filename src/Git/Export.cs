// u251023_code
// u251023_documentation

namespace cooke.Git;
internal class Export
{
    /// <summary>Exports the git log to a file.</summary>
    /// <param name="gitLogCmd">The git log command.</param>
    /// <param name="exportPath">The temporary data path.</param>
    public static void GitLogToFile(string gitLogCmd, string exportPath)
    {
        CLI.DisplayText.Colored(Blueprint.UserMessage.ExportGitLog());

        CLI.Command.Execute("cmd.exe", $@"{gitLogCmd} > {exportPath}");
    }
}
