using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ConfApp.Services
{
    public interface ILocationService
    {
        Task<PermissionStatus> RequestPermissions();
        Task<Location> GetCurrentLocationAsync();
        Task<Location> GetLastKnownLocationAsync();
    }

    public class LocationService : ILocationService
    {
        private readonly double _millisecondsToWaitForALocation = 500;

        public async Task<PermissionStatus> RequestPermissions()
        {
            var status = PermissionStatus.Unknown;

            try
            {
                status = await Permissions
                    .RequestAsync<Permissions.LocationAlways>();
            }
            catch (Exception ex)
            {
                //_telemetryService.TrackError(ex);
                Debug.WriteLine(ex);
            }

            return status;
        }

        public async Task<Location> GetCurrentLocationAsync()
        {
            var grantedAlways = await Permissions.CheckStatusAsync<Permissions.LocationAlways>() ==
                                PermissionStatus.Granted;
            var grantedWHenInUse = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>() ==
                                   PermissionStatus.Granted;

            if (!grantedWHenInUse && !grantedAlways) return null;

            var request = new GeolocationRequest(
                GeolocationAccuracy.Best,
                TimeSpan.FromMilliseconds(_millisecondsToWaitForALocation)
            );
            try
            {
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                    return location;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                //_telemetryService.TrackError(fnsEx);
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                // _telemetryService.TrackError(fneEx);
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                // _telemetryService.TrackError(pEx);
            }
            catch (Exception ex)
            {
                // Unable to get location
                //_telemetryService.TrackError(ex);
            }

            return null;
        }

        public async Task<Location> GetLastKnownLocationAsync()
        {
            var location = new Location(double.NaN, double.NaN, DateTimeOffset.Now);
            try
            {
                location = await Geolocation.GetLastKnownLocationAsync();

                var offset = location.Timestamp - DateTime.Now;
                Debug.WriteLine($" The Known Last Location is {offset.TotalMilliseconds} Milliseconds old");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($" Encountered an error while getting Last Known Location.{ex}");
                // Log the error
            }

            return location;
        }
    }
}