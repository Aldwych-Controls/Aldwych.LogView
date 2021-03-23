using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Microsoft.Extensions.Logging;
using ReactiveUI;

namespace Aldwych.Logging.ViewModels
{
    public class LogItemViewModel : ReactiveObject
    {

        public LogLevel LogLevel
        {
            get => _logLevel;
            set => this.RaiseAndSetIfChanged(ref _logLevel, value);
        }


        public EventId EventId
        {
            get => _eventId;
            set => this.RaiseAndSetIfChanged(ref _eventId, value);
        }


        public Exception Exception
        {
            get => _exception;
            set => this.RaiseAndSetIfChanged(ref _exception, value);
        }


        public object State
        {
            get => _state;
            set => this.RaiseAndSetIfChanged(ref _state, value);
        }


        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }


        public DateTime Created
        {
            get => _created;
            set => this.RaiseAndSetIfChanged(ref _created, value);
        }

        public StreamGeometry IconPath
        {
            get => _iconPath;
            set => this.RaiseAndSetIfChanged(ref _iconPath, value);
        }


        public IBrush Foreground
        {
            get => _foreground;
            set => this.RaiseAndSetIfChanged(ref _foreground, value);
        }

        public LogItemViewModel(string name, LogLevel logLevel, EventId eventId, object state, Exception exception, string message) 
        {
            LogLevel = logLevel;
            EventId = eventId;
            State = state;
            Exception = exception;
            Message = message;
            Created = DateTime.Now;
            _iconPath = IconPathForLogLevel(LogLevel);
            Foreground = BrushForLogLevel(LogLevel);
            _assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
        }



        private StreamGeometry IconPathForLogLevel(LogLevel ll)
        {
            StreamGeometry iconResource;

            switch (ll)
            {               
                case LogLevel.Error:
                    iconResource = (StreamGeometry)Application.Current.FindResource("ErrorLogMessageGeometry");
                    break;
                case LogLevel.Debug:
                    iconResource = (StreamGeometry)Application.Current.FindResource("DebugLogMessageGeometry");
                    break;
                case LogLevel.Trace:
                    iconResource = (StreamGeometry)Application.Current.FindResource("TraceLogMessageGeometry");
                    break;
                case LogLevel.Critical:
                    iconResource = (StreamGeometry)Application.Current.FindResource("CriticalLogMessageGeometry");
                    break;
                case LogLevel.Warning:
                    iconResource = (StreamGeometry)Application.Current.FindResource("WarningLogMessageGeometry");
                    break;
                default:
                    iconResource = (StreamGeometry)Application.Current.FindResource("InfoLogMessageGeometry");
                    break;
            }
           
            return iconResource;
        }

        private IBrush BrushForLogLevel(LogLevel ll)
        {
            switch (ll)
            {
                case LogLevel.Warning:
                    return new SolidColorBrush(Color.FromRgb(255, 157, 66), 1);
                case LogLevel.Trace:
                    return new SolidColorBrush(Color.FromRgb(94, 200, 255), 1);
                case LogLevel.Information:
                    return new SolidColorBrush(Color.FromRgb(81, 202, 51), 1);
                case LogLevel.Error:
                    return new SolidColorBrush(Color.FromRgb(255, 96, 87), 1);
                case LogLevel.Critical:
                    return new SolidColorBrush(Color.FromRgb(229, 229, 229), 1);
                default:
                    return new SolidColorBrush(Color.FromRgb(44, 201, 182), 1);
            }
        }


        private readonly IAssetLoader _assets;
        private IBrush _foreground;
        private StreamGeometry _iconPath;
        private DateTime _created;
        private string _message;
        private object _state;
        private Exception _exception;
        private EventId _eventId;
        private LogLevel _logLevel;

    }


}
