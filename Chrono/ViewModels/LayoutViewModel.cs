using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chrono.Stores;

namespace Chrono.ViewModels
{
    class LayoutViewModel : ViewModelBase
    {
        // Backing Fields
        private string infoText = string.Empty;


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



        public LayoutViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentLayoutContentViewModelChanged +=
                OnCurrentContentViewModelChanged;
            _navigationStore.CurrentNavigationBarViewModelChanged +=
                OnCurrentNavigationBarViewModelChanged;
            _navigationStore.InfoUpdated += OnInfoUpdated;
        }


        /// <summary>
        /// Handles the navigation store's
        /// <see cref="NavigationStore.CurrentLayoutContentViewModelChanged"/>
        /// event.
        /// </summary>
        private void OnCurrentContentViewModelChanged()
        {
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


        private void OnInfoUpdated(object sender, string info)
        {
            InfoText = info;
        }
    }
}
