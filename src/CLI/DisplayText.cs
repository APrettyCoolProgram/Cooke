// u251023_code
// u251023_documentation

namespace cooke.CLI;

/// <summary>
/// Provides functionality to display text in the console with specified foreground and background colors.
/// </summary>
/// <remarks>The <paramref name="background"/> and <paramref name="foreground"/> parameters accept shorthand color
/// codes. For example, use "r" for red, "dm" for dark magenta, "b" for black, and "w" for white. If no colors are
/// specified, the default is a black background and white foreground.</remarks>
internal class DisplayText
{
    /// <summary>Display colored text in the console.</summary>
    /// <param name="textToDisplay">The text to display</param>
    /// <param name="background">The background color of the text</param>
    /// <param name="forground">The foreground color of the text.</param>
    /// <remarks>
    ///     - The background and foreground colors are the first letter(s) of the color that is being converted.
    ///       ex: "r" for red, "dm" for dark magenta
    /// </remarks>
    /// <example>
    ///     To display black background, white text using Du.WithConsole.DisplayText():
    ///     <code>
    ///         Du.WithConsole.DisplayText("Hello, World!");
    ///     </code>
    ///     To display red background, green text using Du.WithConsole.DisplayText():
    ///     <code>
    ///         Du.WithConsole.DisplayText("Hello, World!", "r", "g");
    ///     </code>
    /// </example>
    public static void Colored(string textToDisplay, string background = "b", string forground = "w")
    {
        Console.BackgroundColor = Blueprint.TextColor.GetColor(background);
        Console.ForegroundColor = Blueprint.TextColor.GetColor(forground);
        Console.WriteLine(textToDisplay);
        Console.ResetColor();
    }
}