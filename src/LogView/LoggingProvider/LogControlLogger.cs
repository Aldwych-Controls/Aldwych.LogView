using System;
using Microsoft.Extensions.Logging;

namespace Aldwych.Logging
{
    using Aldwych.Logging.ViewModels;

    public class LogControlLogger : ILogger
    {
        private readonly string _name;
        private readonly LogControlLoggerConfiguration _config;

        public LogControlLogger(string name, LogControlLoggerConfiguration config) => (_name, _config) = (name, config);

        public IDisposable BeginScope<TState>(TState state)
        {
            return ScopeProvider?.Push(state) ?? default;
        }        

        internal IExternalScopeProvider ScopeProvider { get; set; }

        public bool IsEnabled(LogLevel logLevel)
        {
            if ((int)logLevel >= (int)_config.LogLevel)
                return true;
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (_config.EventId == 0 || _config.EventId == eventId.Id)
            {
                var safeName = _name.Replace("ViewModel", "");
                LogCatcher.Append(new LogItemViewModel(_name, logLevel, eventId, state, exception, formatter(state, exception), safeName));
            }
        }
    }

   
}
