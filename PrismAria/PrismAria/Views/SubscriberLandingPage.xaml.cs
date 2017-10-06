using PrismAria.PopupPages;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PrismAria.Views
{
    public partial class SubscriberLandingPage : TabbedPage
    {
        public SubscriberLandingPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform.Equals(Device.iOS))
            {
                this.Children.Add(new UserPopupPage() { Icon="ic_user.png", Title="Profile"});
            }

            this.Title = this.CurrentPage.AutomationId;
            this.CurrentPageChanged += (sender, e) =>
            {
                this.Title = this.CurrentPage.AutomationId;
            };

        }
    }
}
