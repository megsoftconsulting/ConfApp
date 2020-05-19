using System.Diagnostics;
using System.Linq;
using ConfApp.Services;
using CoreLocation;
using ImTools;

namespace ConfApp.iOS.Services
{
    public class GeofencingService : IGeofencingService
    {
        private readonly CLLocationManager _locationManager;

        public GeofencingService()
        {
            // At some point, do introduce a way to get access to the singleton.
            _locationManager = new CLLocationManager
            {
                ActivityType = CLActivityType.Fitness,
                DesiredAccuracy = 1
            };
            _locationManager.DidStartMonitoringForRegion += OnDidStartMonitoringForRegion;
            _locationManager.RegionEntered += OnRegionEntered;
            _locationManager.RegionLeft += OnRegionLeft;
        }

        public void Clear()
        {
            foreach (var r in _locationManager.MonitoredRegions) _locationManager.StopMonitoring(r as CLRegion);
        }

        public void StopMonitoring(string id)
        {
            var regions = _locationManager.MonitoredRegions;
       
            _locationManager.StopMonitoring(regions.ElementAt(0) as CLRegion);
        }

        public void StartMonitoring(
            double latitude,
            double longitude,
            double radius,
            string id)
        {
            var r = new CLCircularRegion(new CLLocationCoordinate2D(latitude, longitude),
                radius, id)
            {
                NotifyOnEntry = true,
                NotifyOnExit = true
            };
            _locationManager.StartMonitoring(r);
        }

        private void OnRegionLeft(object sender, CLRegionEventArgs e)
        {
            Debug.WriteLine($"Left Region {e.Region.Identifier}");
        }

        private void OnRegionEntered(object sender, CLRegionEventArgs e)
        {
            Debug.WriteLine($"Entered Region {e.Region.Identifier}");
        }

        private void OnDidStartMonitoringForRegion(object sender, CLRegionEventArgs e)
        {
            Debug.WriteLine($"Started Monitoring Region {e.Region.Identifier}");
        }
    }
}