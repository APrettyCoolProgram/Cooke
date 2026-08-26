// =============================================================================
// https://github.com/aprettycoolprogram/cooke
// u2251023_code
// u251124_documentation
// =============================================================================

namespace cooke
{
    /// <summary>Entry class for the Cooke application.</summary>
    internal class Program
    {
        /// <summary>Entry method for the Cooke application.</summary>
        /// <param name="args">Arguments passed via the command line.</param>
        static void Main(string[] args) => Session.CookeSession.Start(args, "./cooke.config");
    }
}