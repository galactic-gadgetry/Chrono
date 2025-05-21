using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chrono.Commands;
using Chrono.Models.DTOs;
using Chrono.Models;
using Chrono.Services;
using Chrono.Stores;
using Chrono.Utilities;
using System.Windows.Input;

namespace Chrono.ViewModels
{
    class EditBookDetailsViewModel : DefaultViewModelBase
    {
        // Backing Fields
        private string nameText = string.Empty;


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
        /// True if the view inputs are valid, false otherwise.
        /// </summary>
        public bool CanSaveDetails
        {
            get
            {
                return !string.IsNullOrEmpty(nameText);
            }
        }

        /// <summary>
        /// The app's current book.
        /// </summary>
        public Book CurrentBook => _bookStore.CurrentBook;

        /// <summary>
        /// Text for the Name text box.
        /// </summary>
        public string NameText
        {
            get => nameText;
            set
            {
                nameText = value;
                OnPropertyChanged(nameof(NameText));
                OnPropertyChanged(nameof(CanSaveDetails));
            }
        }


        /// <summary>
        /// Executed when the Cancel button is clicked.
        /// </summary>
        public ICommand CancelButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Save button is clicked.
        /// </summary>
        public ICommand SaveButtonClickedCommand { get; }



        public EditBookDetailsViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            NameText = CurrentBook.Name;

            CancelButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnCancelButtonClicked));
            SaveButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnSaveButtonClicked));

            _bookDetailsNavigationService =
                ServiceFactory.CreateNavigationService(
                    "book details", _bookStore, _navigationStore);
        }


        /// <summary>
        /// Handles the Cancel button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnCancelButtonClicked(object? obj)
        {
            _bookDetailsNavigationService.Navigate();
        }


        /// <summary>
        /// Handles the Save button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnSaveButtonClicked(object? obj)
        {
            // Create a DTO with the input field details.
            BookDTO dto = new()
            {
                Name = NameText,
            };

            // Update the book details and save to file.
            BookService.EditBookDetails(dto, CurrentBook);
            BookService.SaveCurrentBook(_bookStore);

            OnInfoUpdated("Book details updated");

            _bookDetailsNavigationService.Navigate();
        }
    }
}
