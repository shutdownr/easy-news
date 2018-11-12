using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using EasyNews.ViewModels;

namespace EasyNews.Views
{
    /// <summary>
    /// Interaction logic for ArticleWebView.xaml
    /// </summary>
    public partial class ArticleWebView : UserControl
    {
        public ArticleViewModel ArticleViewModel;

        public ArticleWebView()
        {
            InitializeComponent();
            //ArticleViewModel = new ArticleViewModel();
        }
        private void OnWebViewLoaded(object sender, RoutedEventArgs args)
        {
            WebView.DataContext = ArticleViewModel;
        }

        public void Navigate(Uri uri)
        {
            WebView.Navigate(uri);
        }

        public void FixWidth()
        {
            WebView.MaxWidth = WebView.ActualWidth;
            WebViewGrid.MaxWidth = WebView.ActualWidth;
            Trace.WriteLine("Fixed Width!");
        }

        public void UnfixWidth()
        {
            WebView.MaxWidth = double.PositiveInfinity;
            WebViewGrid.MaxWidth = double.PositiveInfinity;
            Trace.WriteLine("Unfixed Width!");
        }
    }
}
