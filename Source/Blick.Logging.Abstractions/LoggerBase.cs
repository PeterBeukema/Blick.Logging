using System;
using Microsoft.Extensions.Logging;

namespace Blick.Logging.Abstractions;

public abstract class LoggerBase : ILogger
{
    protected readonly string CategoryName;
    protected readonly LoggerOptions Options;

    protected LoggerBase(string categoryName, LoggerOptions options)
    {
        CategoryName = categoryName;
        Options = options;
    }

    public abstract void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter);

    public virtual bool IsEnabled(LogLevel logLevel)
        => Options.MinimumLogLevel <= logLevel;

    public virtual IDisposable? BeginScope<TState>(TState state)
        where TState : notnull
        => default!;
}