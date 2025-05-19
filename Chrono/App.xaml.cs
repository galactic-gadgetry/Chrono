using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using Chrono.Services;
using Chrono.Stores;
using Chrono.Utilities;
using Chrono.ViewModels;
using Chrono.Views;

namespace Chrono
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private readonly BookStore _bookStore;


        private readonly NavigationStore _navigationStore;



        public App()
        {
            _bookStore = InitializeBookStore();
            _navigationStore = StoreFactory.GetNewNavigationStore();
        }



        protected override void OnStartup(StartupEventArgs e)
        {
            // If the book store's current book is void state,
            // navigate to the Start Screen, otherwise navigate to
            // the Book Details view.
            if (_bookStore.CurrentBook.IsBookVoid)
            {
                INavigate startScreenNavigationService =
                    ServiceFactory.CreateNavigationService(
                        "start screen", _bookStore, _navigationStore);
                startScreenNavigationService.Navigate();
            }
            else
            {
                INavigate bookDetailsNavigationService =
                    ServiceFactory.CreateNavigationService(
                        "book details", _bookStore, _navigationStore);
                INavigate layoutNavigationService =
                    ServiceFactory.CreateNavigationService(
                        "layout", _bookStore, _navigationStore);
                layoutNavigationService.Navigate();
                bookDetailsNavigationService.Navigate();
            }

                MainViewModel mainViewModel = new(_bookStore, _navigationStore);
            MainWindow = new MainView()
            {
                DataContext = mainViewModel,
            };
            MainWindow.Show();

            base.OnStartup(e);
        }



        private BookStore InitializeBookStore()
        {
            string? lastOpenBookFilePath =
                SettingsService.GetLastOpenBookFilePath();

            // If the settings value returned is null or the file
            // cannot be found, return a new book store.
            if (lastOpenBookFilePath == null || !File.Exists(lastOpenBookFilePath))
            {
                return StoreFactory.GetNewBookStore();
            }

            // Attempt to load the book store in the same state it
            // was in when the app last exited.
            return StoreFactory.LoadBookStoreFromFile(lastOpenBookFilePath);
        }
    }

}
