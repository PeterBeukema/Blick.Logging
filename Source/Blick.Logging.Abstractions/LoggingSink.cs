using System;
using Microsoft.Extensions.Logging;

namespace Blick.Logging.Abstractions;

public abstract class LoggingSink
{
    private readonly LoggerOptions options;

    protected LoggingSink(LoggerOptions options)
    {
        this.options = options;
    }

    public abstract void Log<TState>(
        string categoryName,
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter);

    protected bool IsEnabled(LogLevel logLevel)
        => options.MinimumLogLevel <= logLevel;
}