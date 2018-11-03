using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EasyNews.ViewModels;
using Microsoft.Toolkit.Win32.UI.Controls.WPF;

namespace EasyNews.Views
{
    /// <summary>
    /// Interaction logic for RssScroller.xaml
    /// </summary>
    public partial class RssScroller : UserControl
    {

        private FeedViewModel feedViewModel;

        private RSSFeedViewModel rssFeedViewModel;
        private ArticleViewModel articleViewModel;

        private DoubleAnimation expandAnimation;
        private DoubleAnimation collapseAnimation;
        private Storyboard expandArticleListStoryboard;
        private Storyboard collapseArticleListStoryboard;
        private EventHandler CollapseAnimationStarted;

        public Action<string> ArticleSelected { private get; set; }

        public RssScroller()
        {
            InitializeComponent();
            feedViewModel = new FeedViewModel();
            rssFeedViewModel = new RSSFeedViewModel();
            articleViewModel = new ArticleViewModel();
        }

        public void InitAnimations(EventHandler completed, EventHandler started)
        {
            CollapseAnimationStarted = started;

            var halfSecondDuration = new Duration(TimeSpan.FromMilliseconds(500));
            expandAnimation = new DoubleAnimation(200, 400, halfSecondDuration);
            collapseAnimation = new DoubleAnimation(400, 200, halfSecondDuration);

            collapseAnimation.Completed += completed;
            expandAnimation.Completed += completed;

            expandArticleListStoryboard = new Storyboard();
            expandArticleListStoryboard.Children.Add(expandAnimation);
            collapseArticleListStoryboard = new Storyboard();
            collapseArticleListStoryboard.Children.Add(collapseAnimation);

            Storyboard.SetTargetName(expandAnimation, ArticleList.Name);
            Storyboard.SetTargetName(collapseAnimation, ArticleList.Name);
            Storyboard.SetTargetProperty(expandAnimation, new PropertyPath(FrameworkElement.WidthProperty));
            Storyboard.SetTargetProperty(collapseAnimation, new PropertyPath(FrameworkElement.WidthProperty));
        }

        private async void OnArticleListLoaded(object sender, RoutedEventArgs args)
        {
            feedViewModel.InitFeeds();
            await rssFeedViewModel.GetFeedData(feedViewModel.Feeds);
            articleViewModel.GetArticlesForFeed(rssFeedViewModel.Feeds.ToArray());

            ArticleList.DataContext = articleViewModel;
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
            CollapseAnimationStarted(sender,args);
            expandArticleListStoryboard.Begin(this);
        }

        private void OnArticleListHoverOut(object sender, RoutedEventArgs args)
        {
            Trace.WriteLine("HoverOut");
            CollapseAnimationStarted(sender,args);
            collapseArticleListStoryboard.Begin(this);

        }
    }
}
