using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismAria.ViewModels
{
    public class SubscriberDIscoverPageViewModel : BindableBase
    {
        private string _sampleText = "Sample Text here";
        public string SampleText
        {
            get { return _sampleText; }
            set { SetProperty(ref _sampleText, value); }
        }

        public SubscriberDIscoverPageViewModel()
        {

        }
    }
}
