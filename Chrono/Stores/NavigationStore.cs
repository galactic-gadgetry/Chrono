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



        private void OnCurrentLayoutContentViewModelChanged()
        {
            CurrentLayoutContentViewModelChanged?.Invoke();
        }


        private void OnCurrentMainContentViewModelChanged()
        {
            CurrentMainContentViewModelChanged?.Invoke();
        }


        private void OnCurrentNavigationBarViewModelChanged()
        {
            CurrentNavigationBarViewModelChanged?.Invoke();
        }
    }
}
