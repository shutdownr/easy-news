using System;
using System.Diagnostics;
using System.Windows;
using EasyNews.ViewModels;



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

        private FeedViewModel feedViewModel = new FeedViewModel();

        private RssFeedViewModel rssFeedViewModel = new RssFeedViewModel();
        private ArticleViewModel articleViewModel = new ArticleViewModel();
        private FavoritesViewModel favoritesViewModel = new FavoritesViewModel();

        public MainWindow()
        {
            InitializeComponent();

        }

        public void OnWindowLoaded(object sender, RoutedEventArgs args)
        {
            HomeView.ArticleSelected = ArticleSelected;
            HomeView.ArticleViewModel = articleViewModel;
            HomeView.FavoritesViewModel = favoritesViewModel;

            RssScroller.InitAnimations(CollapseAnimation_Completed, CollapseAnimation_Started);
            RssScroller.ArticleSelected = ArticleSelected;
            RssScroller.FeedViewModel = feedViewModel;
            RssScroller.RssFeedViewModel = rssFeedViewModel;
            RssScroller.ArticleViewModel = articleViewModel;

            ArticleWebView.ArticleViewModel = articleViewModel;

            RssEditGrid.RssScroller = RssScroller;
            RssEditGrid.RssFeedViewModel = rssFeedViewModel;
            RssEditGrid.FeedViewModel = feedViewModel;
        }

        private void CollapseAnimation_Completed(object sender, EventArgs e)
        {
            Trace.WriteLine("Unfixing Width...");
            ArticleWebView.UnfixWidth();
        }

        private void CollapseAnimation_Started(object sender, EventArgs e)
        {
            Trace.WriteLine("Fixing Width...");
            ArticleWebView.FixWidth();
        }

        private void ArticleSelected(string link)
        {
            ArticleWebView.Navigate(new Uri(link));
            ArticleWebView.Visibility = Visibility.Visible;
            RssEditGrid.Visibility = Visibility.Collapsed;
            RssScroller.Visibility = Visibility.Visible;
            HomeView.Visibility = Visibility.Collapsed;
        }

        private void OnHomeClicked(object sender, RoutedEventArgs args)
        {
            RssEditGrid.SetMode(RssEditMode.Add);
            ArticleWebView.Visibility = Visibility.Collapsed;
            RssEditGrid.Visibility = Visibility.Collapsed;
            RssScroller.Visibility = Visibility.Collapsed;
            HomeView.Visibility = Visibility.Visible;
        }

        private void OnAddFeedClicked(object sender, RoutedEventArgs args)
        {
            RssEditGrid.SetMode(RssEditMode.Add);
            ArticleWebView.Visibility = Visibility.Collapsed;
            RssEditGrid.Visibility = Visibility.Visible;
            RssScroller.Visibility = Visibility.Visible;
            HomeView.Visibility = Visibility.Collapsed;
        }

        private void OnRemoveFeedClicked(object sender, RoutedEventArgs args)
        {
            RssEditGrid.SetMode(RssEditMode.Remove);
            ArticleWebView.Visibility = Visibility.Collapsed;
            RssEditGrid.Visibility = Visibility.Visible;
            RssScroller.Visibility = Visibility.Visible;
            HomeView.Visibility = Visibility.Collapsed;
        }

        private void HomeView_Loaded()
        {

        }
    }


}
