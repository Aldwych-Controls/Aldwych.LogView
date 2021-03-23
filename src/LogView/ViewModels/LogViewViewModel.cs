using Avalonia.Controls;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace Aldwych.Logging.ViewModels
{
    internal class LogViewViewModel : ReactiveObject
    {
        //public ObservableCollection<LogItemViewModel> LogItems { get; private set; }


        public ReadOnlyObservableCollection<LogItemViewModel> LogItems { get; }

        int selctedIndex;
        public int SelectedIndex
        {
            get => selctedIndex;
            set => this.RaiseAndSetIfChanged(ref selctedIndex, value);
        }

        private bool showLogLevel;
        public bool ShowLogLevel
        {
            get => showLogLevel;
            set => this.RaiseAndSetIfChanged(ref showLogLevel, value);
        }

        int filterSelectedIndex;
        public int FilterSelectedIndex
        {
            get => filterSelectedIndex;
            set => this.RaiseAndSetIfChanged(ref filterSelectedIndex, value);
        }


        private Func<LogItemViewModel, bool> CreatePredicate(int selectedIndex)
        {
            var inverted = ReverseNumber(selectedIndex, 0, 5);
            return logItemVM => (int)logItemVM.LogLevel <= inverted;
        }

        public int ReverseNumber(int num, int min, int max)
        {
            return (max + min) - num;
        }


        public LogViewViewModel()
        {

            var sl = LogCatcher.LogEntryObservable.ToObservableChangeSet(limitSizeTo: 2000).AsObservableList();
            var dynamicFilter = this.WhenAnyValue(x => x.FilterSelectedIndex).Select(x => CreatePredicate(x));



            var loader = sl.Connect().DisposeMany().Filter(dynamicFilter).Sort(SortExpressionComparer<LogItemViewModel>.Ascending(i => i.Created)).Bind(out var logItems).Subscribe();
            LogItems = logItems;
            LogCatcher.LogEntryObservable.Subscribe(x => SelectedIndex = LogItems.Count -1);

            var updateSelectedIndex = this.WhenAnyValue(x => x.FilterSelectedIndex).Do(_ => UpdateSelectedItem()).Subscribe();


            LogCatcher.Append(new LogItemViewModel("test", Microsoft.Extensions.Logging.LogLevel.None, new Microsoft.Extensions.Logging.EventId(5052, "omg"), this, null, "this is a test"));
            LogCatcher.Append(new LogItemViewModel("test", Microsoft.Extensions.Logging.LogLevel.Warning, new Microsoft.Extensions.Logging.EventId(5052, "omg"), this, null, "this is a test"));
            LogCatcher.Append(new LogItemViewModel("test", Microsoft.Extensions.Logging.LogLevel.Trace, new Microsoft.Extensions.Logging.EventId(5052, "omg"), this, null, "this is a test"));
            LogCatcher.Append(new LogItemViewModel("test", Microsoft.Extensions.Logging.LogLevel.Information, new Microsoft.Extensions.Logging.EventId(5052, "omg"), this, null, "this is a test"));
            LogCatcher.Append(new LogItemViewModel("test", Microsoft.Extensions.Logging.LogLevel.Error, new Microsoft.Extensions.Logging.EventId(5052, "omg"), this, null, "this is a test"));
            LogCatcher.Append(new LogItemViewModel("test", Microsoft.Extensions.Logging.LogLevel.Critical, new Microsoft.Extensions.Logging.EventId(5052, "omg"), this, null, "this is a test"));

            ShowLogLevel = true;



        }

        void UpdateSelectedItem()
        {
           SelectedIndex = LogItems.Count - 1;
        }
    }
}
