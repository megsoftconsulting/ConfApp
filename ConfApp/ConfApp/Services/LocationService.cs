using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ConfApp.Services
{
    public interface ILocationService
    {
        Task<PermissionStatus> RequestPermissions();
        Task<Location> GetCurrentLocationAsync();
    }

    public class LocationService : ILocationService
    {
        public async Task<PermissionStatus> RequestPermissions()
        {
                var status = PermissionStatus.Unknown;
                status = await Permissions
                    .RequestAsync<Permissions.LocationAlways>();
                return status;

        }

        public async Task<Location> GetCurrentLocationAsync()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best,
                TimeSpan.FromSeconds(1));
            try
            {
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                    //Console.WriteLine(
                    //   $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    return location;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
            return null;
        }
    }
}