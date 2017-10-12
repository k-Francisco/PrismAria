using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PrismAria.Models
{
    public class UserBandModel
    {
        public ImageSource userBandImage { get; set; }
        public string userBandName { get; set; }
    }

    public class UserBandModelForEvent
    {
        public string userBandImage { get; set; }
        public string userBandName { get; set; }
        public string userBandRole { get; set; }
    }
}
