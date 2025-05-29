using Chrono.Commands;
using Chrono.Models;
using Chrono.Services;
using Chrono.Stores;
using Chrono.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chrono.ViewModels
{
    class ProjectsViewModel : DefaultViewModelBase
    {
        /// <summary>
        /// Used to manage the app's current book.
        /// </summary>
        private readonly BookStore _bookStore;

        /// <summary>
        /// Used to navigate to the Create New Project view.
        /// </summary>
        private readonly INavigate _createNewProjectNavigationService;

        /// <summary>
        /// Used to manage the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;


        // Backing Fields
        private string activeProjectsCountString = string.Empty;
        private string archivedProjectsCountString = string.Empty;
        private Project? selectedProject;


        /// <summary>
        /// Text for the Active project details label.
        /// </summary>
        public string ActiveProjectsCountString
        {
            get => activeProjectsCountString;
            set
            {
                activeProjectsCountString = value;
                OnPropertyChanged(nameof(ActiveProjectsCountString));
            }
        }

        /// <summary>
        /// Text for the Archived project details label.
        /// </summary>
        public string ArchivedProjectsCountString
        {
            get => archivedProjectsCountString;
            set
            {
                archivedProjectsCountString = value;
                OnPropertyChanged(nameof(ArchivedProjectsCountString));
            }
        }

        /// <summary>
        /// Returns the current book's
        /// <see cref="Book.Projects"/> collection.
        /// </summary>
        public ObservableCollection<Project> Projects =>
         _bookStore.CurrentBook.Projects;

        /// <summary>
        /// Projects list view selected project.
        /// </summary>
        public Project? SelectedProject
        {
            get => selectedProject;
            set
            {
                selectedProject = value;
                OnPropertyChanged(nameof(SelectedProject));
                OnPropertyChanged(nameof(SelectedProjectCreatedDateString));
            }
        }

        /// <summary>
        /// Text for the Created on project details label.
        /// </summary>
        public string SelectedProjectCreatedDateString =>
            $"Created on {SelectedProject?.CreatedDateString}";



        /// <summary>
        /// Executed when the New Project button is clicked.
        /// </summary>
        public ICommand NewProjectButtonClickedCommand { get; }



        public ProjectsViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            InitializeProjectDetails();

            NewProjectButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnNewProjectButtonClicked));

            _createNewProjectNavigationService =
                ServiceFactory.CreateNavigationService(
                    "create new project", _bookStore, _navigationStore);
        }


        /// <summary>
        /// Initializes the strings of the selected project details.
        /// </summary>
        private void InitializeProjectDetails()
        {
            int activeProjectsCount = Projects.Count(p => p.Status == ProjectStatus.Active);
            int archivedProjectsCount = Projects.Count(p => p.Status == ProjectStatus.Archived);

            ActiveProjectsCountString = $"Active: {activeProjectsCount}";
            ArchivedProjectsCountString = $"Archived: {archivedProjectsCount}";
        }

        /// <summary>
        /// Handles the New Project button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnNewProjectButtonClicked(object? obj)
        {
            _createNewProjectNavigationService.Navigate();
        }
    }
}
