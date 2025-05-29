using Chrono.Commands;
using Chrono.Models;
using Chrono.Models.DTOs;
using Chrono.Services;
using Chrono.Stores;
using Chrono.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chrono.ViewModels
{
    class CreateNewProjectViewModel : DefaultViewModelBase
    {
        /// <summary>
        /// Used to manage the app's current book.
        /// </summary>
        private readonly BookStore _bookStore;

        /// <summary>
        /// Used to navigate to the Projects view.
        /// </summary>
        private readonly INavigate _projectsNavigationService;

        /// <summary>
        /// Used to manage the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;


        // Backing Fields
        private string codeText = string.Empty;
        private string nameText = string.Empty;
        private string wbsText = string.Empty;


        /// <summary>
        /// Returns true if the input fields are valid, false
        /// otherwise.
        /// </summary>
        public bool CanCreateProject
        {
            get
            {
                if (string.IsNullOrEmpty(codeText) ||
                    string.IsNullOrEmpty(nameText) ||
                    string.IsNullOrEmpty(wbsText))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Text for the Code text box.
        /// </summary>
        public string CodeText
        {
            get => codeText;
            set
            {
                codeText = value;
                OnPropertyChanged(nameof(CodeText));
                OnPropertyChanged(nameof(CanCreateProject));
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
                OnPropertyChanged(nameof(CanCreateProject));
            }
        }

        /// <summary>
        /// Text for the WBS text box.
        /// </summary>
        public string WbsText
        {
            get => wbsText;
            set
            {
                wbsText = value;
                OnPropertyChanged(nameof(WbsText));
                OnPropertyChanged(nameof(CanCreateProject));
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


        /// <summary>
        /// Initializes a new instance of the
        /// <seealso cref="CreateNewProjectViewModel"/> class.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="navigationStore"></param>
        public CreateNewProjectViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            CancelButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnCancelButtonClicked));
            CreateButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnCreateButtonClicked));

            _projectsNavigationService =
                ServiceFactory.CreateNavigationService(
                "projects", _bookStore, _navigationStore);
        }


        /// <summary>
        /// Creates a new project in the current book's
        /// <see cref="Book.Projects"/> collection.
        /// </summary>
        private void CreateNewProjectRequested()
        {
            // Create a new Project DTO from the input fields.
            ProjectDTO dto = new()
            {
                Code = CodeText,
                Name = NameText,
                Wbs = WbsText,
            };

            // If the project could not be created or added, display
            // and error message.
            (bool result, string? detail) =
                BookService.CreateNewProjectInCurrentBook(_bookStore, dto);
            if (result == false)
            {
                string caption = "Unable to Create Project";
                string message = $"The selected {detail} is " +
                    "assigned to another project. A project's " +
                    $"{detail} must be unique.";
                DialogService.PromptUserWithErrorMessageWithOkButton(
                    caption, message);
            }
            else
            {
                OnInfoUpdated($"New project '{dto.Name}' created");

                _projectsNavigationService.Navigate();
            }
        }

        /// <summary>
        /// Handles the Cancel button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnCancelButtonClicked(object? obj)
        {
            _projectsNavigationService.Navigate();
        }

        /// <summary>
        /// Handles the Create button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnCreateButtonClicked(object? obj)
        {
            CreateNewProjectRequested();
        }
    }
}
