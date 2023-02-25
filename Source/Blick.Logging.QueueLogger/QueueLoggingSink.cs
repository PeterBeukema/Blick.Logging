using System;
using System.Collections.Generic;
using System.Linq;
using Blick.Logging.Abstractions;
using Blick.Logging.QueueLogger.Models;
using Blick.Queueing.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Blick.Logging.QueueLogger;

public class QueueLoggingSink : LoggingSink
{
    private readonly IMessagePublisher<QueueLoggingSink> messagePublisher;
    private readonly IQueue queue = new Queue { Name = "Log" };
    private readonly List<LogEvent> logEvents = new();

    public QueueLoggingSink(
        IOptions<LoggerOptions> options,
        IMessagePublisher<QueueLoggingSink> messagePublisher)
        : base(options)
    {
        this.messagePublisher = messagePublisher;
    }

    public override void Log<TState>(
        string categoryName,
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        var message = formatter(state, exception);

        var logEvent = new LogEvent
        {
            LoggedAt = DateTime.UtcNow,
            Message = message,
            LogLevel = logLevel,
            EventIdentifier = eventId,
            Exception = exception?.Message,
            StackTrace = exception?.StackTrace,
        };
        
        logEvents.Add(logEvent);

        try
        {
            messagePublisher.Publish(logEvent, queue);
        }
        catch
        {
            // Explicitly do nothing - logging should never break the application
        }
    }
}