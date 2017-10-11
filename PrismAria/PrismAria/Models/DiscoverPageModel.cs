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
        public bool isTop { get; set; }

    }

    public class BandModel {
        
        public string imgSource { get; set; }
        public string bandName { get; set; }

    }
}
