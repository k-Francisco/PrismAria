using Prism;
using Prism.Events;
using Prism.Navigation;
using PrismAria.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismAria.ViewModels
{
    public class ChildViewBaseModel : BaseViewModel, IActiveAware, INavigatingAware, IDestructible
    {
        private readonly IEventAggregator _ea;
        public event EventHandler IsActiveChanged;

        public ChildViewBaseModel(IEventAggregator ea) {
            _ea = ea;
            _ea.GetEvent<InitializedTabbedChildrenEvent>().Subscribe(OnInitializationEventFired);

        }

        private void OnInitializationEventFired(NavigationParameters obj)
        {
            
        }

        private bool _isActive;
        public bool IsActive {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value); }
        }

        public override void Destroy()
        {
            _ea.GetEvent<InitializedTabbedChildrenEvent>().Unsubscribe(OnInitializationEventFired);
        }

    }
}
