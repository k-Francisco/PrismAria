using Xamarin.Forms;

namespace PrismAria.Views
{
    public partial class BandStatisticsPage : ContentPage
    {
        public BandStatisticsPage()
        {
            InitializeComponent();
            if (Device.RuntimePlatform.Equals(Device.iOS))
                this.Title = "Statistics";
        }
    }
}
