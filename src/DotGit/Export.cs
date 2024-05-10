// 240509.1117

namespace Cooke.DotGit
{
    /// <summary>Exports git data to a file.</summary>
    public static class Export
    {
        /// <summary>Exports the git log to a file.</summary>
        /// <param name="gitLogCmd">The git log command.</param>
        /// <param name="tempPath">The temporary data path.</param>
        public static void GitLogToFile(string gitLogCmd, string tempPath)
        {
            Message.ToUser.DisplayExportingGitLog();

            Du.WithSystem.Command.Execute("cmd.exe", $@"{gitLogCmd} > {tempPath}\gitlog.txt");
        }
    }
}
