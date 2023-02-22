using System;
using System.Collections.Generic;
using System.Linq;
using Blick.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Blick.Logging.Core;

public class LoggerStrategy : BlickLogger
{
    private readonly List<BlickLogger> loggers;

    public LoggerStrategy(string categoryName, LoggerOptions options)
        : base(categoryName, options)
    {
        loggers = options.BuildLoggers
            .Select(buildLogger => buildLogger(categoryName, options))
            .ToList();
    }

    public override void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!loggers.Any())
        {
            return;
        }
        
        foreach (var logger in loggers)
        {
            logger.Log(logLevel, eventId, state, exception, formatter);
        }
    }
}