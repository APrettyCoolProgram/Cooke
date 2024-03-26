// b240326.0935

namespace Cooke.Catalog
{
    /// <summary>Pre-created directory lists.</summary>
    internal static class DirList
    {
        /// <summary>List of required directories.</summary>
        /// <returns>The list of required directories.</returns>
        public static List<string> RequiredDirectories =>
        [
            "./changelog",
            "./releasenotes",
            "./temp"
        ];
    }
}