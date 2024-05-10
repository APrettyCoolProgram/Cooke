// 240509.1117

using Cooke.Message;
using Cooke.Session;

namespace Cooke.Common
{
    /// <summary>Common framework methods.</summary>
    public static class Framework
    {
        static readonly List<string> requiredDirectories = [
            "./history",
            "./logs",
            "./temp",
        ];

        /// <summary>Reset Cooke to its default state.</summary>
        public static void Reset()
        {
            ToUser.DisplayResetCooke();
            Configuration.Reset();

            ToUser.DisplayDeleteDirectories();
            Du.WithDirectory.Delete.ListOf(requiredDirectories);

            ToUser.DisplayVerifyDirectories();
            Du.WithDirectory.Verify.ListOf(requiredDirectories);

            ToUser.DisplayResetComplete();
        }

        /// <summary>Verify Cooke framework components.</summary>
        public static void Verify()
        {
            Configuration.Verify();
            Du.WithDirectory.Verify.ListOf(requiredDirectories);
        }
    }
}