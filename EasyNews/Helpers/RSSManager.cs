using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CodeHollow.FeedReader;
using Feed = CodeHollow.FeedReader.Feed;

namespace EasyNews.Helpers
{
    /// <summary>
    /// A manager used to communicate with the RSS-Feeds, which is also responsible for some handling of the feeds.
    /// The downloading and parsing of the feeds is done by FeedReader, an external library used only for that purpose.
    /// </summary>
    class RssManager
    {
        /// <summary>
        /// Lazy singleton instance
        /// </summary>
        private static readonly Lazy<RssManager> LazyRssManager = new Lazy<RssManager>(() => new RssManager());

        /// <summary>
        /// Instance that is accessed by other classes
        /// </summary>
        public static RssManager Instance { get { return LazyRssManager.Value; } }

        /// <summary>
        /// Empty constructor
        /// </summary>
        private RssManager()
        {

        }

        /// <summary>
        /// Returns a list of feeds downloaded by the FeedReader.
        /// Shows an error if the internet connection isn't working.
        /// </summary>
        /// <param name="urls">URLs to be downloaded</param>
        /// <returns>The list of downloaded feeds</returns>
        public Task<List<Feed>> GetFeeds(params string[] urls)
        {
            var results = new List<Feed>();

            foreach (var url in urls)
            {
                var feedTask = FeedReader.ReadAsync(url);
                Feed f = feedTask.Result;
                f.Link = url;
                
                results.Add(f);
            }

            return Task.Run(() => results);
        }

        /// <summary>
        /// Strips html tags from a string, converts HTML-entities to symbols
        /// </summary>
        /// <param name="s">String to be formatted</param>
        /// <returns>The formatted string</returns>
        public string StripHtml(string s)
        {
            // Remove HTML-tags
            var htmlRegex = new Regex("<.*?>");
            var output = htmlRegex.Replace(s, "");
            // Convert HTML codes to symbols
            output = System.Net.WebUtility.HtmlDecode(output);
            // Strip leading/trailing whitespace
            output = output.Trim();

            return output;
        }

        /// <summary>
        /// Checks if a string is a valid url to an RSS-Feed
        /// </summary>
        /// <param name="url">The string to be checked</param>
        /// <returns>A bool, that indicates whether the url is valid or not</returns>
        public Task<bool> IsValidRssFeed(string url)
        {
            if (url == null)
            {
                return Task.Run(() => false);
            }

            try
            {
                var _ = new Uri(url);   
            }
            catch (Exception e)
            {
                Trace.WriteLine("Entered URL was invalid. " + e.StackTrace);
                return Task.Run(() => false);
            }
            
            return Task.Run(async () =>
            {
                Feed feed;
                try
                {
                    feed = await FeedReader.ReadAsync(url);
                }
                catch (Exception)
                {
                    return false;
                }

                if (feed.Items.Count > 0)
                {
                    return true;
                }
                return false;
            });
        }

        /// <summary>
        /// Extracts the url of the image from a FeedItem, that is returned from the FeedReader.
        /// The url is parsed from different locations, since most RSS-Feeds use different ways to encapsulate their image-URLs.
        /// </summary>
        /// <param name="item">The item, for which the url is needed</param>
        /// <returns>The url of the image, or a link to an "Image-not-found" image, if the feed has no images, or an image could not be found</returns>
        public string GetImageUrl(FeedItem item)
        {
            var desc = item.Description;

            var imgRegex = new Regex("src=[\"'](.*?)[\"']");
            var imgRegex2 = new Regex("src=(http.*?) ");

            // Capture src in description
            var match = imgRegex.Match(desc);
            if (match.Success)
            {
                return match.Groups[1].ToString();
            }

            // Capture with a different pattern
            match = imgRegex2.Match(desc);
            if (match.Success)
            {
                return match.Groups[1].ToString();
            }

            return @"..\Images\NoImage.png";
        }
    }
}
