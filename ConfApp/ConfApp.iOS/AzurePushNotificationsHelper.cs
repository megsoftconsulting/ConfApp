using System.Diagnostics;
using System.Threading.Tasks;
using WindowsAzure.Messaging;
using Foundation;

namespace ConfApp.iOS
{
    public static class AzurePushNotificationsHelper
    {
        // Azure app-specific connection string and hub path
        public const string ListenConnectionString =
            "Endpoint=sb://confapp.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=qla3ELzJz9Y/FFi9XHReIHiIVA9rDzIpO8dSnkGmEcU=";

        public const string NotificationHubName = "confapp";
        private static NSData _deviceToken;

        static AzurePushNotificationsHelper()
        {
            Hub = new SBNotificationHub(ListenConnectionString, NotificationHubName);
        }

        private static SBNotificationHub Hub { get; }

        public static async Task RegisterTags(string[] tags)
        {
            await Hub.RegisterNativeAsync(_deviceToken, new NSSet(tags));
        }

        public static void RegisterForApplePushNotifications(NSData deviceToken)
        {
            _deviceToken = deviceToken;
           
            Hub.UnregisterAll(deviceToken, error =>
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (error != null)
                {
                    Debug.WriteLine("Error calling Unregister: {0}", error.ToString());
                    return;
                }

                Hub.RegisterNative(deviceToken, null, errorCallback =>
                {
                    if (errorCallback != null)
                        Debug.WriteLine("RegisterNativeAsync error: " + errorCallback);
                });
            });
        }
    }
}