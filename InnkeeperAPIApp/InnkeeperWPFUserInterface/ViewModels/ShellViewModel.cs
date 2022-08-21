using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnkeeperWPFUserInterface.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private LoginViewModel _loginVM;
        private HomeViewModel _homeVM;
        public ShellViewModel(HomeViewModel homeVM)
        {
            _homeVM = homeVM;
            ActivateItemAsync(_homeVM);
        }
    }
}
