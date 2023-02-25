using System;
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

    public QueueLoggingSink(
        IOptions<LoggerOptions> options,
        IMessagePublisher<QueueLoggingSink> messagePublisher)
        : base(options.Value)
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

        try
        {
            messagePublisher.Publish(logEvent, queue);
        }
        catch
        {
            // Explicitly do nothing - logging should never break the application
            // TODO: Properly handle exceptions during log event publishing
        }
    }
}