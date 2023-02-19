using System;
using Blick.Logging.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace Blick.Logging.Core.Extensions;

public static class LoggingBuilderExtensions
{
    public static ILoggingBuilder AddBlickLogging(this ILoggingBuilder loggingBuilder)
    {
        loggingBuilder.AddConfiguration();
        loggingBuilder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, LoggerProvider>());
        
        LoggerProviderOptions.RegisterProviderOptions<LoggerOptions, LoggerProvider>(loggingBuilder.Services);

        return loggingBuilder;
    }

    public static ILoggingBuilder AddBlickLogging(this ILoggingBuilder loggingBuilder, Action<LoggerOptions> configure)
    {
        loggingBuilder.AddBlickLogging();
        loggingBuilder.Services.Configure(configure);
        
        return loggingBuilder;
    }
}