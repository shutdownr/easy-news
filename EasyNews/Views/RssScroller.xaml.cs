using System;
using System.Windows;
using System.Windows.Media.Animation;
using EasyNews.Models;
using EasyNews.ViewModels;

namespace EasyNews.Views
{
    /// <summary>
    /// Interaction logic for RssScroller.xaml
    /// </summary>
    public partial class RssScroller
    {
        /// <summary>
        /// The expandAnimation used by the Scroller
        /// </summary>
        private DoubleAnimation _expandAnimation;

        /// <summary>
        /// The collapseAnimation used by the Scroller
        /// </summary>
        private DoubleAnimation _collapseAnimation;

        /// <summary>
        /// The Storyboard for the expandAnimation
        /// </summary>
        private Storyboard _expandArticleListStoryboard;

        /// <summary>
        /// The Storyboard for the collapseAnimation
        /// </summary>
        private Storyboard _collapseArticleListStoryboard;

        /// <summary>
        /// The EventHandler, that is called when the collapseAnimation starts
        /// </summary>
        private EventHandler _collapseAnimationStarted;

        /// <summary>
        /// Action, that is executed when an Article is selected. Parent class sets this variable.
        /// </summary>
        public Action<EasyNewsFeedItem> ArticleSelected { private get; set; }

        /// <summary>
        /// Constructor
        /// Initializes the animations
        /// </summary>
        public RssScroller()
        {
            InitializeComponent();

            var halfSecondDuration = new Duration(TimeSpan.FromMilliseconds(500));
            _expandAnimation = new DoubleAnimation(200, 400, halfSecondDuration);
            _collapseAnimation = new DoubleAnimation(400, 200, halfSecondDuration);
        }

        /// <summary>
        /// Sets the event handlers for started/completed of the animations
        /// Also initializes the animations and connects them with their Storyboards
        /// </summary>
        /// <param name="completed">The EventHandler to be called when the animations are completed</param>
        /// <param name="started">The EventHandler to be called when the animations are started</param>
        public void InitAnimations(EventHandler completed, EventHandler started)
        {
            _collapseAnimationStarted = started;

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

        /// <summary>
        /// Called when ArticleList is loaded.
        /// Sets the DataContext of the ScrollViewer.
        /// </summary>
        /// <param name="sender">EventSender</param>
        /// <param name="args">EventArgs</param>
        private void OnArticleListLoaded(object sender, RoutedEventArgs args)
        {
            var context = DataContext as ArticleViewModel;
            ScrollViewer.DataContext = context;
        }

        /// <summary>
        /// Called when an article is selected. Calls the EventHandler ArticleSelected with the selected FeedItem
        /// </summary>
        /// <param name="sender">EventSender (the Button that was clicked)</param>
        /// <param name="args">EventArgs</param>
        private void OnArticleSelected(object sender, RoutedEventArgs args)
        {
            var b = (FrameworkElement)sender;
            var item = (EasyNewsFeedItem) b.DataContext;
            
            ArticleSelected(item);
        }

        /// <summary>
        /// Called when the user hovers over the Scroller.
        /// Starts the expandAnimation.
        /// </summary>
        /// <param name="sender">EventSender</param>
        /// <param name="args">EventArgs</param>
        private void OnArticleListHover(object sender, RoutedEventArgs args)
        {
            if (_collapseAnimationStarted == null)
            {
                return;
            }
            _collapseAnimationStarted(sender,args);
            _expandArticleListStoryboard.Begin(this);
        }

        /// <summary>
        /// Called when the user hovers out of the Scroller.
        /// Starts the collapseAnimation.
        /// </summary>
        /// <param name="sender">EventSender</param>
        /// <param name="args">EventArgs</param>
        private void OnArticleListHoverOut(object sender, RoutedEventArgs args)
        {
            if (_collapseAnimationStarted == null)
            {
                return;
            }
            _collapseAnimationStarted(sender,args);
            _collapseArticleListStoryboard.Begin(this);
        }
    }
}
