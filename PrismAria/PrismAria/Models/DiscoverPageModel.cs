using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
