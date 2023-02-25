using System;
using Blick.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Blick.Logging.ConsoleLogger;

public class ConsoleLoggingSink : LoggingSink
{
    public ConsoleLoggingSink(IOptions<LoggerOptions> options)
        : base(options.Value) { }

    public override void Log<TState>(
        string categoryName,
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var now = DateTime.UtcNow;

        var originalForegroundColor = Console.ForegroundColor;
        var originalBackgroundColor = Console.BackgroundColor;

        var logEventForegroundColor = ConsoleColors.ForegroundColors[logLevel];
        var logEventBackgroundColor = ConsoleColors.BackgroundColors[logLevel];

        Console.ForegroundColor = logEventForegroundColor;
        Console.BackgroundColor = logEventBackgroundColor;

        var message = formatter(state, exception);

        message = message.Replace(Environment.NewLine, Environment.NewLine.PadRight(10));
        message = $"[{logLevel}] {now:yyyy-MM-dd HH:mm:ss} {categoryName} - {message}";
        
        Console.WriteLine(message);

        if (exception != null)
        {
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        Console.ForegroundColor = originalForegroundColor;
        Console.BackgroundColor = originalBackgroundColor;
    }
}