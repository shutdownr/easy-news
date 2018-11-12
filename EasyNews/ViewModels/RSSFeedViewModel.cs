using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EasyNews.Helpers;
using Feed = CodeHollow.FeedReader.Feed;
using EasyNewsFeed = EasyNews.Models.Feed;

namespace EasyNews.ViewModels
{
    public class RssFeedViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Feed> _feeds;

        public ObservableCollection<Feed> Feeds
        {
            get { return _feeds; }
            set
            {
                _feeds = value;
                OnPropertyChanged();
            }
        }

        public async Task GetFeedData(ObservableCollection<EasyNewsFeed> feeds)
        {
            Feeds = new ObservableCollection<Feed>();
            var list = feeds.ToArray();
            var resultList = await RssManager.Instance.GetFeedsAsync(list);
            foreach (var result in resultList)
            {
                // Sort FeedItems as they might not be sorted yet
                result.Items = result.Items.OrderByDescending((article) => { return article.PublishingDate; }).ToList();
                Feeds.Add(result);
                Trace.WriteLine(result.Title);
            }
        }

        // Gets a single Feed and adds it to the FeedCollection, without resetting the collection
        public async Task AddFeedData(EasyNewsFeed feed)
        {
            var result = await RssManager.Instance.GetFeedsAsync(feed);
            Feeds.Add(result.First());
            Trace.WriteLine("Added new single Feed: " + result.First().Title);
        }

        public void RemoveFeedData(Feed feed)
        {
            Feeds.Remove(feed);
            Trace.WriteLine("Removed a single Feed: " + feed.Title);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
