// u251023_code
// u251023_documentation

using System.Reflection;
using cooke.Configuration;

namespace cooke.Session;

// <summary>Logic for the Cooke sessions.</summary>
internal class CookeSession
{
    /// <summary>Arguments passed via the command line.</summary>
    public string[] Args { get; set; }

    /// <summary>The current version of the Cooke application.</summary>
    public string Version { get; set; }

    /// <summary>The temporary data path.</summary>
    public string TempPath { get; set; }

    /// <summary>The git command used to export the commit data.</summary>
    public string GitLogCmd { get; set; }

    /// <summary>A list of months.</summary>
    public List<string> Months { get; set; }

    /// <summary>Sleep duration.</summary>
    public int SleepDuration { get; set; }

    /// <summary>Configuration settings for the Cooke application.</summary>
    public CookeConfig Config { get; set; }

    /// <summary>Starts a Cooke session.</summary>
    /// <param name="args">Arguments passed via the command line.</param>
    /// <param name="configPath">Path to the confiturationfile</param>
    internal static void Start(string[] args, string configPath)
    {
        Console.Clear();

        CLI.DisplayText.Colored(Blueprint.UserMessage.StartCooke(), "du", "r");

        Du.DuDirectory.Verify(Blueprint.Framework.RequiredFolders());

        CookeSession ckSession = Initialize(args, configPath);

        if (args.Length > 0)
        {
            CLI.Arguments.Parse(args, ckSession);
        }

        Generate.Changelog.New(ckSession);
        //Generate.ReleaseNote.New(ckSession);

        Stop(ckSession.TempPath);
    }

    /// <summary>Initializes a new Cooke session object.</summary>
    /// <param name="args">Arguments passed via the command line.</param>
    /// <param name="configPath">Path to the configuration file.</param>
    internal static CookeSession Initialize(string[] args, string configPath)
    {
        CLI.DisplayText.Colored(Blueprint.UserMessage.InitializeSession());

        return new CookeSession
        {
            Version       = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
            TempPath      = "./temp",
            GitLogCmd     = "git log --pretty=fuller", //"git log --pretty=format:%H|%an|%ad|%s --date=short",
            Args          = args,
            Months        = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
            SleepDuration = 1000,
            Config        = CookeConfig.Load(configPath)
        };
    }

    /// <summary>Stops a Cooke session.</summary>
    /// <param name="tempPath">The temporary data path.</param>
    internal static void Stop (string tempPath)
    {
        if (Directory.Exists(tempPath))
        {
            Directory.Delete(tempPath, true);
        }

        CLI.DisplayText.Colored(Blueprint.UserMessage.StopCooke());

        Environment.Exit(0);
    }
}