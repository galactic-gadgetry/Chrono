using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chrono.ViewModels;

namespace Chrono.Stores
{
    class NavigationStore
    {
        // Backing Fields
        private ViewModelBase? currentLayoutContentViewModel;
        private ViewModelBase? currentMainContentViewModel;
        private ViewModelBase? currentNavigationBarViewModel;



        public ViewModelBase? CurrentLayoutContentViewModel
        {
            get => currentLayoutContentViewModel;
            set
            {
                currentLayoutContentViewModel = value;
                OnCurrentLayoutContentViewModelChanged();
            }
        }


        public ViewModelBase? CurrentMainContentViewModel
        {
            get => currentMainContentViewModel;
            set
            {
                currentMainContentViewModel = value;
                OnCurrentMainContentViewModelChanged();
            }
        }


        public ViewModelBase? CurrentNavigationBarViewModel
        {
            get => currentNavigationBarViewModel;
            set
            {
                currentNavigationBarViewModel = value;
                OnCurrentNavigationBarViewModelChanged();
            }
        }



        public Action? CurrentLayoutContentViewModelChanged;


        public Action? CurrentMainContentViewModelChanged;


        public Action? CurrentNavigationBarViewModelChanged;


        public event EventHandler<string>? InfoUpdated;



        private void OnCurrentLayoutContentViewModelChanged()
        {
            if (CurrentLayoutContentViewModel != null)
            {
                CurrentLayoutContentViewModel.InfoUpdated +=
                    OnInfoUpdated;
            }
            CurrentLayoutContentViewModelChanged?.Invoke();
        }


        private void OnCurrentMainContentViewModelChanged()
        {
            if (CurrentMainContentViewModel != null)
            {
                CurrentMainContentViewModel.InfoUpdated +=
                    OnInfoUpdated;
            }
            CurrentMainContentViewModelChanged?.Invoke();
        }


        private void OnCurrentNavigationBarViewModelChanged()
        {
            if (CurrentNavigationBarViewModel != null)
            {
                CurrentNavigationBarViewModel.InfoUpdated +=
                    OnInfoUpdated;
            }
            CurrentNavigationBarViewModelChanged?.Invoke();
        }



        private void OnInfoUpdated(object? sender, string info)
        {
            InfoUpdated?.Invoke(this, info);
        }
    }
}
