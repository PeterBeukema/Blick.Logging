using System;
using Blick.Logging.Abstractions;
using Microsoft.Extensions.Logging;

namespace Blick.Logging.QueueLogger;

public class QueueLogger : Logger
{
    public QueueLogger(string categoryName, LoggerOptions options)
        : base(categoryName, options) { }

    public override void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        var now = DateTime.UtcNow;
        var message = formatter(state, exception);

        message = message.Replace(Environment.NewLine, Environment.NewLine.PadRight(10));
        message = $"[{logLevel}] {now:yyyy-MM-dd HH:mm:ss} {CategoryName} - {message}";
        
        Console.WriteLine("*** QUEUE LOGGED: " + message);
    }
}