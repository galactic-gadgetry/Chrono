using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Chrono.Commands;
using Chrono.Models;
using Chrono.Services;
using Chrono.Stores;
using Chrono.Utilities;

namespace Chrono.ViewModels
{
    class SavedBooksViewModel : HybridViewModelBase
    {
        /// <summary>
        /// Used to manage the app's current book.
        /// </summary>
        private readonly BookStore _bookStore;

        /// <summary>
        /// Used to manage the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;


        /// <summary>
        /// True if the <seealso cref="SavedBooks"/> collection
        /// count is less than 1, false otherwise
        /// </summary>
        public bool NoSavedBooks => SavedBooks.Count < 1;

        /// <summary>
        /// Collection of saved book header files.
        /// </summary>
        public List<BookHeader> SavedBooks { get; set; }


        /// <summary>
        /// Executed when the Back button is clicked.
        /// </summary>
        public ICommand BackButtonClickedCommand { get; }

        /// <summary>
        /// Executed when a saved book card's Delete button is
        /// clicked.
        /// </summary>
        public ICommand SavedBookCardDeleteButtonClickedCommand { get; }

        /// <summary>
        /// Executed when a saved book card's Load button is clicked.
        /// </summary>
        public ICommand SavedBookCardLoadButtonClickedCommand { get; }



        public SavedBooksViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            SavedBooks = BookService.GetSavedBookHeaders().
                OrderByDescending(b => b.DateTimeSaved).ToList();

            BackButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnBackButtonClicked));
            SavedBookCardDeleteButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnSavedBookCardDeleteButtonClicked));
            SavedBookCardLoadButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnSavedBookCardLoadButtonClicked));
        }


        
        private void OnBackButtonClicked(object? obj)
        {
            if (_bookStore.CurrentBook.IsBookVoid)
            {
                INavigate _startScreenNavigationService =
                    ServiceFactory.CreateNavigationService(
                        "start screen", _bookStore, _navigationStore);
                _startScreenNavigationService.Navigate();
            }
            else
            {
                throw new NotImplementedException();
            }
        }


        private void OnSavedBookCardDeleteButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }


        private void OnSavedBookCardLoadButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}
