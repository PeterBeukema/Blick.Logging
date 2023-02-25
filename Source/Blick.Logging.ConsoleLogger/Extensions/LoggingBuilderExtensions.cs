using Blick.Logging.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Blick.Logging.ConsoleLogger.Extensions;

public static class LoggingBuilderExtensions
{
    public static ILoggingBuilder WithConsoleLogging(this ILoggingBuilder serviceCollection)
    {
        serviceCollection.Services.AddSingleton<LoggingSink, ConsoleLoggingSink>();

        return serviceCollection;
    }
}