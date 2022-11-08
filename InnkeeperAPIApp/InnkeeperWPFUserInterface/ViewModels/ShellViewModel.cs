using Caliburn.Micro;
using InnkeeperWPFUserInterface.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InnkeeperWPFUserInterface.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private HomeViewModel _homeVM;
        private IEventAggregator _events;

        public ShellViewModel(HomeViewModel homeVM, IEventAggregator events)
        {
            _homeVM = homeVM;
            _events = events;

            _events.SubscribeOnPublishedThread(this);
            ActivateItemAsync(IoC.Get<LoginViewModel>());
        }

        public Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            return ActivateItemAsync(_homeVM);
        }
    }
}
