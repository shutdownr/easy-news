using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CodeHollow.FeedReader;
using Feed = CodeHollow.FeedReader.Feed;
using EasyNewsFeed = EasyNews.Models.Feed;

namespace EasyNews.Helpers
{
    class RssManager
    {
        private static readonly Lazy<RssManager> LazyRssManager = new Lazy<RssManager>(() => new RssManager());

        public static RssManager Instance { get { return LazyRssManager.Value; } }

        private RssManager()
        {

        }


        public  Task<List<Feed>> GetFeedsAsync(params EasyNewsFeed[] feeds)
        {
            Trace.WriteLine("GetFeedsAsync Started");
            var results = new List<Feed>();
            foreach (var feed in feeds)
            {
                Trace.WriteLine("STARTING TO PARSE " + feed.Title);
                var feedTask = FeedReader.ReadAsync(feed.Link);

                var f = feedTask.Result;
                f.Link = feed.Link;

                Trace.WriteLine("FINISHED PARSING " + feed.Title);
                results.Add(f);
            }

            Trace.WriteLine("GetFeedsAsync Finished");
            return Task.Run(() => results);
        }

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

        public Task<bool> IsValidRssFeed(string url)
        {
            if (url == null)
            {
                return Task.Run(() => false);
            }

            try
            {
                var uri = new Uri(url);
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

                Trace.WriteLine(Thread.CurrentThread.ManagedThreadId + " Async Task");
                if (feed.Items.Count > 0)
                {
                    return true;
                }
                return false;
            });
        }

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
            

            return "http://img05.deviantart.net/b8d4/i/2014/327/a/8/warframe_new_logo_look__vector__by_tasquick-d87fzxg.png";
        }
    }
}
