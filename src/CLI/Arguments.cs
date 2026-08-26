// u251023_code
// u251023_documentation

using cooke.Session;

namespace cooke.CLI;

/// <summary>Logic related to arguments passed to Cooke.</summary>
internal class Arguments
{
    /// <summary>
    /// Parses the provided command-line arguments and performs the corresponding action on the specified session.
    /// </summary>
    /// <remarks>The method processes the first argument to determine the requested action.  Supported
    /// commands include "info", "-info", and "/info", which display details about the session.</remarks>
    /// <param name="args">An array of command-line arguments. The first argument specifies the command to execute.</param>
    /// <param name="ckSession">The session object on which the command will operate.</param>
    internal static void Parse(string[] args, CookeSession ckSession)
    {
        if (args[0].ToLower() is "info" or "-info" or "/info")
        {
            DisplaySessionDetails(ckSession);
        }
    }

    /// <summary> Displays details about the specified CookeSession and stops the session.</summary>
    /// <param name="ckSession">The <see cref="CookeSession"/> instance whose details are to be displayed and stopped.</param>
    internal static void DisplaySessionDetails(CookeSession ckSession)
    {
        DisplayText.Colored(Blueprint.UserMessage.Info(ckSession), "b", "w");

        CookeSession.Stop(ckSession.TempPath);
    }
}