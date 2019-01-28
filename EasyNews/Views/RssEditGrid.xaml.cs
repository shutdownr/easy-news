using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EasyNews.Helpers;
using EasyNews.Models;
using EasyNews.Validation;
using EasyNews.ViewModels;

namespace EasyNews.Views
{
    /// <summary>
    /// Interaction logic for RssEditGrid.xaml
    /// </summary>
    public partial class RssEditGrid : UserControl
    {/*
        private RssEditViewModel _rssEditViewModel;

        public RssEditGrid()
        {
            InitializeComponent();
        }
        
        public void SetMode(RssEditMode mode)
        {
            _rssEditViewModelOld = new RssEditViewModelOLD(mode);
            if (mode == RssEditMode.Add)
            {
                AddButton.Visibility = Visibility.Visible;
                RemoveButton.Visibility = Visibility.Collapsed;
                FeedComboBox.Visibility = Visibility.Collapsed;
                UrlInputTextBox.Visibility = Visibility.Visible;
            }
            else if (mode == RssEditMode.Remove)
            {
                AddButton.Visibility = Visibility.Collapsed;
                RemoveButton.Visibility = Visibility.Visible;
                FeedComboBox.Visibility = Visibility.Visible;
                UrlInputTextBox.Visibility = Visibility.Collapsed;
            }
            Grid.DataContext = _rssEditViewModelOld;
        } 

        private void OnGridClicked(object sender, RoutedEventArgs args)
        {
            Keyboard.ClearFocus();
            Trace.WriteLine("Grid got focus!");
        }

        private void OnRemoveClicked(object sender, EventArgs args)
        {
            // Selected text is title of the feed, not the link
            var selectedFeed = FeedComboBox.Text;

            new ArticleViewModel().
            var feed = new CodeHollow.FeedReader.Feed();
            // Search for the matching feed, then forward it do update the ViewModels
            for (var i = 0; i < _rssEditViewModel.Feeds.Count; i++)
            {
                feed = _rssFeedViewModel.Feeds[i];
                if (feed.Title == selectedFeed)
                {
                    // Remove the feed in RssScroller, that will update the ViewModels
                    RssScroller.RemoveFeed(feed);
                    i--;
                }
            }

            FeedManager.Instance.RemoveFeed(feed.Link);
        }

        private void OnAddClicked(object sender, EventArgs args)
        {
            Trace.WriteLine(Thread.CurrentThread.ManagedThreadId + "ThreadButton ");

            // Validate the input given
            UrlInputTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            // Check if Feed has already been added, if so display an error
            if (File.Exists(@".\Feeds.txt"))
            {
                var feeds = File.ReadAllLines(@".\Feeds.txt");
                Trace.WriteLine(feeds.Length);
                Trace.WriteLine(_rssEditViewModelOld.CurrentUrl + " CurrentURL");
                if (feeds.Contains(_rssEditViewModelOld.CurrentUrl))
                {
                    // Create the error
                    ValidationError duplicateError = new ValidationError(new RssValidationRule(), UrlInputTextBox.GetBindingExpression(TextBox.TextProperty))
                    {
                        ErrorContent = "Feed has already been added"
                    };
                    // Attach it to the TextBox
                    System.Windows.Controls.Validation.MarkInvalid(UrlInputTextBox.GetBindingExpression(TextBox.TextProperty), duplicateError);
                    return;
                }
            }

            // No error, feed can be added to the file
            if (!UrlInputTextBox.GetBindingExpression(TextBox.TextProperty).HasValidationError)
            {
                // File does not exist, create it and write the new feed.
                if (!File.Exists(@".\Feeds.txt"))
                {
                    File.WriteAllText(@".\Feeds.txt", _rssEditViewModelOld.CurrentUrl + Environment.NewLine);
                }
                // File exists, append the new feed.
                else
                {
                    File.AppendAllText(@".\Feeds.txt", _rssEditViewModelOld.CurrentUrl + Environment.NewLine);
                }

                // Reload current feeds
                if (RssScroller != null)
                {
                    Trace.WriteLine("Reloading Feeds...");
                    RssScroller.AddFeed(new Feed(_rssEditViewModelOld.CurrentUrl));
                }
            }
        }*/

    }
}
