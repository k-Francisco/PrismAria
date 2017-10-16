using Xamarin.Forms;

namespace PrismAria.Views
{
    public partial class BandSongsAndAlbumsPage : ContentPage
    {
        public BandSongsAndAlbumsPage()
        {
            InitializeComponent();
            if (Device.RuntimePlatform.Equals(Device.iOS))
                this.Title = "Songs and Albums";
        }
    }
}
