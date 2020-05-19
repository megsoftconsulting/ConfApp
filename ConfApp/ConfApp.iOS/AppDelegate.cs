using System.Diagnostics;
using System.Threading.Tasks;
using ConfApp.iOS.Services;
using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using UIKit;
using Xamarin;
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
        private iOSBackgroundTask _task;

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
            FormsMaps.Init();
            InitializeControls(app, options);
            LoadApplication(new App(new iOSInitializer()));
            //StartBackgroundTasks();
            return base.FinishedLaunching(app, options);
        }

        public override void DidEnterBackground(UIApplication uiApplication)
        {
            //var id = UIApplication.SharedApplication.BeginBackgroundTask("DummyTask",
            //    () => { Debug.Write("DummyTask time is ending. ExpirationHandler being called by the OS."); });
            //UIApplication.SharedApplication.EndBackgroundTask(id);
            //base.DidEnterBackground(uiApplication);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication,
            NSObject annotation)
        {
            // return base.OpenUrl(application, url, sourceApplication, annotation);
            Debug.WriteLine("Opened URL was called");
            return true;
        }

        private async void StartBackgroundTasks()
        {
            _task = new iOSBackgroundTask();

            await _task.Start(c => Task.CompletedTask, () => { }, true);
            //{
            //    for (long i = 0; i < long.MaxValue; i++)
            //    {
            //        var time = UIApplication
            //            .SharedApplication.BackgroundTimeRemaining;
            //        Debug.WriteLine($"Background Task {task.TaskId}. Time remaining: {time}");
            //        token.ThrowIfCancellationRequested();
            //       await Task.Delay(1000);
            //    }
            //});
            //await task.Start();
        }

        private void InitializeBeforeXamarinForms(UIApplication app, NSDictionary options)
        {
            AppCenter.Start("b99fa140-bd1f-4068-a3b7-4674fd6db65b",
                typeof(Analytics), typeof(Crashes));

            // AppD
            // FireBase
            // MixPanel
            // Send it to Blog
            // EventHubs

            AppCenterLog.Verbose("LifeCycle", "InitializeBeforeXamarinForms");
        }

        private void ConfigureUnhandledErrorHandling(UIApplication app, NSDictionary options)
        {
        }

        private void InitializeControls(UIApplication app, NSDictionary options)
        {
            //FormsGoogleMaps.Init("AIzaSyBBmi8wp1mAFCjlVP_XuaTpPNgmrWZLnhE");
            FormsMaps.Init();
        }
    }
}