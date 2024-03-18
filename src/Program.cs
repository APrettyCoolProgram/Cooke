// =============================================================================
// Cooke: Generate various release notes and changelogs for your projects.
// https://github.com/APrettyCoolProgram/Cooke
// Copyright (c) A Pretty Cool Program. All rights reserved.
// Licensed under the Apache 2.0 license.
//
// For details about this release, please see the local Source Code README.md:
//   Cooke/README.md
// =============================================================================

// b240318.0737

using Cooke;

namespace Namespace
{
    internal static class Program
    {
        /// <summary>
        /// Main entry point for the Cooke.
        /// </summary>
        /// <param name="args">Arguments passed via the command line.</param>
        private static void Main(string[] args)
        {
            Console.WriteLine($"{Environment.NewLine}Cooke: Starting...");

            Utility.VerifyRequirements();

            AppConfig appConfig = AppConfig.LoadConfigFile();

            Thread.Sleep(1000); // This needs to be here to work?

            if (args.Length > 0)
            {
                ExecuteComplex(appConfig, args);
            }
            else
            {
                Changelog.Generate(appConfig);
            }

            var pause = true;

            Utility.Cleanup();
        }

        /// <summary>
        /// Execute a complex command.
        /// </summary>
        /// <param name="appConfig">The Cooke configuration object.</param>
        /// <param name="arguments">The command line arguments.</param>
        private static void ExecuteComplex(AppConfig appConfig, string[] arguments)
        {
            switch (arguments[0].ToLower())
            {
                case "changelog":
                    Changelog.Generate(appConfig);
                    break;
                case "releasenotes":
                    ReleaseNote.Generate(appConfig);
                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }
    }
}