using PrismAria.PopupPages;
using Xamarin.Forms;

namespace PrismAria.Views
{
    public partial class BandLandingPage : TabbedPage
    {
        public BandLandingPage()
        {
            InitializeComponent();
            if (Device.RuntimePlatform.Equals(Device.iOS))
            {
                this.Children.Add(new UserPopupPage(false) { Icon = "ic_user.png", Title = "Profile" });
            }

            this.Title = this.CurrentPage.AutomationId;
            this.CurrentPageChanged += (sender, e) =>
            {
                this.Title = this.CurrentPage.AutomationId;
            };
        }
    }
}
