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
        private SimpleContainer _container;

        public ShellViewModel(HomeViewModel homeVM, IEventAggregator events, SimpleContainer container)
        {
            _homeVM = homeVM;
            _events = events;
            _container = container;

            _events.SubscribeOnPublishedThread(this);
            ActivateItemAsync(_container.GetInstance<LoginViewModel>());
        }

        public Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            return ActivateItemAsync(_homeVM);
        }
    }
}
