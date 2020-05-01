using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace ConfApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            InitializeBeforeXamarinForms(app, options);
            ConfigureUnhandledErrorHandling(app, options);

            Forms.Init();
            InitializeControls(app, options);
            LoadApplication(new ConfApp.App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }

        private void InitializeBeforeXamarinForms(UIApplication app, NSDictionary options)
        {
        }

        private void ConfigureUnhandledErrorHandling(UIApplication app, NSDictionary options)
        {
        }

        private void InitializeControls(UIApplication app, NSDictionary options)
        {
        }
    }
}