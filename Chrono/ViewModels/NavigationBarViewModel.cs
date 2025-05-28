using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Chrono.Commands;
using Chrono.Services;
using Chrono.Stores;
using Chrono.Utilities;

namespace Chrono.ViewModels
{
    class NavigationBarViewModel : ViewModelBase
    {
        /// <summary>
        /// Used to navigate to the Book Details view.
        /// </summary>
        private readonly INavigate _bookDetailsNavigationService;

        /// <summary>
        /// Used to manage the app's current book.
        /// </summary>
        private readonly BookStore _bookStore;

        /// <summary>
        /// Used to manage the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;

        /// <summary>
        /// Used to navigate to the Projects view.
        /// </summary>
        private readonly INavigate _projectsNavigationService;

        /// <summary>
        /// Used to navigate to the Start Screen view.
        /// </summary>
        private readonly INavigate _startScreenNavigationService;


        /// <summary>
        /// Executed when the Calculator button is clicked.
        /// </summary>
        public ICommand CalculatorButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Close button is clicked.
        /// </summary>
        public ICommand CloseButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Time Sheet button is clicked.
        /// </summary>
        public ICommand DailyTimeSheetButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Log Book button is clicked.
        /// </summary>
        public ICommand LogBookButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Projects button is clicked.
        /// </summary>
        public ICommand ProjectsButtonClickedCommand { get; }



        public NavigationBarViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            CalculatorButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnCalculatorButtonClicked));
            CloseButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnCloseButtonClickedCommand));
            DailyTimeSheetButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnDailyTimeSheetButtonClicked));
            LogBookButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnLogBookButtonClicked));
            ProjectsButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnProjectsButtonClicked));

            _bookDetailsNavigationService =
                ServiceFactory.CreateNavigationService(
                    "book details", _bookStore, _navigationStore);
            _projectsNavigationService =
                ServiceFactory.CreateNavigationService(
                    "projects", _bookStore, _navigationStore);
            _startScreenNavigationService =
                ServiceFactory.CreateNavigationService(
                     "start screen", _bookStore, _navigationStore);
        }



        private void OnCalculatorButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles the Close button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnCloseButtonClickedCommand(object? obj)
        {
            BookService.CloseCurrentBook(_bookStore);

            _startScreenNavigationService.Navigate();
        }


        private void OnDailyTimeSheetButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles the Log Book button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnLogBookButtonClicked(object? obj)
        {
            _bookDetailsNavigationService.Navigate();
        }

        /// <summary>
        /// Handles the Projects button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnProjectsButtonClicked(object? obj)
        {
            _projectsNavigationService.Navigate();
        }
    }
}
