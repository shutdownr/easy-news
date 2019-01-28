using System;
using System.Windows.Input;
using EasyNews.Helpers;
using EasyNews.Models;

namespace EasyNews.ViewModels
{
    /// <summary>
    /// MainViewModel, that is responsible for navigation between different Views.
    /// The current ViewModel is stored here, this ViewModel also updates the ViewModelHolder.
    /// This ViewModel is updated every time the View is changed.
    /// </summary>
    class MainViewModel : BaseViewModel
    {
        /// <summary>
        /// The current ViewModel
        /// </summary>
        private BaseViewModel _currentViewModel;

        /// <summary>
        /// A PageCommand, that changes the current ViewModel and passes it to the ViewModelHolder
        /// </summary>
        private PageCommand _changeCommand;
       
        /// <summary>
        /// Property of _currentViewModel
        /// </summary>
        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            private set { SetProperty(ref _currentViewModel, value); }
        }

        /// <summary>
        /// Property of _changeCommand
        /// </summary>
        public PageCommand ChangeCommand
        {
            get { return _changeCommand; }
            private set { SetProperty(ref _changeCommand, value); }
        }


        /// <summary>
        /// Constructor, that initializes ChangeCommand.
        /// Also subscribes to Mediation notifications for View-changes
        /// </summary>
        public MainViewModel()
        {
            ChangeCommand = new PageCommand(
                param => param is BaseViewModel,
                param => ChangeCurrentViewModel((BaseViewModel) param)
            );

            InitNavigationActions();
        }

        /// <summary>
        /// Called in the constructor to subscribe to View-Changes
        /// </summary>
        private void InitNavigationActions()
        {
            MediationHelper.Instance.Subscribe("ShowArticleView", ShowArticleView);
            MediationHelper.Instance.Subscribe("ShowAddFeedView", ShowAddFeedView);
        }

        /// <summary>
        /// Shows the AddFeedView
        /// </summary>
        /// <param name="param">Not used, but required by Mediation</param>
        private void ShowAddFeedView(object param)
        {
            ChangeCommand.Execute(new AddFeedViewModel());
        } 

        /// <summary>
        /// Shows the ArticleView
        /// </summary>
        /// <param name="param">Not used, but required by Mediation</param>
        private void ShowArticleView(object param)
        {
            var item = (EasyNewsFeedItem)param;
            ChangeCommand.Execute(new CurrentArticleViewModel(item.Link));
        }

        /// <summary>
        /// Changes the CurrentViewModel and updates the ViewModelHolder
        /// </summary>
        /// <param name="viewModel">The new ViewModel</param>
        private void ChangeCurrentViewModel(BaseViewModel viewModel)
        {
            CurrentViewModel = viewModel;
            ViewModelHolder.Instance.CurrentViewModel = viewModel;
        }

        /// <summary>
        /// Inner class used only for ChangeCommand, derived from ICommand
        /// </summary>
        public class PageCommand : ICommand
        {
            /// <summary>
            ///  Action that is executed, when Execute is called and CanExecute is true at the same time.
            /// </summary>
            private Action<object> _execute;

            /// <summary>
            /// Condition, that is required for _execute to be fired.
            /// </summary>
            private Predicate<object> _canExecute;

            /// <summary>
            /// Constructor that initializes the members
            /// </summary>
            /// <param name="canExecute">Initial value for _canExecute</param>
            /// <param name="execute">Initial value for _execute</param>
            public PageCommand(Predicate<object> canExecute, Action<object> execute)
            {
                _canExecute = canExecute;
                _execute = execute;
            }

            /// <summary>
            /// Method that checks if _canExecute is true for a certain parameter
            /// </summary>
            /// <param name="parameter">The parameter to pass to _canExecute</param>
            /// <returns>what _canExecute returns for the value of "parameter"</returns>
            public bool CanExecute(object parameter)
            {
                return _canExecute(parameter);
            }

            /// <summary>
            /// Fires _execute with a parameter
            /// </summary>
            /// <param name="parameter">The parameter to pass to _execute</param>
            public void Execute(object parameter)
            {
                _execute(parameter);
            }

            /// <summary>
            /// EventHandler for changing CanExecute
            /// </summary>
            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }
    }

    
}
