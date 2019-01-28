using System;
using System.Windows;

namespace EasyNews.Views
{
    /// <summary>
    /// Interaction logic for ArticleWebView.xaml
    /// </summary>
    public partial class ArticleWebView
    {
        /// <summary>
        /// Constructor, that initializes the component.
        /// </summary>
        public ArticleWebView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called after the WebView is loaded. Sets the DataContext of the WebView
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="args">Event args</param>
        private void OnWebViewLoaded(object sender, RoutedEventArgs args)
        {
            WebView.DataContext = DataContext;
        }

        /// <summary>
        /// Navigates the WebView to a given URI (usually a URL)
        /// </summary>
        /// <param name="uri">The URI to navigate to</param>
        public void Navigate(Uri uri)
        {
            WebView.Navigate(uri);
        }

        /// <summary>
        /// Fixes the WebViews width, so that it doesn't change during an animation
        /// </summary>
        public void FixWidth()
        {
            WebView.MaxWidth = WebView.ActualWidth;
            WebViewGrid.MaxWidth = WebView.ActualWidth;
        }

        /// <summary>
        /// Unfixes the width, so that the WebView can scale again.
        /// </summary>
        public void UnfixWidth()
        {
            WebView.MaxWidth = double.PositiveInfinity;
            WebViewGrid.MaxWidth = double.PositiveInfinity;
        }
    }
}
