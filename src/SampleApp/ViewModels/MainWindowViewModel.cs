using Aldwych.Logging;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using System.Threading;
using System.Windows.Input;

namespace SampleApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ILogger logger;

        public ICommand AddInfoCommand { get; private set; }

        public ICommand AddTraceCommand { get; private set; }

        public ICommand AddErrorCommand { get; private set; }

        public ICommand AddCriticalCommand { get; private set; }

        public ICommand AddNoneCommand { get; private set; }

        public ICommand AddWarningCommand { get; private set; }


        public MainWindowViewModel()
        {
            var provider = new LogControlLoggerProvider(new LogControlLoggerConfiguration() { LogLevel = LogLevel.Trace });
            logger = provider.CreateLogger("App");

            AddInfoCommand = ReactiveCommand.Create(() => logger.LogInformation("info test"));
            AddTraceCommand = ReactiveCommand.Create(() => logger.LogTrace("trace test", new object[] { Thread.CurrentThread.Name }));
            AddErrorCommand = ReactiveCommand.Create(() => logger.LogError("error test", new object[] { Thread.CurrentThread.Name }));
            AddCriticalCommand = ReactiveCommand.Create(() => logger.LogCritical("error test", new object[] { Thread.CurrentThread.Name }));
            AddNoneCommand = ReactiveCommand.Create(() => logger.Log(LogLevel.Debug, "error test", new object[] { Thread.CurrentThread.Name }));
            AddWarningCommand = ReactiveCommand.Create(() => logger.LogWarning("error test", new object[] { Thread.CurrentThread.Name }));
        }
    }
}
