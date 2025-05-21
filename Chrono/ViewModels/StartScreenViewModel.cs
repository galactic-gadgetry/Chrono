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
        /// Used to navigate to the Saved Books view.
        /// </summary>
        private readonly INavigate _savedBooksNavigationService;


        // Backing Fields
        private List<BookHeader> savedBooks = new();


        /// <summary>
        /// True if the <seealso cref="SavedBooks"/> collection count
        /// is less than 1, false otherwise.
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

        /// <summary>
        /// Executed when the More Documents hyperlink is clicked.
        /// </summary>
        public ICommand MoreDocumentsHyperlinkClickedCommand { get; }

        /// <summary>
        /// Executed when a saved book card's Delete button is
        /// clicked.
        /// </summary>
        public ICommand SavedBookCardDeleteButtonClickedCommand { get; }

        /// <summary>
        /// Executed when a saved book card's Load button is clicked.
        /// </summary>
        public ICommand SavedBookCardLoadButtonClickedCommand { get; }



        public StartScreenViewModel(BookStore bookStore, NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            SavedBooks = GetSavedBooksList();

            CreateNewLogBookButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnCreateNewLogBookButtonClicked));
            OpenExistingLogBookButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnOpenExistingLogBookButtonClicked));
            MoreDocumentsHyperlinkClickedCommand = new RelayCommand(
                new Action<object?>(OnMoreDocumentsHyperlinkClicked));
            SavedBookCardDeleteButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnSavedBookCardDeleteButtonClicked));
            SavedBookCardLoadButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnSavedBookCardLoadButtonClicked));

            _createNewBookNavigationService =
                ServiceFactory.CreateNavigationService(
                    "create new book", _bookStore, _navigationStore);
            _layoutNavigationService =
                ServiceFactory.CreateNavigationService(
                    "layout", _bookStore, _navigationStore);
            _savedBooksNavigationService =
                ServiceFactory.CreateNavigationService(
                    "saved books", _bookStore, _navigationStore);
        }


        /// <summary>
        /// Deletes the book files associated with the header.
        /// </summary>
        /// <param name="header"></param>
        private void DeleteBookRequested(BookHeader header)
        {
            BookService.DeleteBookFile(header);

            OnInfoUpdated($"Log book '{header.Name}' deleted");

            // Reset the SavedBooks collection.
            SavedBooks = GetSavedBooksList();
        }

        /// <summary>
        /// Retrieves the 10 most recent saved book header files.
        /// </summary>
        /// <returns></returns>
        private List<BookHeader> GetSavedBooksList()
        {
            List<BookHeader> headers = BookService.GetSavedBookHeaders().
                OrderByDescending(b => b.DateTimeSaved).ToList();

            if (headers.Count > 10)
            {
                return headers.GetRange(0, 10);
            }
            else
            {
                return headers;
            }
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

        /// <summary>
        /// Handles the Open Existing Log Book button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnOpenExistingLogBookButtonClicked(object? obj)
        {
            _layoutNavigationService.Navigate();
            _savedBooksNavigationService.Navigate();
        }

        /// <summary>
        /// Handles the More Documents hyperlink click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnMoreDocumentsHyperlinkClicked(object? obj)
        {
            _layoutNavigationService.Navigate();
            _savedBooksNavigationService.Navigate();
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


        private void OnSavedBookCardLoadButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}
