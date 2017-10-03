using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PrismAria.Controls
{
    public class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage(Page root) : base(root) {
            BarTextColor = Color.FromHex("#2C3E50");
        }
    }
}
