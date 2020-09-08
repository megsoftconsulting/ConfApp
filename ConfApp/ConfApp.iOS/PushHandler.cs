//using System;
//using Foundation;
//using UIKit;
//using UrbanAirship;
//using UserNotifications;

//namespace ConfApp.iOS
//{
//    public class PushHandler : UAPushNotificationDelegate
//    {
       
//        public override void ReceivedBackgroundNotification(UANotificationContent notificationContent,
//            Action<UIBackgroundFetchResult> completionHandler)
//        {
//            Console.WriteLine("The application received a background notification");
          
           
//            completionHandler(UIBackgroundFetchResult.NoData);
//        }

//        public override void ReceivedForegroundNotification(UANotificationContent notificationContent,
//            Action completionHandler)
//        {
//            // Application received a foreground notification
//            Console.WriteLine("The application received a foreground notification");

//            // iOS 10 - let foreground presentations options handle it
//            if (NSProcessInfo.ProcessInfo.IsOperatingSystemAtLeastVersion(new NSOperatingSystemVersion(10, 0, 0)))
//            {
//                completionHandler();
//                return;
//            }

//            var alertController = UIAlertController.Create(notificationContent.AlertTitle,
//                notificationContent.AlertBody,
//                UIAlertControllerStyle.Alert);

//            var okAction = UIAlertAction.Create("OK", UIAlertActionStyle.Default, action => { });

//            alertController.AddAction(okAction);

//            var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;

//            alertController.PopoverPresentationController.SourceView = topController.View;

//            topController.PresentViewController(alertController, true, null);

//            completionHandler();
//        }

//        public override void ReceivedNotificationResponse(UANotificationResponse notificationResponse,
//            Action completionHandler)
//        {
//            Console.WriteLine("The user selected the following action identifier::{0}",
//                notificationResponse.ActionIdentifier);

//            var notificationContent = notificationResponse.NotificationContent;

//            var message = string.Format("Action Identifier:{0}", notificationResponse.ActionIdentifier);
//            var alertBody = notificationContent.AlertBody;

//            if (alertBody.Length > 0) message += string.Format("\nAlert Body:\n{0}", alertBody);

//            var responseText = notificationResponse.ResponseText;

//            if (responseText != null) message += string.Format("\nResponse:\n{0}", responseText);

//            var alertController = UIAlertController.Create(notificationContent.AlertTitle,
//                alertBody,
//                UIAlertControllerStyle.Alert);

//            var okAction = UIAlertAction.Create("OK", UIAlertActionStyle.Default, null);
//            alertController.AddAction(okAction);

//            var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;
//            if (alertController.PopoverPresentationController != null)
//                alertController.PopoverPresentationController.SourceView = topController.View;

//            topController.PresentViewController(alertController, true, null);

//            completionHandler();
//        }

//        public override UNNotificationPresentationOptions ExtendPresentationOptions(
//            UNNotificationPresentationOptions options, UNNotification notification)
//        {
//            return options | UNNotificationPresentationOptions.Alert | UNNotificationPresentationOptions.Sound | UNNotificationPresentationOptions.Badge;
//        }
//    }
//}