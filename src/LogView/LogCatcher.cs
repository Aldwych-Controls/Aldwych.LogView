using System;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Threading;
using Aldwych.Logging.ViewModels;

namespace Aldwych.Logging
{
    internal static class LogCatcher
    {
        private readonly static ISubject<LogItemViewModel> LogItems = new Subject<LogItemViewModel>();
        private static long _counter;


        public static IObservable<LogItemViewModel> LogEntryObservable
        {
            get { return LogItems.AsObservable(); }
        }

        public static void Append(LogItemViewModel loggingEvent)
        {
            Interlocked.Increment(ref _counter);
            LogItems.OnNext(loggingEvent);
        }

    }
}
