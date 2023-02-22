using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Blick.Logging.Abstractions;

public class LoggerOptions
{
    public LogLevel MinimumLogLevel { get; set; } = LogLevel.Trace;

    public List<Func<string, LoggerOptions, BlickLogger>> BuildLoggers { get; set; } = new();
}