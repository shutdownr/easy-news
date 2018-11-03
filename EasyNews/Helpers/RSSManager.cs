using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CodeHollow.FeedReader;
using EasyNews.Models;
using Feed = CodeHollow.FeedReader.Feed;
using EasyNewsFeed = EasyNews.Models.Feed;

namespace EasyNews.Helpers
{
    class RSSManager
    {
        private static readonly Lazy<RSSManager> LazyRSSManager = new Lazy<RSSManager>(() => new RSSManager());

        public static RSSManager Instance { get { return LazyRSSManager.Value; } }

        private RSSManager()
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

                Trace.WriteLine("FINISHED PARSING " + feed.Title);
                results.Add(f);
            }

            Trace.WriteLine("GetFeedsAsync Finished");
            return Task.Run(() => results);
        }

        public string stripHTML(string s)
        {
            Trace.WriteLine("Stripping HTML from:\n" + s);
            // Remove HTML-tags
            var htmlRegex = new Regex("<.*?>");
            var output = htmlRegex.Replace(s, "");
            // Convert HTML codes to symbols
            output = System.Net.WebUtility.HtmlDecode(output);
            // Strip leading/trailing whitespace
            output = output.Trim();

            Trace.WriteLine("Stripped HTML: \n" + output);
            return output;
        }

        public string getImageUrl(FeedItem item)
        {
            var desc = item.Description;

            var imgRegex = new Regex("src=[\"'](.*?)[\"']");
            var imgRegex2 = new Regex("src=(http.*?) ");

            // Capture src in description
            var match = imgRegex.Match(desc);
            if (match.Success)
            {
                Trace.WriteLine("Captured <img src=\"\">!");
                return match.Groups[1].ToString();
            }

            // Capture with a different pattern
            match = imgRegex2.Match(desc);
            if (match.Success)
            {
                Trace.WriteLine("Captured <img src=>!");
                return match.Groups[1].ToString();
            }
            

            Trace.WriteLine("Using default image, didn't capture anything...\n" + item.Link);
            return "http://img05.deviantart.net/b8d4/i/2014/327/a/8/warframe_new_logo_look__vector__by_tasquick-d87fzxg.png";
        }
    }
}
