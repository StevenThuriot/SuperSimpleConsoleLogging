using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace SuperSimpleConsoleLogging;

public static class SuperSimpleConsole
{
    public static IServiceCollection AddSuperSimpleConsoleLogging(this IServiceCollection services, bool verbose = false)
    {
        return services.AddLogging(c => c.AddSuperSimpleConsoleLogging(verbose));
    }

    public static void AddSuperSimpleConsoleLogging(this ILoggingBuilder builder, bool verbose = false)
    {
        builder.AddConsoleFormatter<SuperSimpleConsoleFormatter, ConsoleFormatterOptions>().AddConsole(o => o.FormatterName = nameof(SuperSimpleConsoleFormatter));
        builder.SetMinimumLevel(verbose ? LogLevel.Trace : LogLevel.Information);
    }
}