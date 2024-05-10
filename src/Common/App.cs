// 240509.1117

namespace Cooke.Common
{
    /// <summary>Common application methods.</summary>
    public static class App
    {
        /// <summary>Exit Cooke.</summary>
        public static void Exit(string tempPath)
        {
            Cleanup(tempPath);
        }

        /// <summary>Cleanup framework before exiting.</summary>
        private static void Cleanup(string tempPath)
        {
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }
        }
    }
}
