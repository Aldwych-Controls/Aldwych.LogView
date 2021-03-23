using System;
using Microsoft.Extensions.Logging;

namespace Aldwych.Logging
{
    public static class LogControlLoggerExtensions
    {
        public static ILoggingBuilder AddColorConsoleLogger(this ILoggingBuilder builder) => builder.AddColorConsoleLogger(new LogControlLoggerConfiguration());

        public static ILoggingBuilder AddColorConsoleLogger(this ILoggingBuilder builder, Action<LogControlLoggerConfiguration> configure)
        {
            var config = new LogControlLoggerConfiguration();
            configure(config);
            return builder.AddColorConsoleLogger(config);
        }

        public static ILoggingBuilder AddColorConsoleLogger(this ILoggingBuilder builder, LogControlLoggerConfiguration config)
        {
            builder.AddProvider(new LogControlLoggerProvider(config));
            return builder;
        }
    }
}
