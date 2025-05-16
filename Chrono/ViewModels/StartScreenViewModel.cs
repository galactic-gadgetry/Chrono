using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Chrono.Commands;

namespace Chrono.ViewModels
{
    class StartScreenViewModel : ViewModelBase
    {

        public string WelcomeMessageText
        {
            get
            {
                TimeSpan currentTime = DateTime.Now.TimeOfDay;
                TimeSpan noon = new(12, 0, 0);
                TimeSpan fivePM = new(17, 0, 0);
                if (currentTime < noon)
                {
                    return "Good morning";
                }
                else if (currentTime > noon && currentTime < fivePM)
                {
                    return "Good afternoon";
                }
                else
                {
                    return "Good evening";
                }
            }
        }



        public ICommand CreateNewLogBookButtonClickedCommand { get; }


        public ICommand OpenExistingLogBookButtonClickedCommand { get; }



        public StartScreenViewModel()
        {
            CreateNewLogBookButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnCreateNewLogBookButtonClicked));
            OpenExistingLogBookButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnOpenExistingLogBookButtonClicked));
        }



        private void OnCreateNewLogBookButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }


        private void OnOpenExistingLogBookButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}
