using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Aldwych.Logging.ViewModels;

namespace Aldwych.Controls
{
    public class LogView : UserControl
    {

        public LogView()
        {
            InitializeComponent();
            DataContext = new LogViewViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
