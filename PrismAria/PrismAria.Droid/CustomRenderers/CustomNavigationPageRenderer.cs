using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using PrismAria.Controls;
using PrismAria.Droid.CustomRenderers;
using Xamarin.Forms.Platform.Android.AppCompat;
using Android.Graphics;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationPageRenderer))]
namespace PrismAria.Droid.CustomRenderers
{
    public class CustomNavigationPageRenderer : NavigationPageRenderer
    {
        private Android.Support.V7.Widget.Toolbar toolbar;

        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);
            if(child.GetType() == typeof(Android.Support.V7.Widget.Toolbar))
            {
                toolbar = (Android.Support.V7.Widget.Toolbar)child;
                toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
            }
        }

        //protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    base.OnElementPropertyChanged(sender, e);

        //    var page = this.Element as NavigationPage;

        //    if (page != null && toolbar != null) {

        //        Typeface tf = Typeface.CreateFromAsset(Android.App.Application.Context.Assets, "BryantBoldAlt-Regular.otf");

        //        TextView title = (TextView)toolbar.FindViewById(Resource.Id.toolbar_title);
        //        title.SetText(page.CurrentPage.Title, TextView.BufferType.Normal);
        //        title.SetTypeface(tf, TypefaceStyle.Normal);
        //    }
        //}

        private void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        {
            var view = e.Child.GetType();
            if (e.Child.GetType() == typeof(Android.Widget.TextView))
            {

                var textView = (Android.Widget.TextView)e.Child;
                var font = Typeface.CreateFromAsset(Android.App.Application.Context.Assets, "BryantBoldAlt-Regular.otf");
                textView.Typeface = font;
                toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
        }
    }
}