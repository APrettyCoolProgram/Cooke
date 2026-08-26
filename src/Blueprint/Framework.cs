// u251023_code
// u251023_documentation

namespace cooke.Blueprint;

/// <summary>Framework blueprints.</summary>
internal class Framework
{
    /// <summary>Cooke needs these folders to function properly.</summary>
    /// <returns>A list of folders.</returns>
    internal static List<string> RequiredFolders() =>
    [
        "./history",
        "./logs",
        "./temp"
    ];
}