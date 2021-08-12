using Aldwych.Logging;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
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

        public ICommand StartRunningCommand { get; private set; }


        bool running;
        public bool Running
        {
            get => running;
            set => this.RaiseAndSetIfChanged(ref running, value);
        }

        int selectedFilterIndex;
        public int SelectedFilterIndex
        {
            get => selectedFilterIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedFilterIndex, value);
                FilterLevel = (LogLevel)value;
            }
        }

        LogLevel filterLevel;
        public LogLevel FilterLevel
        {
            get => filterLevel;
            set => this.RaiseAndSetIfChanged(ref filterLevel, value);
        }

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
            StartRunningCommand = ReactiveCommand.Create(() => Running = true);

            var cmds = new List<ICommand>() { AddInfoCommand, AddTraceCommand, AddErrorCommand, AddCriticalCommand, AddWarningCommand };
            var cmdCount = cmds.Count;
            var rnd = new Random();
            Observable.Interval(TimeSpan.FromSeconds(0.5)).Subscribe(x =>
            {
                if (!Running)
                    return;

                var i = rnd.Next(0, cmdCount);
                Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() => cmds[i].Execute(null));
            });
        }

        
    }
}
