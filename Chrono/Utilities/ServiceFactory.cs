using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chrono.Services;
using Chrono.Stores;
using Chrono.ViewModels;

namespace Chrono.Utilities
{
    class ServiceFactory
    {

        public static INavigate CreateNavigationService(string type,
            BookStore bookStore, NavigationStore navigationStore)
        {
            switch (type.ToLower())
            {
                case "book details":
                    return new LayoutNavigationService<BookDetailsViewModel>(
                        navigationStore,
                        () => new BookDetailsViewModel(bookStore, navigationStore));
                case "create new book":
                    return new LayoutNavigationService<CreateNewBookViewModel>(
                        navigationStore,
                        () => new CreateNewBookViewModel(bookStore, navigationStore));
                case "create new project":
                    return new LayoutNavigationService<CreateNewProjectViewModel>(
                        navigationStore,
                        () => new CreateNewProjectViewModel(bookStore, navigationStore));
                case "edit book details":
                    return new LayoutNavigationService<EditBookDetailsViewModel>(
                        navigationStore,
                        () => new EditBookDetailsViewModel(bookStore, navigationStore));
                case "layout":
                    return new NavigationService<LayoutViewModel>(
                        navigationStore,
                        () => new LayoutViewModel(bookStore, navigationStore));
                case "nav bar":
                    return new NavBarNavigationService<NavigationBarViewModel>(
                        navigationStore,
                        () => new NavigationBarViewModel(bookStore, navigationStore));
                case "projects":
                    return new LayoutNavigationService<ProjectsViewModel>(
                        navigationStore,
                        () => new ProjectsViewModel(bookStore, navigationStore));
                case "saved books":
                    return new LayoutNavigationService<SavedBooksViewModel>(
                        navigationStore,
                        () => new SavedBooksViewModel(bookStore, navigationStore));
                case "start screen":
                    return new NavigationService<StartScreenViewModel>(
                        navigationStore,
                        () => new StartScreenViewModel(bookStore, navigationStore));
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }
    }
}
