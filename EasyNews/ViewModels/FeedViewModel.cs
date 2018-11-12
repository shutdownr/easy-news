using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using EasyNews.Helpers;
using EasyNews.Models;

namespace EasyNews.ViewModels
{
    public class FeedViewModel : INotifyPropertyChanged
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

        private static readonly string[] DefaultFeeds =
        {
            "https://www.bild.de/rssfeeds/vw-sport/vw-sport-16729856,view=rss2.bild.xml",
            "http://www.faz.net/rss/aktuell/sport/fussball/",
            "http://rss.sueddeutsche.de/rss/Sport",
            "https://www.frankenpost.de/storage/rss/rss/fp/nachrichten_hofrehau.xml",
        };

        public void InitFeeds()
        {
            var feeds = new ObservableCollection<Feed>();
            // Init from file
            string[] lines;

            try
            {
                lines = File.ReadAllLines(@".\Feeds.txt");
            }
            catch (FileNotFoundException)
            {
                File.WriteAllLines(@".\Feeds.txt", DefaultFeeds);
                lines = DefaultFeeds;
            }
            catch (Exception e)
            {
                ErrorManager.Instance.DisplayError(e.Message);
                return;
            }

            foreach (var link in lines)
            {
                Trace.WriteLine("Got link: " + link);
                feeds.Add(new Feed(link));
            }

            Feeds = feeds;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}