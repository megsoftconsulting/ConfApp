using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WindowsAzure.Messaging;
using ConfApp.iOS.Services;
using ConfApp.Services.Telemetry;
using ConfApp.Services.Telemetry.Events;
using FFImageLoading.Forms.Platform;
using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using UIKit;
using UserNotifications;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ObjCRuntime;

namespace ConfApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        // Azure app-specific connection string and hub path
        public const string ListenConnectionString =
            "Endpoint=sb://confapp.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=qla3ELzJz9Y/FFi9XHReIHiIVA9rDzIpO8dSnkGmEcU=";

        public const string NotificationHubName = "confapp";

        private IAnalyticsService _analyticsService;

        //private PushHandler _pushHandler;
        private iOSBackgroundTask _task;
        private SBNotificationHub Hub { get; set; }

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            InitializeAzureNotificationHub();
            // InitializeAirship();
            InitializeBeforeXamarinForms(app, options);
            ConfigureUnhandledErrorHandling(app, options);
            Forms.Init();
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

        //private void InitializeAirship()
        //{
        //    // Set log level for debugging config loading (optional)
        //    // It will be set to the value in the loaded config upon takeOff
        //    UAirship.SetLogLevel(UALogLevel.Trace);

        //    // Populate AirshipConfig.plist with your app's info from https://go.urbanairship.com
        //    // or set runtime properties here.
        //    var config = UAConfig.DefaultConfig();
        //    config.DefaultAppSecret = "M3YRh3aKTYaYkk_gRFilig";
        //    config.DefaultAppKey = "YW1Rm20KQBm1baBu1ZlCKg";

        //    config.ProductionAppKey = config.DefaultAppKey;
        //    config.ProductionAppSecret = config.DefaultAppSecret;
        //    config.RequestAuthorizationToUseNotifications = true;

        //    if (!config.Validate())
        //        throw new RuntimeException("The AirshipConfig.plist must be a part of the app bundle and " +
        //                                   "include a valid appkey and secret for the selected production level.");

        //    //WarnIfSimulator();

        //    // Bootstrap the Airship SDK
        //    UAirship.TakeOff(config);

        //    Console.WriteLine("Config:{0}", config);

        //    UAirship.Push().ResetBadge();
        //    UAirship.Push().UserPushNotificationsEnabled = true;
        //    UAirship.NamedUser().Identifier = "67004006";

        //    _pushHandler = new PushHandler();
        //    UAirship.Push().PushNotificationDelegate = _pushHandler;


        //    UAirship.Push().WeakRegistrationDelegate = this;

        //    //NSNotificationCenter.DefaultCenter.AddObserver(new NSString("channelIDUpdated"), notification =>
        //    //{
        //    //    //FIXME: Find a way to call the refreshView from the HomeViewController
        //    //});
        //}

        //[Export("registrationSucceededForChannelID:deviceToken:")]
        //public void RegistrationSucceeded(string channelID, string deviceToken)
        //{
        //    Console.Write($"RegistrationSucceeded channel ID:{channelID}, deviceToken: {deviceToken}");
        //    UAirship.NamedUser().Identifier = "67004006";
        //    // NSNotificationCenter.DefaultCenter.PostNotificationName("channelIDUpdated", this);
        //}

        ////[ProtocolMember(IsProperty = false, IsRequired = false, IsStatic = false, Name = "ApnsRegistrationSucceeded", ParameterByRef = new bool[] { false }, ParameterType = new Type[] { typeof() }, Selector = "apnsRegistrationSucceededWithDeviceToken:")]
        ////[Export("apnsRegistrationSucceededWithDeviceToken:")]
        //public void ApnsRegistrationSucceeded(NSData deviceToken)
        //{
        //}

        //[Export("registrationFailed")]
        //public void RegistrationFailed()
        //{
        //}

        //[Export("apnsRegistrationFailedWithError:")]
        //public void ApnsRegistrationFailed(NSError error)
        //{
        //}

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Debug.WriteLine("RegisteredForRemoteNotifications Called");
            AzurePushNotificationsHelper.RegisterForApplePushNotifications(deviceToken, new string[] { "claudio" });
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            System.Diagnostics.Debug.WriteLine($"FailedToRegisterForRemoteNotifications {error}");
        }

        [Export("application:didRegisterUserNotificationSettings:")]
        public void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
        {
            System.Diagnostics.Debug.WriteLine("DidRegisterUserNotificationSettings called");
        }

        [Export("application:didReceiveLocalNotification:")]
        public void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
        {
            System.Diagnostics.Debug.WriteLine("ReceivedLocalNotification called");
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo,Action<UIBackgroundFetchResult> completionHandler)
        {
            Process(userInfo, false);
            completionHandler(UIBackgroundFetchResult.NoData);
        }

        public void Process(NSDictionary options, bool fromFinishedLaunching)
        {
            System.Diagnostics.Debug.WriteLine("Processing Push Notification data");
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