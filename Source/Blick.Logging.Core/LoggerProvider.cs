using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Blick.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Blick.Logging.Core;

public class LoggerProvider : ILoggerProvider
{
    private readonly ConcurrentDictionary<string, ILogger> loggers = new(StringComparer.OrdinalIgnoreCase);
    private readonly IDisposable? onChangeToken;
    private LoggerOptions options;
    private readonly IEnumerable<LoggingSink> sinks;

    public LoggerProvider(
        IOptionsMonitor<LoggerOptions> optionsMonitor,
        IEnumerable<LoggingSink> sinks)
    {
        this.sinks = sinks;
        options = optionsMonitor.CurrentValue;
        onChangeToken = optionsMonitor.OnChange(newValue => options = newValue);
    }

    public ILogger CreateLogger(string categoryName)
        => loggers.GetOrAdd(categoryName, name => new LoggingSinkStrategy(name, options, sinks));

    public void Dispose()
    {
        loggers.Clear();
        onChangeToken?.Dispose();
    }
}