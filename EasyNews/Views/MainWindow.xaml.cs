using System.Windows;
using EasyNews.Helpers;
using EasyNews.Utility;
using EasyNews.ViewModels;

namespace EasyNews.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// The MainViewModel, that controls navigation and control-flow in the application
        /// </summary>
        private MainViewModel _mainViewModel;

        /// <summary>
        /// The MenuViewModel, that controls the MenuBar.
        /// </summary>
        private MenuViewModel _menuViewModel;

        /// <summary>
        /// Constructor
        /// Sets this window as MainWindw of the application. Used in Settingsmanager for Window position.
        /// Applies the Theme and sets the initial position of the window.
        /// Adds a BoolToColorConverter to the Resources.
        /// Initializes the component and the members.
        /// </summary>
        public MainWindow()
        {
            Application.Current.MainWindow = this;

            SettingsManager.Instance.ApplyTheme(SettingsManager.Instance.GetCurrentTheme());
            SettingsManager.Instance.ApplyLastWindowPlacement();

            var boolToColorConverter = new BoolToColorConverter("SecondaryAccentBrush", "MaterialDesignDarkForeground");
            boolToColorConverter.ContextElement = this;
            Resources.Add("ColorConverter", boolToColorConverter);

            InitializeComponent();

            _menuViewModel = new MenuViewModel();
            Menu.DataContext = _menuViewModel;

            _mainViewModel = new MainViewModel();
            DataContext = _mainViewModel;
            Pages.DataContext = _mainViewModel;



            _mainViewModel.ChangeCommand.Execute(new HomeViewModel());
        }

        /// <summary>
        /// Called, when the heart in the MenuBar is clicked.
        /// MenuViewModel toggles isFavorite for the current article.
        /// </summary>
        /// <param name="sender">EventSender</param>
        /// <param name="args">EventArgs</param>
        public void OnFavorized(object sender, RoutedEventArgs args)
        {
            _menuViewModel.ToggleFavorite();
        }

        /// <summary>
        /// Called when the MenuButton "Add Feed" is clicked.
        /// Passes an AddFeedViewModel to the MainViewModel, which will in the end navigate to the corresponding View.
        /// </summary>
        /// <param name="sender">EventSender</param>
        /// <param name="e">EventArgs</param>
        private void OnAddFeedClicked(object sender, RoutedEventArgs e)
        {
            _mainViewModel.ChangeCommand.Execute(new AddFeedViewModel());
        }

        /// <summary>
        /// Called when the MenuButton "Remove Feed" is clicked.
        /// Passes a RemoveFeedViewModel to the MainViewModel, which will in the end navigate to the corresponding View.
        /// </summary>
        /// <param name="sender">EventSender</param>
        /// <param name="e">EventArgs</param>
        private void OnRemoveFeedClicked(object sender, RoutedEventArgs e)
        {
            _mainViewModel.ChangeCommand.Execute(new RemoveFeedViewModel());
        }

        /// <summary>
        /// Called when the MenuButton "Home" is clicked.
        /// Passes a HomeViewModel to the MainViewModel, which will in the end navigate to the corresponding View.
        /// </summary>
        /// <param name="sender">EventSender</param>
        /// <param name="e">EventArgs</param>
        private void OnHomeClicked(object sender, RoutedEventArgs e)
        {
            _mainViewModel.ChangeCommand.Execute(new HomeViewModel());
        }

        /// <summary>
        /// Called when the MenuButton "Favorites" is clicked.
        /// Passes a FavoritesViewModel to the MainViewModel, which will in the end navigate to the corresponding View.
        /// </summary>
        /// <param name="sender">EventSender</param>
        /// <param name="e">EventArgs</param>
        private void OnFavoritesClicked(object sender, RoutedEventArgs e)
        {
            _mainViewModel.ChangeCommand.Execute(new FavoriteViewModel());
        }

        /// <summary>
        /// Called when the MenuButton "Settings" is clicked.
        /// Passes a SettingsViewModel to the MainViewModel, which will in the end navigate to the corresponding View.
        /// </summary>
        /// <param name="sender">EventSender</param>
        /// <param name="e">EventArgs</param>
        private void OnSettingsClicked(object sender, RoutedEventArgs e)
        {
            _mainViewModel.ChangeCommand.Execute(new SettingsViewModel());
        }
    }


}