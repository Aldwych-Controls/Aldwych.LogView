using System;
using Microsoft.Extensions.Logging;

namespace Aldwych.Logging
{
    public static class LogControlLoggerExtensions
    {
        public static ILoggingBuilder AddLogger(this ILoggingBuilder builder) => builder.AddLogger(new LogControlLoggerConfiguration());

        public static ILoggingBuilder AddLogger(this ILoggingBuilder builder, Action<LogControlLoggerConfiguration> configure)
        {
            var config = new LogControlLoggerConfiguration();
            configure(config);

            return builder.AddLogger(config);
        }

        public static ILoggingBuilder AddLogger(this ILoggingBuilder builder, LogControlLoggerConfiguration config)
        {
            builder.AddProvider(new LogControlLoggerProvider(config));
            return builder;
        }
    }
}
