using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Aldwych.Logging
{
    public class LogControlLoggerProvider : ILoggerProvider
    {
        private readonly LogControlLoggerConfiguration _config;
        private readonly ConcurrentDictionary<string, LogControlLogger> _loggers = new ConcurrentDictionary<string, LogControlLogger>();

        public LogControlLoggerProvider(LogControlLoggerConfiguration config) => _config = config;

        public ILogger CreateLogger(string categoryName) => _loggers.GetOrAdd(categoryName, name => new LogControlLogger(name, _config));

        public void Dispose() => _loggers.Clear();
    }
}
