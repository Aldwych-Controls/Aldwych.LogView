using Aldwych.Logging;
using Aldwych.Logging.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using DynamicData;
using DynamicData.Binding;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;

namespace Aldwych.Controls
{
    public enum LogLevelType
    {
        DisplayAsText,
        DisplayAsDot,
    }

    public class LogView : TemplatedControl
    {
        public static readonly StyledProperty<LogLevel> LogLevelFilterProperty = AvaloniaProperty.Register<LogView, LogLevel>(nameof(LogLevelFilter));

        public static readonly StyledProperty<string> TimespanFormatProperty = AvaloniaProperty.Register<LogView, string>(nameof(TimespanFormat), "HH:mm:ss");

        public static readonly StyledProperty<object> SelectedItemProperty = AvaloniaProperty.Register<LogView, object>(nameof(SelectedItem));

        public static readonly StyledProperty<ReadOnlyObservableCollection<LogItemViewModel>> LogItemsProperty = AvaloniaProperty.Register<LogView, ReadOnlyObservableCollection<LogItemViewModel>>(nameof(LogItems));

   

        public static readonly StyledProperty<LogLevelType> LogLevelCellTypeProperty = AvaloniaProperty.Register<LogView, LogLevelType>(nameof(LogLevelCellType));

        public LogLevelType LogLevelCellType
        {
            get { return GetValue(LogLevelCellTypeProperty); }
            set { SetValue(LogLevelCellTypeProperty, value); }
        }

        public static readonly StyledProperty<bool> AutoScrollEnabledProperty = AvaloniaProperty.Register<LogView, bool>(nameof(AutoScrollEnabled), true);

        public bool AutoScrollEnabled
        {
            get { return GetValue(AutoScrollEnabledProperty); }
            set { SetValue(AutoScrollEnabledProperty, value); }
        }

        public ReadOnlyObservableCollection<LogItemViewModel> LogItems
        {
            get { return GetValue(LogItemsProperty); }
            set { SetValue(LogItemsProperty, value); }
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public event PropertyChangingEventHandler PropertyChanging;


        public string TimespanFormat
        {
            get { return GetValue(TimespanFormatProperty); }
            set { SetValue(TimespanFormatProperty, value); }
        }

        public LogLevel LogLevelFilter
        {
            get { return GetValue(LogLevelFilterProperty); }
            set { SetValue(LogLevelFilterProperty, value); }
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            _dataGrid = e.NameScope.Find<DataGrid>("PART_DataGrid");
        }
     

        protected override Size MeasureOverride(Size availableSize)
        {
            ScrollToEnd();
            return base.MeasureOverride(availableSize);
        }

        public LogView()
        {
            var sl = LogCatcher.LogEntryObservable.ToObservableChangeSet(limitSizeTo: 2000).AsObservableList();
            var dynamicFilter = this.WhenAnyValue(x => x.LogLevelFilter).Select(x => CreatePredicate(x));
            var loader = sl.Connect().DisposeMany().Filter(dynamicFilter).Sort(SortExpressionComparer<LogItemViewModel>.Ascending(i => i.Created)).Bind(out var logItems).Subscribe();
            LogItems = logItems;
            LogCatcher.LogEntryObservable.Subscribe(x =>
            {
                SelectedItem = LogItems.LastOrDefault();
                ScrollToEnd();
            });
        }

        public void ScrollToEnd()
        {
            try
            {
                if (AutoScrollEnabled && _dataGrid != null)
                    _dataGrid.ScrollIntoView(SelectedItem, null);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private Func<LogItemViewModel, bool> CreatePredicate(LogLevel logLevel)
        {
            var index = (int)logLevel;
            var inverted = ReverseNumber(index, 0, 5);

            return logItemVM => (int)logItemVM.LogLevel <= inverted;

            int ReverseNumber(int num, int min, int max)
            {
                return (max + min) - num;
            }
        }

        private DataGrid _dataGrid;
    }
}
