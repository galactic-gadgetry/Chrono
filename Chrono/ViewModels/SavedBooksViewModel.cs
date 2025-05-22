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
        /// Used to navigate to the Book Details view.
        /// </summary>
        private readonly INavigate _bookDetailsNavigationService;

        /// <summary>
        /// Used to manage the app's current book.
        /// </summary>
        private readonly BookStore _bookStore;

        /// <summary>
        /// Used to navigate to the layout UI component.
        /// </summary>
        private readonly INavigate _layoutNavigationService;

        /// <summary>
        /// Used to manage the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;


        // Backing Fields
        private List<BookHeader> savedBooks = new();


        /// <summary>
        /// True if the <seealso cref="SavedBooks"/> collection
        /// count is less than 1, false otherwise
        /// </summary>
        public bool NoSavedBooks => SavedBooks.Count < 1;

        /// <summary>
        /// Collection of saved book header files.
        /// </summary>
        public List<BookHeader> SavedBooks
        {
            get => savedBooks;
            set
            {
                savedBooks = value;
                OnPropertyChanged(nameof(SavedBooks));
            }
        }


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

            _bookDetailsNavigationService =
                ServiceFactory.CreateNavigationService(
                    "book details", _bookStore, _navigationStore);
            _layoutNavigationService =
                ServiceFactory.CreateNavigationService(
                    "layout", _bookStore, _navigationStore);
        }


        /// <summary>
        /// Deletes the book files associated with the header.
        /// </summary>
        /// <param name="header"></param>
        private void DeleteBookRequested(BookHeader header)
        {
            BookService.DeleteBookFile(header);

            OnInfoUpdated($"Log book '{header.Name}' deleted");

            SavedBooks = BookService.GetSavedBookHeaders().
                OrderByDescending(b => b.DateTimeSaved).ToList();
        }

        /// <summary>
        /// Loads the book from a save file to the book store's
        /// current book and navigates to the Book Details view.
        /// </summary>
        /// <param name="header"></param>
        private void LoadBookRequested(BookHeader header)
        {
            _ = BookService.LoadBookToBookStoreFromJson(
                _bookStore, header.BookSaveFilePath);

            // Navigate to the Book Details view.
            _layoutNavigationService.Navigate();
            _bookDetailsNavigationService.Navigate();
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

        /// <summary>
        /// Handles the saved book card's Delete button click event.
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="InvalidOperationException">Thrown if
        /// the caller is not of type BookHeader</exception>
        private void OnSavedBookCardDeleteButtonClicked(object? obj)
        {
            BookHeader? header = obj as BookHeader;
            if (header == null)
            {
                throw new InvalidOperationException("The caller must " +
                    "be a BookHeader object");
            }

            bool result =
                DialogService.PromptUserWithDeleteConfirmationMessage(header);

            if (result)
            {
                DeleteBookRequested(header);
            }
        }

        /// <summary>
        /// Handles the saved book card's Load button click event.
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="InvalidOperationException"></exception>
        private void OnSavedBookCardLoadButtonClicked(object? obj)
        {
            BookHeader? header = obj as BookHeader;
            if (header == null)
            {
                throw new InvalidOperationException("The caller must " +
                    "be a BookHeader object");
            }

            LoadBookRequested(header);
        }
    }
}
