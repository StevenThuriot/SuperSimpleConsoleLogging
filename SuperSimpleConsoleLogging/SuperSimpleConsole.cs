using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SuperSimpleConsoleLogging;

public static class SuperSimpleConsole
{
    public static IServiceCollection AddSuperSimpleConsoleLogging(this IServiceCollection services, bool verbose = false)
    {
        return services.AddLogging(c => c.AddSuperSimpleConsoleLogging(verbose));
    }

    public static IServiceCollection AddSuperSimpleConsoleLogging(this IServiceCollection services, Action<SuperSimpleConsoleFormatterOptions> configure, bool verbose = false)
    {
        return services.AddLogging(c => c.AddSuperSimpleConsoleLogging(configure, verbose));
    }

    public static ILoggingBuilder AddSuperSimpleConsoleLogging(this ILoggingBuilder builder, bool verbose = false)
    {
        return builder.AddConsoleFormatter<SuperSimpleConsoleFormatter, SuperSimpleConsoleFormatterOptions>().AddSuperSimpleConsoleLoggingInternal(verbose);
    }

    public static ILoggingBuilder AddSuperSimpleConsoleLogging(this ILoggingBuilder builder, Action<SuperSimpleConsoleFormatterOptions> configure, bool verbose = false)
    {
        return builder.AddConsoleFormatter<SuperSimpleConsoleFormatter, SuperSimpleConsoleFormatterOptions>(configure).AddSuperSimpleConsoleLoggingInternal(verbose);
    }

    private static ILoggingBuilder AddSuperSimpleConsoleLoggingInternal(this ILoggingBuilder builder, bool verbose)
    {
        return builder.AddConsole(o => o.FormatterName = nameof(SuperSimpleConsoleFormatter))
                      .SetMinimumLevel(verbose ? LogLevel.Trace : LogLevel.Information);
    }
}