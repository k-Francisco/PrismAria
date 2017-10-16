using Xamarin.Forms;

namespace PrismAria.Views
{
    public partial class BandDetailsPage : ContentPage
    {
        public BandDetailsPage()
        {
            InitializeComponent();
            if (Device.RuntimePlatform.Equals(Device.iOS))
                this.Title = "Details";
        }
    }
}
