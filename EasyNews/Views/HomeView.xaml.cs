using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using EasyNews.Models;
using EasyNews.ViewModels;
using MaterialDesignThemes.Wpf;

namespace EasyNews.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public ArticleViewModel ArticleViewModel;
        public FavoritesViewModel FavoritesViewModel;

        public Action<string> ArticleSelected { private get; set; }

        public HomeView()
        {
            InitializeComponent();
        }

        private void OnArticleListLoaded(object sender, RoutedEventArgs args)
        {
            ScrollViewer.DataContext = ArticleViewModel;
            Trace.WriteLine("OnArticleListloaded" + ArticleList.Items.Count);
            foreach (EasyNewsFeedItem item in ArticleList.Items)
            {
                Trace.WriteLine("EasyNewsFeedItem Link: " );
                if (FavoritesViewModel.Favorites.Contains(item.FeedItem.Link))
                {
                    Trace.WriteLine("Found favorite!");
                    var card = ArticleList.ItemContainerGenerator.ContainerFromItem(item);
                    var stackPanel1 = VisualTreeHelper.GetChild(card,0);
                    var stackPanel2 = VisualTreeHelper.GetChild(stackPanel1, 0);
                    var button = VisualTreeHelper.GetChild(stackPanel2, 2);
                    var ripple = VisualTreeHelper.GetChild(button, 0);
                    var grid = VisualTreeHelper.GetChild(ripple, 0);
                    var canvas = VisualTreeHelper.GetChild(grid, 1);
                    var packIcon = VisualTreeHelper.GetChild(canvas, 0) as PackIcon;
                    if (packIcon == null)
                    {
                        continue;
                    }

                    packIcon.Foreground = (Brush) FindResource("PrimaryHueDarkBrush");
                }
            }
        }

        private void OnArticleSelected(object sender, RoutedEventArgs args)
        {
            var b = (FrameworkElement)sender;
            var link = (string)b.Tag;
            ArticleSelected(link);
        }

        private void OnFavorized(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            var b = sender as Button;
            var link = b.Tag.ToString();

            var icon = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(b, 0),0),1),0) as PackIcon;
            
            if (icon == null)
            {
                return;
            }

            if (FavoritesViewModel.Favorites.Contains(link))
            {
                icon.Foreground = (Brush) FindResource("MaterialDesignBodyLight");
                FavoritesViewModel.Favorites.Remove(link);
            }
            else
            {
                icon.Foreground = (Brush) FindResource("PrimaryHueDarkBrush");
                FavoritesViewModel.Favorites.Add(link);
            }

            FavoritesViewModel.SaveFavorites();
        }
    }
}
