using Microsoft.Extensions.Logging.Console;

namespace SuperSimpleConsoleLogging;

public class SuperSimpleConsoleFormatterOptions : ConsoleFormatterOptions
{
    /// <summary>
    /// Colorize console output
    /// </summary>
    /// <remarks>Default is false</remarks>
    public bool Colorized { get; set; }
}
