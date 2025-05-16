using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chrono.ViewModels
{
    class MainViewModel : ViewModelBase
    {

        public ViewModelBase? CurrentContentViewModel { get; set; } = new StartScreenViewModel();
    }
}
