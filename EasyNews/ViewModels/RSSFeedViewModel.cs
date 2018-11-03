using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNews.Helpers;
using Feed = CodeHollow.FeedReader.Feed;
using EasyNewsFeed = EasyNews.Models.Feed;

namespace EasyNews.ViewModels
{
    class RSSFeedViewModel
    {
        public ObservableCollection<Feed> Feeds { get; set; }

        public async Task GetFeedData(ObservableCollection<EasyNewsFeed> feeds)
        {
            Feeds = new ObservableCollection<Feed>();
            var list = feeds.ToArray();
            var resultList = await RSSManager.Instance.GetFeedsAsync(list);
            foreach (var result in resultList)
            { 
                Feeds.Add(result);
                Trace.WriteLine(result.Title);
            }
        }
    }
}
