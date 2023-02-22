using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Blick.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Blick.Logging.Core;

public class LoggerProvider : ILoggerProvider
{
    private readonly ConcurrentDictionary<string, BlickLogger> loggers = new(StringComparer.OrdinalIgnoreCase);
    private readonly IDisposable? onChangeToken;
    private LoggerOptions options;

    public LoggerProvider(IOptionsMonitor<LoggerOptions> optionsMonitor)
    {
        options = optionsMonitor.CurrentValue;
        onChangeToken = optionsMonitor.OnChange(newValue => options = newValue);
    }

    public ILogger CreateLogger(string categoryName)
        => loggers.GetOrAdd(categoryName, name => new LoggerStrategy(name, options));

    public void Dispose()
    {
        loggers.Clear();
        onChangeToken?.Dispose();
    }
}