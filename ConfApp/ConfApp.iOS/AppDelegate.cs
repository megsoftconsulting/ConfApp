using System;
using System.Threading.Tasks;
using ConfApp.iOS.Services;
using ConfApp.Services.Telemetry;
using ConfApp.Services.Telemetry.Events;
using FFImageLoading.Forms.Platform;
using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using ObjCRuntime;
using UIKit;
using UrbanAirship;
using UserNotifications;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace ConfApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate, IUARegistrationDelegate
    {
        private IAnalyticsService _analyticsService;
        private iOSBackgroundTask _task;
        private PushHandler PushHandler { get; set; }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            ConfigureUnhandledErrorHandling(app, options);
            Forms.Init();
            InitializeAzureNotificationHub();
            InitializeAirship();
            InitializeAppCenter(app, options);
            InitializeControls(app, options);

            var application = new App(new iOSInitializer());

            _analyticsService = application
                .Container
                .Resolve(typeof(IAnalyticsService)) as IAnalyticsService;

            _analyticsService?.TrackEvent(new EventBase("Application Launched Successfully."));

            LoadApplication(application);
            return base.FinishedLaunching(app, options);
        }

        private void InitializeAzureNotificationHub()
        {
            UNUserNotificationCenter
                .Current
                .RequestAuthorization(
                    UNAuthorizationOptions.Alert |
                    UNAuthorizationOptions.Badge |
                    UNAuthorizationOptions.Sound,
                    (granted, error) =>
                        InvokeOnMainThread(UIApplication.SharedApplication.RegisterForRemoteNotifications));
        }

        private void InitializeAirship()
        {
            // Set log level for debugging config loading (optional)
            // It will be set to the value in the loaded config upon takeOff
            UAirship.SetLogLevel(UALogLevel.Error);

            // Populate AirshipConfig.plist with your app's info from https://go.urbanairship.com
            // or set runtime properties here.
            var config = UAConfig.Config();
            // ConfApp Development
            config.DevelopmentAppKey = "sXNOJQIMSnuIi6lkARClIA";
            config.DevelopmentAppSecret = "9X09Cc0ESxiZbAqZE98glA";
            // config.DetectProvisioningMode = true;
            config.RequestAuthorizationToUseNotifications = true;
            config.ClearNamedUserOnAppRestore = true;
            config.DevelopmentLogLevel = UALogLevel.Error;
            // SalesHub2 QA Live
            //config.DefaultAppSecret = "M3YRh3aKTYaYkk_gRFilig";
            //config.DefaultAppKey = "YW1Rm20KQBm1baBu1ZlCKg";

            if (!config.Validate())
                throw new RuntimeException("The AirshipConfig.plist must be a part of the app bundle and " +
                                           "include a valid appkey and secret for the selected production level.");

            //WarnIfSimulator();

            // Bootstrap the Airship SDK
            UAirship.TakeOff(config);

            //Console.WriteLine("Config:{0}", config);

            UAirship.NamedUser().Identifier = "67004006";
            //UAirship.NamedUser().AddTags(new string[] { "non-prod" }, "test_group");
            //UAirship.NamedUser().ForceUpdate();
            Console.WriteLine($"NamedUser: {UAirship.NamedUser().Identifier}");
            Console.WriteLine($"Channel ID: {UAirship.Channel().Identifier}");


            PushHandler = new PushHandler();
            UAirship.Push().PushNotificationDelegate = PushHandler;
            UAirship.Push().ResetBadge();
            UAirship.Push().WeakRegistrationDelegate = this;
        }


        [Export("registrationSucceededForChannelID:deviceToken:")]
        public void RegistrationSucceeded(string channelId, string deviceToken)
        {
            AzurePushNotificationsHelper.RegisterForApplePushNotifications(NSData.FromString(deviceToken));
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Console.WriteLine("RegisteredForRemoteNotifications Called");
            // Called Azure Notification Hub Register Native
        }

        [Export("application:didReceiveLocalNotification:")]
        public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
        {
            Console.WriteLine("AppDelegate.ReceivedLocalNotification");
        }

        [Export("application:didRegisterForRemoteNotificationsWithDeviceToken:")]
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo,
            Action<UIBackgroundFetchResult> completionHandler)
        {
            Console.WriteLine("AppDelegate.DidReceiveRemoteNotification");
            //ProcessNotification(userInfo, false);
            completionHandler(UIBackgroundFetchResult.NoData);
        }

        private void ProcessNotification(NSDictionary options, bool fromFinishedLaunching)
        {
            if (null != options && options.ContainsKey(new NSString("aps")))
                ProcessForDemo(options, fromFinishedLaunching);

            if (options != null && options.ContainsKey(new NSString("message-type")))
                ProcessComplex(options, fromFinishedLaunching);
        }

        private void ProcessComplex(NSDictionary options, bool fromFinishedLaunching)
        {
            if (options != null && options.ContainsKey(new NSString("message-type")))
            {
                var type = options.ObjectForKey(new NSString("message-type"));
                var data = options["message-data"];
            }
        }

        private void ProcessForDemo(NSDictionary options, bool fromFinishedLaunching)
        {
            if (null != options && options.ContainsKey(new NSString("aps")))
            {
                //Get the aps dictionary
                var aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;

                var alert = string.Empty;

                //Extract the alert text
                // NOTE: If you're using the simple alert by just specifying
                // "  aps:{alert:"alert msg here"}  ", this will work fine.
                // But if you're using a complex alert with Localization keys, etc.,
                // your "alert" object from the aps dictionary will be another NSDictionary.
                // Basically the JSON gets dumped right into a NSDictionary,
                // so keep that in mind.
                //
                try
                {
                    if (aps.ContainsKey(new NSString("alert")))
                        alert = (aps["alert"] as NSString).ToString();

                    //If this came from the ReceivedRemoteNotification while the app was running,
                    // we of course need to manually process things like the sound, badge, and alert.
                    if (!fromFinishedLaunching)
                        //Manually show an alert
                        if (!string.IsNullOrEmpty(alert))
                            ShowAlertDialog(alert);
                }
                catch (Exception)
                {
                }
            }
        }

        private static void ShowAlertDialog(string alert)
        {
            var myAlert = UIAlertController.Create("Message Received", alert, UIAlertControllerStyle.Alert);
            myAlert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Destructive, null));
            UIApplication.SharedApplication?.KeyWindow?.RootViewController?.PresentViewController(
                myAlert,
                true, null);
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

        private void InitializeAppCenter(UIApplication app, NSDictionary options)
        {
            AppCenter.Start("b99fa140-bd1f-4068-a3b7-4674fd6db65b",
                typeof(Analytics), typeof(Crashes));

            // AppD
            // FireBase
            // MixPanel
            // Send it to Blog
            // EventHubs

            AppCenterLog.Verbose("LifeCycle", "InitializeAppCenter");
        }

        private void ConfigureUnhandledErrorHandling(UIApplication app, NSDictionary options)
        {
            AppDomain.CurrentDomain.UnhandledException += OnGlobalUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
        }

        private void OnGlobalUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }

        private void InitializeControls(UIApplication app, NSDictionary options)
        {
            //FormsGoogleMaps.Init("AIzaSyBBmi8wp1mAFCjlVP_XuaTpPNgmrWZLnhE");
            FormsMaps.Init();
            CachedImageRenderer.Init();
            CachedImageRenderer.InitImageSourceHandler();
        }
    }
}