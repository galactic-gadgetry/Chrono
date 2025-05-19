using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Chrono.Commands;
using Chrono.Models;
using Chrono.Models.DTOs;
using Chrono.Services;
using Chrono.Stores;
using Chrono.Utilities;

namespace Chrono.ViewModels
{
    class CreateNewBookViewModel : ViewModelBase
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
        public bool CanCreateBook
        {
            get
            {
                return !string.IsNullOrEmpty(nameText);
            }
        }

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
                OnPropertyChanged(nameof(CanCreateBook));
            }
        }


        /// <summary>
        /// Executed when the Cancel button is clicked.
        /// </summary>
        public ICommand CancelButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Create button is clicked.
        /// </summary>
        public ICommand CreateButtonClickedCommand { get; }



        public CreateNewBookViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            CancelButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnCancelButtonClicked));
            CreateButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnCreateButtonClicked));

            _bookDetailsNavigationService =
                ServiceFactory.CreateNavigationService(
                    "book details", _bookStore, _navigationStore);
        }


        /// <summary>
        /// Uses information from the view to create a new
        /// <see cref="Book"/> instance.
        /// </summary>
        private void CreateNewBookRequested()
        {
            BookDTO dto = new() { Name = NameText };
            Book book = BookService.CreateNewCurrentBook(_bookStore, dto);
            BookService.SaveCurrentBook(_bookStore);

            // Update the info bar.
            OnInfoUpdated($"New book '{book.Name}' created");

            _bookDetailsNavigationService.Navigate();
        }

        
        private void OnCancelButtonClicked(object? obj)
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
        /// Handles the Create button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnCreateButtonClicked(object? obj)
        {
            CreateNewBookRequested();
        }
    }
}
