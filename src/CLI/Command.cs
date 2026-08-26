// u251023_code
// u251023_documentation

using System.Diagnostics;

namespace cooke.CLI;
internal class Command
{
    /// <summary>Execute a command.</summary>
    /// <param name="command">The command to execute.</param>
    /// <param name="arguments">Optional command arguments.</param>
    /// <param name="terminateAfterExecution">Determines if the console is terminated after executing the command.</param>
    internal static void Execute(string command, string arguments, bool terminateAfterExecution = true)
    {
        if (terminateAfterExecution)
        {
            Process.Start(command, $"/c {arguments}");
        }
        else
        {
            Process.Start(command, arguments);
        }
    }
}