using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using PrismAria.Controls;
using PrismAria.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(AudioSlider), typeof(AudioSliderRenderer))]
namespace PrismAria.iOS.CustomRenderers
{
    public class AudioSliderRenderer : SliderRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.MinimumTrackTintColor = UIColor.White;
                Control.MaximumTrackTintColor = UIColor.FromRGBA(173, 174, 178, 40);

                if (e.NewElement != null && (e.NewElement as AudioSlider).HasThumb)
                {
                    Control.SetThumbImage(UIImage.FromBundle("small_slider"), UIControlState.Normal);
                }
                else
                {
                    Control.SetThumbImage(new UIImage(), UIControlState.Normal);
                }
            }
        }
    }
}