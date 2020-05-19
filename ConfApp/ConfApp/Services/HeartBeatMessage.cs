using Xamarin.Essentials;

namespace ConfApp.Services
{
    public class HeartBeatMessage
    {
        public HeartBeatMessage(string deviceId, Location location)
        {
            DeviceId = deviceId;
            Location = location;
        }

        public string DeviceId { get; }
        public Location Location { get; }
    }
}