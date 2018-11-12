using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using EasyNews.Models;
using EasyNews.ViewModels;

namespace EasyNews.Views
{
    /// <summary>
    /// Interaction logic for RssScroller.xaml
    /// </summary>
    public partial class RssScroller : UserControl
    {

        public FeedViewModel FeedViewModel;

        public RssFeedViewModel RssFeedViewModel;
        public ArticleViewModel ArticleViewModel;

        private DoubleAnimation _expandAnimation;
        private DoubleAnimation _collapseAnimation;
        private Storyboard _expandArticleListStoryboard;
        private Storyboard _collapseArticleListStoryboard;
        private EventHandler _collapseAnimationStarted;

        public Action<string> ArticleSelected { private get; set; }

        public RssScroller()
        {
            InitializeComponent();
        }

        public void InitAnimations(EventHandler completed, EventHandler started)
        {
            _collapseAnimationStarted = started;

            var halfSecondDuration = new Duration(TimeSpan.FromMilliseconds(500));
            _expandAnimation = new DoubleAnimation(200, 400, halfSecondDuration);
            _collapseAnimation = new DoubleAnimation(400, 200, halfSecondDuration);

            _collapseAnimation.Completed += completed;
            _expandAnimation.Completed += completed;

            _expandArticleListStoryboard = new Storyboard();
            _expandArticleListStoryboard.Children.Add(_expandAnimation);
            _collapseArticleListStoryboard = new Storyboard();
            _collapseArticleListStoryboard.Children.Add(_collapseAnimation);

            Storyboard.SetTargetName(_expandAnimation, ArticleList.Name);
            Storyboard.SetTargetName(_collapseAnimation, ArticleList.Name);
            Storyboard.SetTargetProperty(_expandAnimation, new PropertyPath(WidthProperty));
            Storyboard.SetTargetProperty(_collapseAnimation, new PropertyPath(WidthProperty));
        }

        private void OnArticleListLoaded(object sender, RoutedEventArgs args)
        {
            ScrollViewer.DataContext = ArticleViewModel;
            ReloadFeeds();
        }

        public async void ReloadFeeds()
        {
            if (FeedViewModel != null && RssFeedViewModel != null && ArticleViewModel != null)
            {
                FeedViewModel.InitFeeds();
                await RssFeedViewModel.GetFeedData(FeedViewModel.Feeds);
                ArticleViewModel.GetArticlesForFeed(RssFeedViewModel.Feeds.ToArray());
            }
            
        }

        public async void AddFeed(Feed feed)
        {
            await RssFeedViewModel.AddFeedData(feed);
            // We just added a new feed, this one is certainly the last one
            ArticleViewModel.AddArticlesForFeed(RssFeedViewModel.Feeds.Last());
        }

        public void RemoveFeed(CodeHollow.FeedReader.Feed feed)
        {
            // Remove the feed from the RssFeedViewModel
            RssFeedViewModel.RemoveFeedData(feed);
            // Also remove from ArticleViewModel, to update the View
            ArticleViewModel.RemoveArticlesForFeed(feed);
        }


        private void OnArticleSelected(object sender, RoutedEventArgs args)
        {
            var b = (FrameworkElement)sender;
            var link = (string)b.Tag;
            ArticleSelected(link);
        }

        private void OnArticleListHover(object sender, RoutedEventArgs args)
        {
            Trace.WriteLine("HoverIn");
            _collapseAnimationStarted(sender,args);
            _expandArticleListStoryboard.Begin(this);
        }

        private void OnArticleListHoverOut(object sender, RoutedEventArgs args)
        {
            Trace.WriteLine("HoverOut");
            _collapseAnimationStarted(sender,args);
            _collapseArticleListStoryboard.Begin(this);
        }
    }
}
