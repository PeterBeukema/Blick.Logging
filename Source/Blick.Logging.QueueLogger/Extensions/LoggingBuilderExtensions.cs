using Blick.Logging.Abstractions;
using Blick.Queueing.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Blick.Logging.QueueLogger.Extensions;

public static class LoggingBuilderExtensions
{
    public static ILoggingBuilder WithQueueLogging(this ILoggingBuilder serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Services.AddMessageQueueing(configuration);
        serviceCollection.Services.AddSingleton<LoggingSink, QueueLoggingSink>();
        
        return serviceCollection;
    }
}