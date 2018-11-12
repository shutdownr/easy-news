using CodeHollow.FeedReader;
using EasyNews.Helpers;

namespace EasyNews.Models
{
    public class EasyNewsFeedItem
    {
        public string ImageLink { get; set; } = "";

        public FeedItem FeedItem { get; set; } = new FeedItem();

        public EasyNewsFeedItem()
        {

        }

        public EasyNewsFeedItem(FeedItem feedItem)
        {
            ImageLink = RssManager.Instance.GetImageUrl(feedItem);

            feedItem.Description = RssManager.Instance.StripHtml(feedItem.Description);
            FeedItem = feedItem;         
        }

    }
}
