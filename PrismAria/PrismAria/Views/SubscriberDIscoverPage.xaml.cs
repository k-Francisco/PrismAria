using Xamarin.Forms;

namespace PrismAria.Views
{
    public partial class SubscriberDIscoverPage : ContentPage
    {
        public SubscriberDIscoverPage()
        {
            InitializeComponent();
            if (Device.RuntimePlatform.Equals(Device.iOS))
            {
                this.Title = "Explore";
            }
           
        }
    }
}
