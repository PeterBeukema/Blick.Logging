using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Blick.Logging.ConsoleLogger;

public static class ConsoleColors
{
    public static readonly Dictionary<LogLevel, ConsoleColor> ForegroundColors = new()
    {
        { LogLevel.Trace, ConsoleColor.Cyan },
        { LogLevel.Debug, ConsoleColor.White },
        { LogLevel.Information, ConsoleColor.Blue },
        { LogLevel.Warning, ConsoleColor.Yellow },
        { LogLevel.Error, ConsoleColor.Red },
        { LogLevel.Critical, ConsoleColor.Red },
        { LogLevel.None, ConsoleColor.White },
    };
    
    public static readonly Dictionary<LogLevel, ConsoleColor> BackgroundColors = new()
    {
        { LogLevel.Trace, ConsoleColor.Black },
        { LogLevel.Debug, ConsoleColor.Black },
        { LogLevel.Information, ConsoleColor.Black },
        { LogLevel.Warning, ConsoleColor.Black },
        { LogLevel.Error, ConsoleColor.Black },
        { LogLevel.Critical, ConsoleColor.DarkRed },
        { LogLevel.None, ConsoleColor.Black },
    };
}