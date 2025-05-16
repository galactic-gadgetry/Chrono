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
    class StartScreenViewModel : ViewModelBase
    {
        /// <summary>
        /// Used to manage the app's current book.
        /// </summary>
        private readonly BookStore _bookStore;

        /// <summary>
        /// Used to navigate to the Create New Book view.
        /// </summary>
        private readonly INavigate _createNewBookNavigationService;

        /// <summary>
        /// Used to navigate to the layout UI component.
        /// </summary>
        private readonly INavigate _layoutNavigationService;

        /// <summary>
        /// Used to  manage the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;


        /// <summary>
        /// Text for the Welcome Message label.
        /// </summary>
        public string WelcomeMessageText
        {
            get
            {
                TimeSpan currentTime = DateTime.Now.TimeOfDay;
                TimeSpan noon = new(12, 0, 0);
                TimeSpan fivePM = new(17, 0, 0);
                if (currentTime < noon)
                {
                    return "Good morning";
                }
                else if (currentTime > noon && currentTime < fivePM)
                {
                    return "Good afternoon";
                }
                else
                {
                    return "Good evening";
                }
            }
        }


        /// <summary>
        /// Executed when the Create New Log Book button is clicked.
        /// </summary>
        public ICommand CreateNewLogBookButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Open Existing Log Book button is
        /// clicked.
        /// </summary>
        public ICommand OpenExistingLogBookButtonClickedCommand { get; }



        public StartScreenViewModel(BookStore bookStore, NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            CreateNewLogBookButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnCreateNewLogBookButtonClicked));
            OpenExistingLogBookButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnOpenExistingLogBookButtonClicked));

            _createNewBookNavigationService =
                ServiceFactory.CreateNavigationService(
                    "create new book", _bookStore, _navigationStore);
            _layoutNavigationService =
                ServiceFactory.CreateNavigationService(
                    "layout", _bookStore, _navigationStore);
        }


        /// <summary>
        /// Handles the Create New Log Book button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnCreateNewLogBookButtonClicked(object? obj)
        {
            _layoutNavigationService.Navigate();
            _createNewBookNavigationService.Navigate();
        }

        
        private void OnOpenExistingLogBookButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}
