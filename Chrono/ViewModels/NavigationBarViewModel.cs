using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Chrono.Commands;

namespace Chrono.ViewModels
{
    class NavigationBarViewModel : ViewModelBase
    {

        public ICommand CalculatorButtonClickedCommand { get; }


        public ICommand DailyTimeSheetButtonClickedCommand { get; }


        public ICommand LogBookButtonClickedCommand { get; }


        public ICommand ProjectsButtonClickedCommand { get; }



        public NavigationBarViewModel()
        {
            CalculatorButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnCalculatorButtonClicked));
            DailyTimeSheetButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnDailyTimeSheetButtonClicked));
            LogBookButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnLogBookButtonClicked));
            ProjectsButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnProjectsButtonClicked));
        }



        private void OnCalculatorButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }


        private void OnDailyTimeSheetButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }


        private void OnLogBookButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }


        private void OnProjectsButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}
