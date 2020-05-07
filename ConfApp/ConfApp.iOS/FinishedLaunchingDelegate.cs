using System;
using Foundation;
using MetalPerformanceShaders;
using UIKit;

namespace ConfApp.iOS
{
    public static class FinishedLaunchingDelegate
    {
        private static AppDelegate _app;
        public static void SetApp(AppDelegate app)
        {
            _app = app;
        }
        public static void ConfigureUnhandledErrorHandling(UIApplication app, NSDictionary options)
        {
            // Here is where you would setup the CrashReporting and Global Unhandled Error Handling. 
        }

        public static void InitializeBeforeXamarinForms(UIApplication app, NSDictionary options)
        {
            // Initialize and start anything needed to happen before Xamarin Forms is Initialized.
        }

        public static void InitializeControls(UIApplication app, NSDictionary options)
        {
            // Initialize controls and libraries (e.g.: Xamarin Essentials, Shiny, etc).
        }
    }
}