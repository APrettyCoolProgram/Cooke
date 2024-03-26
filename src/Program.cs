// =============================== Version 0.1.1 ===============================
// Cooke: Generate various release notes and changelogs for your projects.
// https://github.com/APrettyCoolProgram/Cooke
// Copyright (c) A Pretty Cool Program. All rights reserved.
// Licensed under the Apache 2.0 license.
//
// For details about this release, please see the local Source Code README.md:
//   Cooke/README.md
// =============================================================================

// b240326.0934

using Cooke;

namespace Namespace
{
    internal static class Program
    {
        /// <summary>Main entry point for Cooke.</summary>
        /// <param name="args">Arguments passed via the command line.</param>
        /// <remarks>
        /// - If no arguments are passed, the application will generate all of the available documents.
        /// - If any of the following arguments are passed, the application will create a specific document:
        ///     <c>changelog</c>: Generate a CHANGELOG.md file
        /// </remarks>
        private static void Main(string[] args)
        {
            const string configPath = "./cooke-config.json";

            Utility.DisplayMsg($"{Environment.NewLine}Cooke: Starting...");

            Utility.VerifyFramework(configPath);

            AppConfig appConfig = AppConfig.Load(configPath);

            Thread.Sleep(1000); // This needs to be here to work?

            if (args.Length > 0)
            {
                ExecuteComplex(appConfig, args);
            }
            else
            {
                Utility.DisplayMsg("Cooke: No argument passed, building all documentation...", appConfig.VerboseLog);
                Changelog.Generate(appConfig);
            }

            Utility.Cleanup();

            Utility.ExitApp($"Cooke: Process complete! [v{appConfig.AppVer}]");
        }

        /// <summary>Execute a complex command.</summary>
        /// <param name="appConfig">The Cooke configuration object.</param>
        /// <param name="args">The command line arguments.</param>
        /// <remarks>
        /// - Complex commands are commands that require additional arguments.
        /// - This method isn't used yet.
        /// </remarks>
        private static void ExecuteComplex(AppConfig appConfig, string[] args)
        {
            switch (args[0].ToLower())
            {
                case "changelog":
                    Changelog.Generate(appConfig);
                    break;

                case "releasenotes":
                    ReleaseNote.Generate(appConfig);
                    break;

                case "reset":
                    Utility.Reset(appConfig.AppVer);
                    break;


                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }
    }
}