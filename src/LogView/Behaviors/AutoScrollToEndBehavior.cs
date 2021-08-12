using Aldwych.Logging.ViewModels;
using Avalonia.Controls;
using Avalonia.Xaml.Interactivity;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;

namespace Aldwych.Logging.Behaviors
{
    public class AutoScrollToEndBehavior : Behavior<DataGrid>
    {
        IDisposable logSub;

        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject is DataGrid)
            {
                var dg = AssociatedObject as DataGrid;
                var pvm = dg.Parent.DataContext;

                if (pvm != null && pvm is LogViewViewModel vm)
                {
                    logSub = vm.WhenAnyValue(x => x.SelectedItem).Subscribe(s => dg.ScrollIntoView(s, null));
                }
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject is { })
            {
                logSub.Dispose();
                logSub = null;
            }
        }
    }
}
