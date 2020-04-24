using System;
using Foundation;
using UIKit;

namespace PrismHub.iOS
{
    public class FinishedLaunchingDelegate
    {
        private WeakReference<UIApplicationDelegate> _appDelegate;

        public FinishedLaunchingDelegate(UIApplicationDelegate appDelegate)
        {
            _appDelegate = new WeakReference<UIApplicationDelegate>(appDelegate);
        }

        public FinishedLaunchingDelegate ConfigureUnhandledErrorHandling(UIApplication app, NSDictionary options)
        {
            // Here is where you would setup the CrashReporting and Global Unhandled Error Handling. 
            return this;
        }

        public FinishedLaunchingDelegate InitializeBeforeXamarinForms(UIApplication app, NSDictionary options)
        {
            // Initialize and start anything needed to happen before Xamarin Forms is Initialized.
            return this;
        }

        public FinishedLaunchingDelegate InitializeControls(UIApplication app, NSDictionary options)
        {
            // Initialize controls and libraries (e.g.: Xamarin Essentials, Shiny, etc).
            return this;
        }
    }
}