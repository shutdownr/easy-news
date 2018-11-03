using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EasyNews.Views
{
    /// <summary>
    /// Interaction logic for RssEditGrid.xaml
    /// </summary>
    public partial class RssEditGrid : UserControl
    {
        public RssEditGrid()
        {
            InitializeComponent();
        }

        private void OnRssTextBoxFocused(object sender, RoutedEventArgs args)
        {
            HintText.Visibility = Visibility.Collapsed;
        }
        private void OnRssTextBoxFocusLost(object sender, RoutedEventArgs args)
        {
            if (UrlInputTextBox.Text == "")
            {
                HintText.Visibility = Visibility.Visible;
            }
            else
            {
                HintText.Visibility = Visibility.Collapsed;
            }
        }

        private void OnGridClicked(object sender, RoutedEventArgs args)
        {
            Keyboard.ClearFocus();
            Trace.WriteLine("Grid got focus!");
        }
    }
}
