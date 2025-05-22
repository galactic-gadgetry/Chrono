using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chrono.Services;
using Chrono.Stores;
using Chrono.Utilities;

namespace Chrono.ViewModels
{
    class LayoutViewModel : ViewModelBase
    {
        // Backing Fields
        private string infoText = string.Empty;


        /// <summary>
        /// Used to manage the app's current book.
        /// </summary>
        private readonly BookStore _bookStore;

        /// <summary>
        /// Used to navigate to the Navigation Bar UI component.
        /// </summary>
        private readonly INavigate _navBarNavigationService;

        /// <summary>
        /// Used to manage the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;


        /// <summary>
        /// The layout's current content view-model.
        /// </summary>
        public ViewModelBase? CurrentContentViewModel =>
            _navigationStore.CurrentLayoutContentViewModel;

        /// <summary>
        /// The layout's current navigation bar view-model.
        /// </summary>
        public ViewModelBase? CurrentNavigationBarViewModel =>
            _navigationStore.CurrentNavigationBarViewModel;

        /// <summary>
        /// Text for the info bar label.
        /// </summary>
        public string InfoText
        {
            get => infoText;
            set
            {
                infoText = value;
                OnPropertyChanged(nameof(InfoText));
            }
        }



        public LayoutViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            _navBarNavigationService =
                ServiceFactory.CreateNavigationService(
                    "nav bar", _bookStore, _navigationStore);

            _navigationStore.CurrentLayoutContentViewModelChanged +=
                OnCurrentContentViewModelChanged;
            _navigationStore.CurrentNavigationBarViewModelChanged +=
                OnCurrentNavigationBarViewModelChanged;
            _navigationStore.InfoUpdated += OnInfoUpdated;
        }



        private void NavigateNavBar()
        {
            _navBarNavigationService.Navigate();
        }

        /// <summary>
        /// Handles the navigation store's
        /// <see cref="NavigationStore.CurrentLayoutContentViewModelChanged"/>
        /// event.
        /// </summary>
        private void OnCurrentContentViewModelChanged()
        {
            if (CurrentContentViewModel is DefaultViewModelBase &&
                CurrentNavigationBarViewModel is not NavigationBarViewModel)
            {
                NavigateNavBar();
            }
            else if (CurrentContentViewModel is HybridViewModelBase &&
                _bookStore.CurrentBook.IsBookVoid == false)
            {
                // This should have the nav bar loaded.
                throw new NotImplementedException();
            }

                OnPropertyChanged(nameof(CurrentContentViewModel));
        }

        /// <summary>
        /// Handles the navigation store's
        /// <see cref="NavigationStore.CurrentNavigationBarViewModelChanged"/>
        /// event.
        /// </summary>
        private void OnCurrentNavigationBarViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentNavigationBarViewModel));
        }


        private void OnInfoUpdated(object? sender, string info)
        {
            InfoText = info;
        }
    }
}
