using System;
using Foundation;
using UIKit;
using UrbanAirship;
using UserNotifications;

namespace ConfApp.iOS
{
    public class PushHandler : UAPushNotificationDelegate
    {
        public override void ReceivedBackgroundNotification(UANotificationContent notificationContent,
            Action<UIBackgroundFetchResult> completionHandler)
        {
            Console.WriteLine("PushHandler.ReceivedBackgroundNotification - The application received a background notification");
        
            ProcessContent(notificationContent);

            completionHandler(UIBackgroundFetchResult.NoData);
        }

        private void ProcessContent(UANotificationContent n)
        {
            var aps = n.NotificationInfo;
            if (aps.ContainsKey(new NSString("aps"))) Console.WriteLine("Contains APS");
        }

        public override void ReceivedForegroundNotification(UANotificationContent notificationContent,
            Action completionHandler)
        {
           
            Console.WriteLine(
                "PushHandler.ReceivedForegroundNotification - The application received a foreground notification");
           
            
            ProcessContent(notificationContent);
            completionHandler();
        }

        public override UNNotificationPresentationOptions ExtendPresentationOptions(
            UNNotificationPresentationOptions options, UNNotification notification)
        {
            Console.WriteLine("PushHandler.ExtendPresentationOptions - Asking the App if it can present the Received Notification");

            var r=  options
                   | UNNotificationPresentationOptions.Alert
                   | UNNotificationPresentationOptions.Sound
                   | UNNotificationPresentationOptions.Badge;
            
            return r;
        }
    }
}