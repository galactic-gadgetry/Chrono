using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
