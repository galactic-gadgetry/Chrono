using System.Configuration;
using System.Data;
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
            _bookStore = StoreFactory.CreateBookStore();
            _navigationStore = StoreFactory.CreateNavigationStore();
        }



        protected override void OnStartup(StartupEventArgs e)
        {
            // This should be replaced with a conditional statement
            // to navigate to the appropriate views depending on the
            // book store state.
            INavigate _startScreenNavigationService =
                ServiceFactory.CreateNavigationService(
                    "start screen", _bookStore, _navigationStore);
            _startScreenNavigationService.Navigate();

            MainViewModel mainViewModel = new(_bookStore, _navigationStore);
            MainWindow = new MainView()
            {
                DataContext = mainViewModel,
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
