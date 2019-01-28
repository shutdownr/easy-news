using System.Collections.Generic;
using System.Linq;
using EasyNews.Helpers;
using EasyNews.Models;
using Feed = CodeHollow.FeedReader.Feed;

namespace EasyNews.ViewModels
{
    /// <summary>
    /// ViewModel, that is based off HomeViewModel.
    /// This ViewModel extends HomeViewModel by Add/Remove-Methods for feeds and feedItems.
    /// This ViewModel serves as a base for other ViewModels, that are used in Views.
    /// </summary>
    class ArticleViewModel : HomeViewModel
    {
        /// <summary>
        /// Adds a new feed to the list of feeds.
        /// The feed is downloaded and the articles are added to the list of articles as well.
        /// </summary>
        /// <param name="url">The url of the feed to be added</param>
        public async void AddFeed(string url)
        {
            var favorites = FavoritesManager.Instance.GetFavorites();

            var result = await RssManager.Instance.GetFeeds(url);
            var feed = result.First();
            RssFeeds.Add(feed);

            List<EasyNewsFeedItem> easyFeedItems = new List<EasyNewsFeedItem>();

            foreach (var feedItem in feed.Items)
            {
                var isFav = favorites.Contains(feedItem);
                var easyFeedItem = new EasyNewsFeedItem(feedItem, isFav);
                // Parse Image URL
                easyFeedItem.ImageLink = RssManager.Instance.GetImageUrl(feedItem);
                // Strip HTML tags
                easyFeedItem.Description = RssManager.Instance.StripHtml(feedItem.Description);
                // Add the item at the correct position;
                easyFeedItems.Add(easyFeedItem);
            }
            AddFeedItems(easyFeedItems);
        }

        /// <summary>
        /// Adds feedItems to the list of feedItems.
        /// All feedItems are inserted at the correct date (since the list is sorted by date)
        /// </summary>
        /// <param name="feedItems">List of feedItems to be added</param>
        private void AddFeedItems(List<EasyNewsFeedItem> feedItems)
        {
            var counter = 0;

            foreach (var article in feedItems)
            {
                // New Article is older than the oldest article in the list, append article to end
                if (counter >= FeedItems.Count)
                {
                    FeedItems.Add(article);
                }
                // Insert new Article somewhere in the middle of the array, we don't know where it is
                else
                {
                    // Iterate through Articles until we found the right date to insert our new article
                    while (counter < FeedItems.Count &&
                           FeedItems[counter].PublishingDate > article.PublishingDate)
                    {
                        counter++;
                    }
                    // All articles we searched were newer than our article => Append the article
                    if (counter == FeedItems.Count)
                    {
                        FeedItems.Add(article);
                    }
                    else
                    {
                        // Found the right date, insert the article, redo with the next one
                        FeedItems.Insert(counter, article);
                    }
                }
            }
            // ReSharper Disable All
            OnPropertyChanged("HasFeedItems");
            // ReSharper Restore All
        }

        /// <summary>
        /// Removes a feed from the list of feeds.
        /// After that the articles from that feed are also removed from the list of articles.
        /// </summary>
        /// <param name="url">URL of the feed to be removed</param>
        public void RemoveFeed(string url)
        {
            Feed feedToBeRemoved = null;
            for (var i = 0; i < RssFeeds.Count; i++)
            {
                if (RssFeeds[i].Link == url)
                {
                    feedToBeRemoved = RssFeeds[i];
                    RssFeeds.Remove(feedToBeRemoved);
                }
            }

            if (feedToBeRemoved == null)
            {
                return;
            }

            RemoveFeedItems(feedToBeRemoved);

        }


        /// <summary>
        /// Removes feedItems for a given feed from the list of feedItems.
        /// </summary>
        /// <param name="feed">The feed for which the articles should be removed</param>
        private void RemoveFeedItems(Feed feed)
        {
            var counter = 0;

            foreach (var article in feed.Items)
            {
                while (counter < FeedItems.Count && FeedItems[counter].Link != article.Link)
                {
                    counter++;
                }

                if (counter == FeedItems.Count)
                {
                    break;
                }
                else
                {
                    FeedItems.RemoveAt(counter);
                    if (counter > 0)
                    {
                        counter--;
                    }
                }
            }

            // ReSharper Disable All
            OnPropertyChanged("HasFeedItems");
            // ReSharper Restore All
        }
    }
}
