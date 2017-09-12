using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace PrismAria.Models
{
    public class DiscoverPageModel
    {
        public string categoryName { get; set; }
        public List<BandModel> bandList { get; set; }

    }

    public class BandModel {

        

        public string imgSource { get; set; }
        public string bandName { get; set; }
        //private DelegateCommand _bandClickCommand;
        //public DelegateCommand BandClickCommand =>
        //    _bandClickCommand ?? (_bandClickCommand = new DelegateCommand(BandClick));

        //private void BandClick()
        //{
        //    Debug.WriteLine(this.bandName);
        //}

        public DelegateCommand<BandModel> BandClick { get; set; }
    }
}
