using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace SuperSimpleConsoleLogging;

internal sealed class SuperSimpleConsoleFormatter : ConsoleFormatter, IDisposable
{
    private readonly IDisposable _optionsReloadToken;
    private SuperSimpleConsoleFormatterOptions FormatterOptions { get; set; }

    public SuperSimpleConsoleFormatter(IOptionsMonitor<SuperSimpleConsoleFormatterOptions> options)
        : base(nameof(SuperSimpleConsoleFormatter))
    {
        FormatterOptions = options.CurrentValue;
        _optionsReloadToken = options.OnChange(o => FormatterOptions = o);
    }

    public void Dispose()
    {
        _optionsReloadToken?.Dispose();
    }

    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider scopeProvider, TextWriter textWriter)
    {
        string text = logEntry.Formatter!(logEntry.State, logEntry.Exception);
        if (logEntry.Exception == null && text == null)
        {
            return;
        }

        bool inColor = false;

        LogLevel logLevel = logEntry.LogLevel;
        Exception? exception = logEntry.Exception;

        var syslogSeverityString = GetLogLevelString(logLevel);
        if (syslogSeverityString != default)
        {
            if (FormatterOptions.Colorized && syslogSeverityString.color is not null)
            {
                inColor = true;
                textWriter.Write(syslogSeverityString.color);
            }

            textWriter.Write(syslogSeverityString.syslogSeverity);
            textWriter.Write(": ");
        }

        string timestampFormat = FormatterOptions.TimestampFormat;

        if (timestampFormat is not null)
        {
            textWriter.Write(GetCurrentDateTime().ToString(timestampFormat));
            textWriter.Write(' ');
        }

        if (!string.IsNullOrEmpty(text))
        {
            WriteReplacingNewLine(text);
        }

        if (exception is not null)
        {
            textWriter.Write(' ');
            WriteReplacingNewLine(exception.ToString());
        }

        if (inColor)
        {
            textWriter.Write(Reset);
        }

        textWriter.Write(Environment.NewLine);

        void WriteReplacingNewLine(string message)
        {
            string value = message.Replace(Environment.NewLine, " ");
            textWriter.Write(value);
        }
    }

    private DateTimeOffset GetCurrentDateTime() => FormatterOptions.UseUtcTimestamp ? DateTimeOffset.UtcNow : DateTimeOffset.Now;

    const string Black =  "\u001b[30m";
    const string Red =  "\u001b[31m";
    const string Green =  "\u001b[32m";
    const string Yellow =  "\u001b[33m";
    const string Blue =  "\u001b[34m";
    const string Magenta =  "\u001b[35m";
    const string Cyan =  "\u001b[36m";
    const string White =  "\u001b[37m";
    const string Reset = "\u001b[0m";
    private static (string syslogSeverity, string? color) GetLogLevelString(LogLevel logLevel) => logLevel switch
    {
        LogLevel.Trace => ("trce", null),
        LogLevel.Debug => ("dbug", Magenta),
        //LogLevel.Information => "info",
        LogLevel.Warning => ("warn", Yellow),
        LogLevel.Error => ("fail", Red),
        LogLevel.Critical => ("crit", Red),
        _ => default,
    };
}