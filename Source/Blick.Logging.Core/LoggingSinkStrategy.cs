using System;
using System.Collections.Generic;
using System.Linq;
using Blick.Logging.Abstractions;
using Microsoft.Extensions.Logging;

namespace Blick.Logging.Core;

public class LoggingSinkStrategy : ILogger
{
    private readonly string categoryName;
    private readonly LoggerOptions options;
    private readonly IEnumerable<LoggingSink> sinks;

    public LoggingSinkStrategy(
        string categoryName,
        LoggerOptions options,
        IEnumerable<LoggingSink> sinks)
    {
        this.categoryName = categoryName;
        this.options = options;
        this.sinks = sinks;
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!sinks.Any())
        {
            return;
        }
        
        foreach (var sink in sinks)
        {
            sink.Log(categoryName, logLevel, eventId, state, exception, formatter);
        }
    }

    public bool IsEnabled(LogLevel logLevel)
        => options.MinimumLogLevel <= logLevel;

    public IDisposable BeginScope<TState>(TState state)
        where TState : notnull
        => default!;
}