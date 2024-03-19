// b240319.1411

using System.Diagnostics;

namespace Cooke
{
    /// <summary>Utility methods for Cooke.</summary>
    internal static class Utility
    {

        /// <summary>Verify the Cooke framework.</summary>
        public static void VerifyFramework(string configPath)
        {
            Utility.DisplayMsg("Cooke: Verifying framework...");

            AppConfig.Verify(configPath);
            VerifyRequiredDirectories();
        }

        /// <summary>Verify framework directories exist, and create them if they do not.</summary>
        private static void VerifyRequiredDirectories()
        {
            foreach (var dir in Catalog.DirList.RequiredDirectories)
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
        }

        /// <summary>Executes a system command.</summary>
        /// <param name="cmd">The system command.</param>
        /// <param name="arg">The command arguments.</param>
        /// <param name="terminate">Determines if the command should terminate after processing.</param>
        /// <remarks>
        /// - <c>processName</c> example: "cmd"
        /// - <c>processArgument</c> example: "git log"
        /// </remarks>
        /// <see href="https://gist.github.com/APrettyCoolProgram/9e0e3b02632ddb7f8a309783cc2cee10">View the gist</see>
        public static void ExeSysCmd(string cmd, string arg, bool terminate = true)
        {
            if (terminate)
            {
                Process.Start(cmd, $"/c {arg}");
            }
            else
            {
                Process.Start(cmd, arg);
            }
        }

        /// <summary>Display a message to the user.</summary>
        /// <param name="msg"></param>
        /// <param name="verbose"></param>
        public static void DisplayMsg(string msg, bool verbose = true)
        {
            if (verbose)
            {
                Console.WriteLine(msg);
            }
        }

        /// <summary>Reset Cooke to its default state.</summary>
        public static void Reset(string appVer)
        {
            Utility.DisplayMsg("Cooke: Resetting...");

            foreach (var dir in Catalog.DirList.RequiredDirectories)
            {
                if (Directory.Exists($"./{dir}"))
                {
                    Directory.Delete($"./{dir}", true);
                }
            }

            if (File.Exists("./cooke-config.json"))
            {
                File.Delete("./cooke-config.json");
            }

            Utility.ExitApp($"Cooke: Process complete! [v{appVer}]");
        }


        /// <summary>Clean up Cooke.</summary>
        public static void Cleanup()
        {
            Utility.DisplayMsg("Cooke: Cleaning up...");

            if (Directory.Exists("./temp"))
            {
                Directory.Delete("./temp", true);
            }
        }

        /// <summary>Exits the application gracefully.</summary>
        /// <param name="exitMsg">The exit message that is displayed to the user.</param>
        /// <param name="exitCode">The exit code.</param>
        public static void ExitApp(string exitMsg, int exitCode = 0)
        {
            Console.WriteLine(exitMsg);
            Environment.Exit(exitCode);
        }
    }
}