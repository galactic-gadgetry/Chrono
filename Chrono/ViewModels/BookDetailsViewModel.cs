using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Chrono.Commands;
using Chrono.Models;
using Chrono.Services;
using Chrono.Stores;
using Chrono.Utilities;

namespace Chrono.ViewModels
{
    class BookDetailsViewModel : DefaultViewModelBase
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
        /// Used to navigate to the Start Screen view.
        /// </summary>
        private readonly INavigate _startScreenNavigationService;


        /// <summary>
        /// The app's current book.
        /// </summary>
        public Book CurrentBook =>
            _bookStore.CurrentBook;


        /// <summary>
        /// Executed when the Archive button is clicked.
        /// </summary>
        public ICommand ArchiveButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Delete button is clicked.
        /// </summary>
        public ICommand DeleteButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Edit button is clicked.
        /// </summary>
        public ICommand EditButtonClickedCommand { get; }



        public BookDetailsViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            ArchiveButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnArchiveButtonClicked));
            DeleteButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnDeleteButtonClicked));
            EditButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnEditButtonClicked));

            _startScreenNavigationService =
                ServiceFactory.CreateNavigationService(
                    "start screen", _bookStore, _navigationStore);
        }


        /// <summary>
        /// Deletes the current book and navigates to the Start
        /// Screen view.
        /// </summary>
        private void DeleteBookRequested()
        {
            BookService.DeleteCurrentBook(_bookStore);

            OnInfoUpdated("Log book deleted");

            _startScreenNavigationService.Navigate();
        }


        private void OnArchiveButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles the Delete button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnDeleteButtonClicked(object? obj)
        {
            bool result =
                DialogService.PromptUserWithDeleteConfirmationMessage(_bookStore);

            if (result)
            {
                DeleteBookRequested();
            }
        }


        private void OnEditButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}
