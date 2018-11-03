using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using EasyNews.Helpers;
using EasyNews.ViewModels;
using Button = System.Windows.Controls.Button;


namespace EasyNews.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Tim, 15.10.2018. </remarks>

        private FeedViewModel feedViewModel;

        private RSSFeedViewModel rssFeedViewModel;
        private ArticleViewModel articleViewModel;

        private DoubleAnimation expandAnimation;
        private DoubleAnimation collapseAnimation;
        private Storyboard expandArticleListStoryboard;
        private Storyboard collapseArticleListStoryboard;

        public MainWindow()
        {
            InitializeComponent();
            feedViewModel = new FeedViewModel();
            rssFeedViewModel = new RSSFeedViewModel();
            articleViewModel = new ArticleViewModel();

            RssScroller.InitAnimations(CollapseAnimation_Started, CollapseAnimation_Completed);
            RssScroller.ArticleSelected = ArticleSelected;
        }

        private void CollapseAnimation_Completed(object sender, EventArgs e)
        {
            WebView.MaxWidth = Double.PositiveInfinity;
            WebViewGrid.MaxWidth = Double.PositiveInfinity;
        }

        private void CollapseAnimation_Started(object sender, EventArgs e)
        {
            WebView.MaxWidth = WebView.ActualWidth;
            WebViewGrid.MaxWidth = WebView.ActualWidth;
        }

        private void ArticleSelected(string link)
        {
            WebView.Navigate(new Uri(link));
            WebViewGrid.Visibility = Visibility.Visible;
            RssEditGrid.Visibility = Visibility.Collapsed;
        }

        private void OnWebViewLoaded(object sender, RoutedEventArgs args)
        {
            WebView.DataContext = articleViewModel;
        }


        private void OnAddFeedClicked(object sender, RoutedEventArgs args)
        {
            WebViewGrid.Visibility = Visibility.Collapsed;
            RssEditGrid.Visibility = Visibility.Visible;

            Trace.WriteLine("Feed added");
        }

        private void OnRemoveFeedClicked(object sender, RoutedEventArgs args)
        {
            Trace.WriteLine("Feed removed");
            WebViewGrid.Visibility = Visibility.Collapsed;
            RssEditGrid.Visibility = Visibility.Visible;
        }
    }


}
