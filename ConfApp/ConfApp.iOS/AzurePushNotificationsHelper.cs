using System.Diagnostics;
using Foundation;
using WindowsAzure.Messaging;
namespace ConfApp.iOS
{
    public static class AzurePushNotificationsHelper
    {

        // Azure app-specific connection string and hub path
        public const string ListenConnectionString =
            "Endpoint=sb://confapp.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=qla3ELzJz9Y/FFi9XHReIHiIVA9rDzIpO8dSnkGmEcU=";

        public const string NotificationHubName = "confapp";

        private static SBNotificationHub Hub { get; set; }
        private static NSData DeviceToken;

        static AzurePushNotificationsHelper()
        {
            Hub = new SBNotificationHub(ListenConnectionString, NotificationHubName);
        }

        public static async System.Threading.Tasks.Task RegisterTags(string[] tags)
        {
            await Hub.RegisterNativeAsync(DeviceToken, new NSSet(tags));
        }

        public static void RegisterForApplePushNotifications(NSData deviceToken, string[] tags)
        {
            DeviceToken = deviceToken;

            Hub.UnregisterAll(deviceToken, error =>
            {
                if (error != null)
                {
                    Debug.WriteLine("Error calling Unregister: {0}", error.ToString());
                    return;
                }

                Hub.RegisterNative(deviceToken, new NSSet(tags), errorCallback =>
                {
                    if (errorCallback != null)
                        Debug.WriteLine("RegisterNativeAsync error: " + errorCallback);
                });
            });
        }




    }
}
