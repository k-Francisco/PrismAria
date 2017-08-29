using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismAria.ViewModels
{
    public class BaseViewModel : BindableBase, INavigatingAware, IDestructible
    {
        public virtual void Destroy()
        {
            
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}
