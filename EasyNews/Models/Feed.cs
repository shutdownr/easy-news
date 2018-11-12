using System.ComponentModel;

namespace EasyNews.Models
{
    public class Feed : INotifyPropertyChanged
    {
        private string _title;
        private string _link;

        public string Link
        {
            get { return _link;}
            set
            {
                if (_link != value)
                {
                    _link = value;
                    RaisePropertyChanged("Link");
                }
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }

        public Feed(string link, string title="")
        {
            _title = title;
            _link = link;
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
