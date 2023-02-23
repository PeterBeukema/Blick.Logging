using Blick.Logging.Abstractions;

namespace Blick.Logging.ConsoleLogger.Extensions;

public static class LoggerOptionsExtensions
{
    public static LoggerOptions UseConsoleLogger(this LoggerOptions loggerOptions)
    {
        loggerOptions.BuildLoggers.Add(BuildConsoleLogger);

        return loggerOptions;
    }

    private static Logger BuildConsoleLogger(string categoryName, LoggerOptions loggerOptions)
        => new ConsoleLogger(categoryName, loggerOptions);
}