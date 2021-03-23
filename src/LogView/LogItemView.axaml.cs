using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Aldwych.Controls
{
    public class LogItemView : UserControl
    {
        public LogItemView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
