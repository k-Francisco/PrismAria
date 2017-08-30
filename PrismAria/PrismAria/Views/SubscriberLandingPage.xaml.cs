using System.Threading.Tasks;
using Xamarin.Forms;

namespace PrismAria.Views
{
    public partial class SubscriberLandingPage : TabbedPage
    {
        public SubscriberLandingPage()
        {
            InitializeComponent();
            this.Title = this.CurrentPage.AutomationId;
            this.CurrentPageChanged += (sender, e) =>
            {
                this.Title = this.CurrentPage.AutomationId;
            };

        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //    await Task.Delay(2000);
        //    await Task.WhenAll(
        //        this.FadeTo(0,2000, Easing.BounceIn)
        //        );
        //}
    }
}
