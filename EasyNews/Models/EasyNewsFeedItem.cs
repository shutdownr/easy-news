using System.ComponentModel;
using CodeHollow.FeedReader;

namespace EasyNews.Models
{
    /// <summary>
    /// Custom FeedItem model class, extended from FeedReader.FeedItem
    /// Contains all the info about one article, and the link to the image and if the article is a favorite.
    /// </summary>
    public class EasyNewsFeedItem : FeedItem, INotifyPropertyChanged
    {
        /// <summary>
        /// URL to the image as a string
        /// </summary>
        private string _imageLink;
        /// <summary>
        /// Bool that indicates if the article is a favorite
        /// </summary>
        private bool _isFavorite;

        /// <summary>
        /// Property for _imageLink
        /// </summary>
        public string ImageLink
        {
            get { return _imageLink; }
            set
            {
                if (_imageLink != value)
                {
                    _imageLink = value;
                    RaisePropertyChanged("ImageLink");
                }
            }
        }

        /// <summary>
        /// Property for _isFavorite
        /// </summary>
        public bool IsFavorite
        {
            get { return _isFavorite; }
            set
            {
                if (_isFavorite != value)
                {
                    _isFavorite = value;
                    RaisePropertyChanged("IsFavorite");
                }
            }
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public EasyNewsFeedItem()
        {

        }

        /// <summary>
        /// Constructor from a FeedItem and optionally an initial value for IsFavorite
        /// </summary>
        /// <param name="feedItem">The feedItem from which to construct an EasyNewsFeedItem</param>
        /// <param name="isFavorite">The initial value for IsFavorite</param>
        public EasyNewsFeedItem(FeedItem feedItem, bool isFavorite = false)
        {
            Link = feedItem.Link;
            Description = feedItem.Description;
            Author = feedItem.Author;
            Categories = feedItem.Categories;
            Content = feedItem.Content;
            Id = feedItem.Id;
            PublishingDate = feedItem.PublishingDate;
            PublishingDateString = feedItem.PublishingDateString;
            SpecificItem = null;
            Title = feedItem.Title;

            IsFavorite = isFavorite;
        }

        /// <summary>
        /// The model's PropertyChangedEventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Property Changed function, that calls the handler
        /// </summary>
        /// <param name="property">The name of the property, that changed</param>
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
