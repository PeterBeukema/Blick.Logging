using System;
using System.Collections.Generic;
using Blick.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Blick.Logging.Core;

public class LoggerStrategy : LoggerBase
{
    public LoggerStrategy(string categoryName, LoggerOptions options) : base(categoryName, options) { }

    public override void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        var loggers = Options.BuildLoggers?.Invoke(CategoryName, Options);

        if (loggers == null)
        {
            return;
        }
        
        foreach (var logger in loggers)
        {
            logger.Log(logLevel, eventId, state, exception, formatter);
        }
    }
}