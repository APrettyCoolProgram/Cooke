// b240318.0844

using System;
using System.Diagnostics;

namespace Cooke
{
    internal class Utility
    {
        /// <summary>
        /// Executes a system command.
        /// </summary>
        /// <param name="command">The system command.</param>
        /// <param name="argument">The command arguments.</param>
        /// <param name="terminate">Determines if the command should terminate after processing.</param>
        /// <remarks>
        ///     - <c>processName</c> example: "cmd"
        ///     - <c>processArgument</c> example: "git log"
        /// </remarks>
        /// <see href="https://gist.github.com/APrettyCoolProgram/9e0e3b02632ddb7f8a309783cc2cee10">This is A Pretty Cool Program Gist</see>
        public static void ExecuteSystemCommand(string command,string argument,bool terminate = true)
        {
            if (terminate)
            {
                Process.Start(command,$"/c {argument}");
            }
            else
            {
                Process.Start(command,argument);
            }
        }


        /// <summary>
        /// Verify the Cooke framework.
        /// </summary>
        public static void VerifyRequirements()
        {
            Console.WriteLine($"Cooke: Verifying framework...");

            VerifyAppConfigFile();
            VerifyRequiredDirectories();
        }

        /// <summary>
        /// Verify the Cooke-config.json file exists, and create it if it does not.
        /// </summary>
        private static void VerifyAppConfigFile()
        {
            if (!File.Exists(@"./Cooke-config.json"))
            {
                AppConfig.CreateDefaultConfigFile();
            }
        }

        /// <summary>
        /// Verify the directory where Cooke data will be stored.
        /// </summary>
        public static void VerifyRequiredDirectories()
        {
            var requiredDirectories = new List<string>
            {
                "./changelog",
                "./releasenotes",
                "./temp"
            };

            foreach (var directory in requiredDirectories)
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }
        }

        /// <summary>
        /// Clean up Cooke.
        /// </summary>
        public static void Cleanup()
        {
            Console.WriteLine($"Cooke: Cleaning up..{Environment.NewLine}");

            if (Directory.Exists("./temp"))
            {
                Directory.Delete("./temp", true);
            }
        }
    }
}