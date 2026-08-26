// u251023_code
// u251023_documentation

namespace cooke.Du;
internal class DuDirectory
{
    /// <summary>Verify each directory in the list exists, and create the directory if it does not.</summary>
    /// <param name="directories">The list of directories.</param>
    /// <example>
    ///     Calling Du.WithDirectories.Verify():
    ///     <code>
    ///         static readonly List<string> listOfDirectories = [
    ///             "directory01",
    ///             "directory03",
    ///             "directory02",
    ///         ];
    ///
    ///         Du.WithDirectory.Verify(listOfDirectories);
    ///     </code>
    /// </example>
    public static void Verify(List<string> directories)
    {
        foreach (var directory in directories)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }

    /// <summary>Delete a list of directories.</summary>
    /// <param name="directories">The list of directories.</param>
    /// <example>
    ///     Calling Du.WithDirectories.Delete():
    ///     <code>
    ///         static readonly List<string> listOfDirectories = [
    ///             "directory01",
    ///             "directory03",
    ///             "directory02",
    ///         ];
    ///
    ///         Du.WithDirectory.Delete(listOfDirectories);
    ///     </code>
    /// </example>
    public static void Delete(List<string> directories)
    {
        foreach (var directory in directories)
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }
        }
    }
}
