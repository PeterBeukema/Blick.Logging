using Blick.Logging.Abstractions;

namespace Blick.Logging.QueueLogger.Extensions;

public static class LoggerOptionsExtensions
{
    public static LoggerOptions UseQueueLogger(this LoggerOptions loggerOptions)
    {
        loggerOptions.BuildLoggers.Add(BuildQueueLogger);
        
        return loggerOptions;
    }

    private static Logger BuildQueueLogger(string categoryName, LoggerOptions options)
    {
        return new QueueLogger(categoryName, options);
    }
}