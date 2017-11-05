using Plugin.Connectivity;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions.EventArguments;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismAria.ViewModels
{
    public abstract class ChildViewBaseModel : BindableBase, IActiveAware, INavigatingAware, IDestructible
    {
        protected bool HasInitialized { get; set; }
        protected readonly INavigationService _navigationService;

        public event EventHandler IsActiveChanged;

        private bool _isActive;
        public bool IsActive {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value, RaiseIsActiveChanged); }
        }

        protected bool _isConnected = CrossConnectivity.Current.IsConnected;

        public DelegateCommand<string> NavigateCommand { get; set; }

        public virtual void Destroy()
        {
            
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
           
        }

        protected virtual void RaiseIsActiveChanged() {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }

        public ChildViewBaseModel(INavigationService navigationService) {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {
                _isConnected = args.IsConnected;
            };

            CrossMediaManager.Current.MediaFileChanged += (object sender, MediaFileChangedEventArgs e) =>
            {
                Singleton.Instance.MediaFileArgs = e;
            };
        }

        private async void Navigate(string obj)
        {
            await _navigationService.NavigateAsync(obj);
        }
    }
}
