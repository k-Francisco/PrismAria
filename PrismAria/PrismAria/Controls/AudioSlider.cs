using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PrismAria.Controls
{
    public class AudioSlider : Slider
    {
        public static readonly BindableProperty HasThumbProperty =
            BindableProperty.Create(nameof(HasThumb), typeof(bool), typeof(HorizontalListView), true);

        public bool HasThumb
        {
            get { return (bool)GetValue(HasThumbProperty); }
            set { SetValue(HasThumbProperty, value); }
        }
    }
}
