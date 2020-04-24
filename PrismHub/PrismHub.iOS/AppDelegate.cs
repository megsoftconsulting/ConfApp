using System;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace PrismHub.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        private readonly WeakReference<FinishedLaunchingDelegate> _finishedLaunchingDelegate;

        public AppDelegate()
        {
            _finishedLaunchingDelegate =
                new WeakReference<FinishedLaunchingDelegate>(new FinishedLaunchingDelegate(this));
        }


        protected FinishedLaunchingDelegate Delegate
        {
            get
            {
                _finishedLaunchingDelegate.TryGetTarget(out var d);
                return d;
            }
        }

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Delegate
                .InitializeBeforeXamarinForms(app, options)
                .ConfigureUnhandledErrorHandling(app, options);

            Forms.Init();
            Delegate.InitializeControls(app, options);
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }
}