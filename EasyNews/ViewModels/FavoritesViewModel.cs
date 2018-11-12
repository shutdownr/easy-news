using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using EasyNews.Helpers;

namespace EasyNews.ViewModels
{
    public class FavoritesViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<string> _favorites;

        public ObservableCollection<string> Favorites
        {
            get { return _favorites; }
            set
            {
                _favorites = value;
                OnPropertyChanged();
            }
        }
        

        public FavoritesViewModel()
        {
            Favorites = new ObservableCollection<string>();
            InitFavorites();
        }

        private void InitFavorites()
        {
            string[] favs = { };
            try
            {
                favs = File.ReadAllLines(@".\Favorites.txt");
            }
            catch (FileNotFoundException)
            {
                File.Create(@".\Favorites.txt").Close();
            }
            catch (Exception e)
            {
                ErrorManager.Instance.DisplayError(e.Message);
                return;
            }

            Favorites = new ObservableCollection<string>(favs);
        }

        public void SaveFavorites()
        {
            string[] favs = Favorites.ToArray();
            Array.Sort(favs);
            File.WriteAllLines(@".\Favorites.txt", favs);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
