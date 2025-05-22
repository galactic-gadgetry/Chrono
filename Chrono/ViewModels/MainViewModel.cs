using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chrono.Services;
using Chrono.Stores;

namespace Chrono.ViewModels
{
    class MainViewModel : ViewModelBase
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
        /// The main view's current content view-model.
        /// </summary>
        public ViewModelBase? CurrentContentViewModel =>
            _navigationStore.CurrentMainContentViewModel;



        public MainViewModel(BookStore bookStore, NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            _navigationStore.CurrentMainContentViewModelChanged +=
                OnCurrentContentViewModelChanged;
        }


        /// <summary>
        /// Handles the window closing event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>True if the user wishes to close the window,
        /// false otherwise.</returns>
        public bool OnWindowClosing(object? sender, CancelEventArgs e)
        {
            // Save the app settings.
            SettingsService.SetAppSettings(_bookStore);

            // The book service's CloseCurrentBook method returns
            // true if the user wishes to continue closing the
            // window, false otherwise.
            return BookService.CloseCurrentBook(_bookStore);
        }


        /// <summary>
        /// Handles the navigation store's
        /// <see cref="NavigationStore.CurrentMainContentViewModelChanged"/>
        /// event.
        /// </summary>
        private void OnCurrentContentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentContentViewModel));
        }
    }
}
