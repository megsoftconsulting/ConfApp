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
                    .RequestAsync<Permissions.LocationWhenInUse>();
            }
            catch (Exception ex)
            {
                //AnalyticsService.TrackError(ex);
                Debug.WriteLine(ex);
            }

            return status;
        }

        public async Task<Location> GetCurrentLocationAsync()
        {
            try
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

                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                    return location;
            }

            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                //AnalyticsService.TrackError(fnsEx);
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                // AnalyticsService.TrackError(fneEx);
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                // AnalyticsService.TrackError(pEx);

            }
            catch (NullReferenceException nre)
            {
                Debug.WriteLine(nre);
            }
            catch (Exception ex)
            {
                // Unable to get location
                //AnalyticsService.TrackError(ex);
                Debug.WriteLine(ex);
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
            catch (NullReferenceException ex)
            {
              // TODO: This is most likely because we are running on the simulator. Ignore it.
            }

            return location;
        }
    }
}