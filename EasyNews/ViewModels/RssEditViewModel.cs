using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EasyNews.ViewModels
{
    public class RssEditViewModel : INotifyPropertyChanged
    {
        private RssEditMode _mode;

        public RssEditMode Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                OnPropertyChanged();
            }
        }

        private string _currentUrl;

        public string CurrentUrl
        {
            get { return _currentUrl; }
            set
            {
                _currentUrl = value;
                OnPropertyChanged();
            }
        }


        public string Title
        {
            get
            {
                if (RssEditMode.Add == Mode)
                {
                    return "Add Feed";
                }
                else if (RssEditMode.Remove == Mode)
                {
                    return "Remove Feed";
                }

                return "";
            }
        }


        public string IconKind
        {
            get
            {
                if (RssEditMode.Add == Mode)
                {
                    return "plus";
                }
                else if (RssEditMode.Remove == Mode)
                {
                    return "minus";
                }

                return "";
            }
        }


        public RssEditViewModel(RssEditMode mode)
        {
            Mode = mode;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum RssEditMode
    {
        Add, Remove
    }
}
