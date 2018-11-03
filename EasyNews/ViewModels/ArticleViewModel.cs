using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeHollow.FeedReader;
using EasyNews.Helpers;
using EasyNews.Models;
using Feed = CodeHollow.FeedReader.Feed;

namespace EasyNews.ViewModels
{
    class ArticleViewModel
    {
        public ObservableCollection<FeedItemImage> Articles { get; set; }

        private string link;

        public string Link
        {
            get { return link; } set { link = value; }
        }

        public ArticleViewModel()
        {
            link = "https://www.sueddeutsche.de/";
        }

        public void GetArticlesForFeed(params Feed[] feeds)
        {
            var articlesList = new List<FeedItemImage>();
            foreach (var feed in feeds)
            {
                foreach (var article in feed.Items)
                {
                    FeedItemImage itemImage = new FeedItemImage(article);
                    articlesList.Add(itemImage);
                    Trace.WriteLine("Added article " + article.Title);
                }
            }

            var articleListSorted = articlesList.OrderByDescending((article1) => { return article1.FeedItem.PublishingDate; }).ToList();
            Articles = new ObservableCollection<FeedItemImage>(articleListSorted);
        }
    }
}
