using Microsoft.Extensions.Logging.Console;

namespace SuperSimpleConsoleLogging;

public class SuperSimpleConsoleFormatterOptions : ConsoleFormatterOptions
{
    /// <summary>
    /// Colorize console output
    /// </summary>
    /// <remarks>Default is false</remarks>
    public bool Colorized { get; set; }

    /// <summary>
    /// Prefix log lines with the severity level
    /// </summary>
    /// <remarks>Default is true</remarks>
    public bool PrefixLevel { get; set; } = true;

    /// <summary>
    /// Only write the exception message instead of the whole thing
    /// </summary>
    /// <remarks>Default is false</remarks>
    public bool ExceptionMessageOnly { get; set; }

    /// <summary>
    /// Auto replace newlines in text when not null
    /// </summary>
    /// <remarks>Default is 1 space character</remarks>
    public string ReplaceNewLine { get; set; } = " ";
}
