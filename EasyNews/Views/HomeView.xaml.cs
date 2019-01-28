using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using EasyNews.Helpers;
using EasyNews.Models;
using EasyNews.Utility;
using EasyNews.ViewModels;

namespace EasyNews.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView
    {
        /// <summary>
        /// Constructor, initializes the component and the DataContext based on ViewModelHolder
        /// Adds a BoolToColorConverter to the Resources.
        /// Adds OnWindowResize to the functions that are called, when the window size changes.
        /// </summary>
        public HomeView()
        {
            DataContext = ViewModelHolder.Instance.CurrentViewModel;

            var boolToColorConverter = new BoolToColorConverter("SecondaryAccentBrush");
            boolToColorConverter.ContextElement = this;
            Resources.Add("ColorConverter", boolToColorConverter);
            InitializeComponent();

            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.SizeChanged += OnWindowResize;
            }
        }

        /// <summary>
        /// Called when the user scrolls. Shows a scrollbar and starts a timer that hides it again after scrolling.
        /// </summary>
        /// <param name="sender">EventSender</param>
        /// <param name="args">EventArgs</param>
        private void OnScrolled(object sender, RoutedEventArgs args)
        {
            if (scrollTimer == null)
            {
                scrollTimer = new DispatcherTimer();
                scrollTimer.Tick += new EventHandler(ScrollTimerFinished);
                scrollTimer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            }
            else if (scrollTimer != null)
            {
                scrollTimer.Stop();
            }

            ScrollbarPlaceholder.Visibility = Visibility.Collapsed;
            ScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollTimer.Start();
        }

        /// <summary>
        /// DispatcherTimer used for hiding the scrollbar after scrolling
        /// </summary>
        private DispatcherTimer scrollTimer;

        /// <summary>
        /// Called when the timer finishes, hides the scrollbar.
        /// </summary>
        /// <param name="sender">EventSender</param>
        /// <param name="e">EventArgs</param>
        private void ScrollTimerFinished(object sender, EventArgs e)
        {
            ScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            ScrollbarPlaceholder.Visibility = Visibility.Visible;
            scrollTimer.Stop();
        }

        /// <summary>
        /// Called after ArticleList is loaded. Sets the DataContext of ScrollViewer
        /// </summary>
        /// <param name="sender">EventSender</param>
        /// <param name="args">EventArgs</param>
        private void OnArticleListLoaded(object sender, RoutedEventArgs args)
        {
            ScrollViewer.DataContext = DataContext;
        }

        /// <summary>
        /// Called, when add new feeds is clicked.
        /// This button is only available, when the user has no feeds added.
        /// Sends a Mediation Notification to show the AddFeedView.
        /// </summary>
        /// <param name="sender">EventSender</param>
        /// <param name="args">EventArgs</param>
        private void OnAddFeedClicked(object sender, RoutedEventArgs args)
        {
            MediationHelper.Instance.SendNotification("ShowAddFeedView");
        }

        /// <summary>
        /// Called when an article is selected.
        /// Sends a Mediation Notification to show the corresponding article in the ArticleView.
        /// </summary>
        /// <param name="sender">EventSender</param>
        /// <param name="args">EventArgs</param>
        private void OnArticleSelected(object sender, RoutedEventArgs args)
        {
            var b = (FrameworkElement)sender;
            var item = (EasyNewsFeedItem) b.DataContext;

            MediationHelper.Instance.SendNotification("ShowArticleView", item);
            MediationHelper.Instance.SendNotification("UpdateMenuLink", item);
        }

        /// <summary>
        /// Called, when an article is Favorized or Unfavorized.
        /// Toggles the clicked favorite via FavoritesManager.
        /// Also updates the ViewModel.
        /// </summary>
        /// <param name="sender">EventSender</param>
        /// <param name="e">EventArgs</param>
        private void OnFavorized(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            var b = sender as Button;
            if (b == null)
            {
                return;
            }
            var item = b.DataContext as EasyNewsFeedItem;
            if (item != null)
            {
                FavoritesManager.Instance.ToggleFavorite(item);
                var feedItem = ((HomeViewModel)DataContext).FeedItems.Where((fItem) => fItem.Link == item.Link);
                var first = feedItem.First();
            }
        }

        /// <summary>
        /// Called whenever the window size changes.
        /// Passes the new screen width to the ViewModel to update the ColumnCount if required.
        /// </summary>
        /// <param name="sender">EventSender</param>
        /// <param name="args">EventArgs</param>
        private void OnWindowResize(object sender, RoutedEventArgs args)
        {
            ((HomeViewModel) DataContext).SetScreenWidth((int)ScrollViewer.ActualWidth);
        }
    }
}
