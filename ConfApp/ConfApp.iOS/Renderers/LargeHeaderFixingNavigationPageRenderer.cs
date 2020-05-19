using System.Diagnostics;
using ConfApp;
using ConfApp.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BigTitleNavigationPage), typeof(LargeHeaderFixingNavigationPageRenderer))]

namespace ConfApp.iOS.Renderers
{
    public class LargeHeaderFixingNavigationPageRenderer : NavigationRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            var body = UIKit.UIFont.PreferredBody;
            var title =
                UIKit.UIFont.PreferredTitle1;
            var b = UIFont.PreferredTitle2;
            var c = UIFont.PreferredTitle3;

            if (Element is NavigationPage navigationPage)
            {
                NavigationBar.PrefersLargeTitles = true;
                var barBackgroundColor = navigationPage.BarBackgroundColor;
                
                NavigationBar.StandardAppearance.BackgroundColor = barBackgroundColor == Color.Default
                    ? UINavigationBar.Appearance.BarTintColor
                    : barBackgroundColor.ToUIColor();
                //
                //var ttAttributes = NavigationBar.TitleTextAttributes;
                //if(ttAttributes == null) Debug.WriteLine("TitleTextAttributes is null");

                //var lttAttributes = NavigationBar.LargeTitleTextAttributes;
                //if (lttAttributes == null) Debug.WriteLine("LargeTitleTextAttributes is null");

                //var sa = NavigationBar.StandardAppearance;
                //if (sa == null) Debug.WriteLine("StandardAppearance is null");

                //NavigationBar.StandardAppearance.TitleTextAttributes = ttAttributes;
                //NavigationBar.StandardAppearance.LargeTitleTextAttributes = lttAttributes;
                //NavigationBar.ScrollEdgeAppearance = sa;
            }
        }
    }
}