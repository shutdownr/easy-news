using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using EasyNews.Models;
using Feed = CodeHollow.FeedReader.Feed;

namespace EasyNews.ViewModels
{
    public class ArticleViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<EasyNewsFeedItem> _articles;

        public ObservableCollection<EasyNewsFeedItem> Articles
        {
            get { return _articles; }
            set
            {
                _articles = value;
                IsVisible = value.Count > 0;
                OnPropertyChanged();
            }
        }

        private string _link;

        public string Link
        {
            get
            {
                return _link;
            }
            set
            {
                _link = value;
                OnPropertyChanged();
            }
        }

        private bool _isVisible;

        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }
        
        public ArticleViewModel()
        {
            _link = "https://www.sueddeutsche.de/";
            Articles = new ObservableCollection<EasyNewsFeedItem>();
        }

        // Adds articles from one feed to the collection, without resetting it
        public void AddArticlesForFeed(Feed feed)
        {
            if (Articles.Count == 0)
            {
                IsVisible = true;
            }
            var counter = 0;
            foreach (var article in feed.Items)
            {
                EasyNewsFeedItem item = new EasyNewsFeedItem(article);

                // New Article is older than the oldest article in the list, append article to end
                if (counter >= Articles.Count)
                {
                    Articles.Add(item);
                }
                // Insert new Article somewhere in the middle of the array, we don't know where it is
                else
                {
                    // Iterate through Articles until we found the right date to insert our new article
                    while (counter < Articles.Count &&
                           Articles[counter].FeedItem.PublishingDate > article.PublishingDate)
                    {
                        counter++;
                    }
                    // All articles we searched were newer than our article => Append the article
                    if (counter == Articles.Count)
                    {
                        Articles.Add(item);
                    }
                    else
                    {
                        // Found the right date, insert the article, redo with the next one
                        Articles.Insert(counter, item);
                    }
                }
            }
        }

        public void RemoveArticlesForFeed(Feed feed)
        {
            var counter = 0;

            foreach (var article in feed.Items)
            {
                while (counter < Articles.Count && Articles[counter].FeedItem != article)
                {
                    counter++;
                }

                if (counter == Articles.Count)
                {
                    break;
                }
                else
                {
                    Articles.RemoveAt(counter);
                    if (counter > 0)
                    {
                        counter--;
                    }
                }
            }

            if (Articles.Count == 0)
            {
                IsVisible = false;
            }
        }


        public void GetArticlesForFeed(params Feed[] feeds)
        {
            var articlesList = new List<EasyNewsFeedItem>();
            foreach (var feed in feeds)
            {
                foreach (var article in feed.Items)
                {
                    EasyNewsFeedItem item = new EasyNewsFeedItem(article);
                    articlesList.Add(item);
                }
            }

            var articleListSorted = articlesList.OrderByDescending((article1) => { return article1.FeedItem.PublishingDate; }).ToList();
            Articles = new ObservableCollection<EasyNewsFeedItem>(articleListSorted);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
