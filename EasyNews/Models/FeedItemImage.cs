using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeHollow.FeedReader;
using EasyNews.Helpers;

namespace EasyNews.Models
{
    class FeedItemImage
    {
        public string ImageLink { get; set; } = "";

        public FeedItem FeedItem { get; set; } = new FeedItem();

        public FeedItemImage()
        {

        }

        public FeedItemImage(FeedItem feedItem)
        {
            ImageLink = RSSManager.Instance.getImageUrl(feedItem);

            feedItem.Description = RSSManager.Instance.stripHTML(feedItem.Description);
            FeedItem = feedItem;         
        }

    }
}
