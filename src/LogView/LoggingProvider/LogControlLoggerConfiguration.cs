using Microsoft.Extensions.Logging;

namespace Aldwych.Logging
{
    public class LogControlLoggerConfiguration
    {
        public int EventId { get; set; }

        public LogLevel LogLevel { get; set; } = LogLevel.Information;

        public bool TimestampIsVisible { get; set; } = true;

        public string TimestampFormat { get; set; } = "hh:mm:ss ";

    }

   
}
