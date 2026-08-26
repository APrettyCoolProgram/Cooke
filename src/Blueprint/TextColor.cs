// u251023_code
// u251023_documentation

namespace cooke.Blueprint;

/// <summary>Text color conversion blueprints.</summary>
internal class TextColor
{
    /// <summary>Convert a string to a ConsoleColor</summary>
    /// <param name="colorCode">The string to convert.</param>
    /// <returns>A ConsoleColor.</returns>
    internal static ConsoleColor GetColor(string colorCode) => colorCode.ToLower() switch
    {
        "b"  => ConsoleColor.Black,
        "u"  => ConsoleColor.Blue,
        "c"  => ConsoleColor.Cyan,
        "du" => ConsoleColor.DarkBlue,
        "dc" => ConsoleColor.DarkCyan,
        "da" => ConsoleColor.DarkGray,
        "dg" => ConsoleColor.DarkGreen,
        "dm" => ConsoleColor.DarkMagenta,
        "dr" => ConsoleColor.DarkRed,
        "dy" => ConsoleColor.DarkYellow,
        "a"  => ConsoleColor.Gray,
        "g"  => ConsoleColor.Green,
        "m"  => ConsoleColor.Magenta,
        "r"  => ConsoleColor.Red,
        "w"  => ConsoleColor.White,
        "y"  => ConsoleColor.Yellow,
        _    => ConsoleColor.White,
    };
}