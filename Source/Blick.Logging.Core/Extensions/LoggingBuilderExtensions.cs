using System;
using Blick.Logging.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace Blick.Logging.Core.Extensions;

public static class LoggingBuilderExtensions
{
    public static ILoggingBuilder AddLogger(this ILoggingBuilder loggingBuilder, Action<LoggerOptions> configure)
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddConfiguration();
        
        loggingBuilder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, LoggerProvider>());
        
        LoggerProviderOptions.RegisterProviderOptions<LoggerOptions, LoggerProvider>(loggingBuilder.Services);

        loggingBuilder.Services.Configure(configure);

        return loggingBuilder;
    }
}