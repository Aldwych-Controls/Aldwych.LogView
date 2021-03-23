using System;
using Microsoft.Extensions.Logging;

namespace Aldwych.Logging
{
    public class LogControlLoggerConfiguration
    {
        public int EventId { get; set; }
        public LogLevel LogLevel { get; set; } = LogLevel.Information;
        public ConsoleColor Color { get; set; } = ConsoleColor.Green;
    }
}
