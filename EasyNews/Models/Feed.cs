using System;
using System.ComponentModel;

namespace EasyNews.Models
{
    public class Feed : INotifyPropertyChanged
    {
        private string title;
        private string link;

        public string Link
        {
            get { return link;}
            set
            {
                if (link != value)
                {
                    link = value;
                    RaisePropertyChanged("Link");
                }
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }

        public Feed(string link, string title="")
        {
            this.title = title;
            this.link = link;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
}
}
