using Xamarin.Forms;

namespace PrismAria.Views
{
    public partial class BandArticlesPage : ContentPage
    {
        public BandArticlesPage()
        {
            InitializeComponent();
            if (Device.RuntimePlatform.Equals(Device.iOS))
                this.Title = "Articles";
        }
    }
}
