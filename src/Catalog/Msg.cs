// b240319.1411

namespace Cooke.Catalog
{
    ///<summary>Pre-created messages.</summary>
    internal static class Msg
    {
        /// <summary>First execution message.</summary>
        /// <remarks>
        /// The first time Cooke is executed for a repository, there are a few housekeeping things we need to do. This
        /// message will let the user know.
        /// </remarks>
        /// <returns>The message displayed to the user when Cooke is executed for the first time.</returns>
        public static string FirstExecution() => Environment.NewLine +
                                                 $"  ======= {Environment.NewLine}" +
                                                 $"  WARNING {Environment.NewLine}" +
                                                 $"  ======= {Environment.NewLine}" +
                                                 Environment.NewLine +
                                                 $"  It looks like this is the first time you are running Cooke {Environment.NewLine}" +
                                                 $"  for this repository, so a default configuration file has {Environment.NewLine}" +
                                                  "  been created for you." +
                                                 Environment.NewLine +
                                                 Environment.NewLine +
                                                 $"  Please review the Cooke-config.json file and make any {Environment.NewLine}" +
                                                  "  necessary changes, then run Cooke again." +
                                                 Environment.NewLine;
    }
}