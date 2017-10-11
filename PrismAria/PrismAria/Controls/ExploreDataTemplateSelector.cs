using PrismAria.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PrismAria.Controls
{
    public class ExploreDataTemplateSelector : DataTemplateSelector
    {

        public DataTemplate TopBandsTemplate { get; set; }
        public DataTemplate OtherBandsTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((DiscoverPageModel)item).isTop ? TopBandsTemplate : OtherBandsTemplate;
        }
    }
}
