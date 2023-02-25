using System;
using Microsoft.Extensions.Logging;

namespace Blick.Logging.QueueLogger.Models;

public class LogEvent
{
    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;
    public string Message { get; set; } = string.Empty;
    public LogLevel LogLevel { get; set; }
    public string? Exception { get; set; }
    public string? StackTrace { get; set; }
    public EventId EventIdentifier { get; set; }
}