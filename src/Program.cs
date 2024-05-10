// ================================================================= 0.2.0 =====
// Cooke: Generate changelogs and release notes for git repositories.
// https://github.com/APrettyCoolProgram/Cooke
// Copyright (c) A Pretty Cool Program. All rights reserved.
// Licensed under the Apache 2.0 license.
// ================================================================ 240509 =====

// 240509.1117

using Cooke.Common;
using Cooke.Message;
using Cooke.Session;

namespace Cooke
{
    internal static class Program
    {
        /// <summary>Cooke!</summary>
        /// <param name="passedArguments">Arguments passed via the command line.</param>
        /// <remarks>
        ///     - Only the first argument (the "command") is parsed, all other arguments are ignored.
        /// </remarks>
        private static void Main(string[] passedArguments)
        {
            Console.Clear();
            Framework.Verify();
            ToUser.DisplayStartCooke();

            var ckSession = CkSession.Load();
            ToUser.DisplayConfiguration(ckSession);

            if (passedArguments.Length == 0)
            {
                Changelog.Generate.New(ckSession);
            }
            else
            {
                ParseCommand(passedArguments[0], ckSession);
            }

            App.Exit(ckSession.TempPath);
        }

        /// <summary> Parse the command passed via the command line.</summary>
        /// <param name="command">The passed argument.</param>
        /// <param name="ckSession">The Cooke session object.</param>
        private static void ParseCommand(string command, CkSession ckSession)
        {
            switch (command.ToLower())
            {
                case "changelog":
                    Changelog.Generate.New(ckSession);
                    break;

                //case "releasenotes":
                //    //ReleaseNote.Generate.New(coSession);
                //    break;

                case "reset":
                    Framework.Reset();
                    break;

                case "help":
                    ToUser.DisplayHelp();
                    break;

                default:
                    ToUser.DisplayInvalidCommand(command);
                    break;
            }
        }
    }
}