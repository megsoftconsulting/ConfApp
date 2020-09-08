using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ConfApp.Services;
using ConfApp.Services.Telemetry;
using ConfApp.ViewModels;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ConfApp.About
{
    public class AboutViewModel : TabViewModelBase
    {
        private readonly IEventHubProducer _client;
        private readonly IGeofencingService _geofenceService;
        private readonly IScreenshotService _screenshotService;
        private readonly ILocationService _locationService;

        private readonly CancellationTokenSource _token = new CancellationTokenSource();
        private ObservableCollection<Circle> _circles = new ObservableCollection<Circle>();
        private ObservableCollection<Polygon> _polygons = new ObservableCollection<Polygon>();
        private MapSpan _regionSPan;

        public AboutViewModel(INavigationService navigationService,
            IEventHubProducer client,
            ILocationService locationService,
            IAnalyticsService analyticsService,
            IGeofencingService geofenceService,
            IScreenshotService screenshotService) : base(navigationService, analyticsService)
        {
            _client = client;
            _locationService = locationService;
            _geofenceService = geofenceService;
            _screenshotService = screenshotService;
            Title = "About";
            //StartUpdatesCommand = new DelegateCommand(StartUpdates);
            AddRegionToMapCommand = new DelegateCommand(AddRegionToMap);
            GetCurrentLocationCommand = new DelegateCommand(GetCurrentLocation);
            TakeScreenshotCommand = new DelegateCommand(OnTakescreenshot);
        }

        public DelegateCommand TakeScreenshotCommand { get; set; }

        private void OnTakescreenshot()
        {
            var _ = _screenshotService.Capture();
        }

        public MapSpan RegionSpan
        {
            get => _regionSPan;
            set => SetProperty(ref _regionSPan, value);
        }

        public ObservableCollection<Polygon> Polygons
        {
            get => _polygons;
            set => SetProperty(ref _polygons, value);
        }

        public ObservableCollection<Circle> Circles
        {
            get => _circles;
            set => SetProperty(ref _circles, value);
        }

        public DelegateCommand GetCurrentLocationCommand { get; set; }

        public DelegateCommand AddRegionToMapCommand { get; set; }
        public DelegateCommand StartUpdatesCommand { get; set; }


        private async void GetCurrentLocation()
        {
            var l = await _locationService.GetCurrentLocationAsync();
            if (l == null) return;
            var p = new Position(l.Latitude, l.Longitude);
            var span = MapSpan.FromCenterAndRadius(p, Distance.FromMeters(50));
            RegionSpan = span;
        }

        private async void AddRegionToMap()
        {
            var l = await _locationService.GetCurrentLocationAsync();
            var id = DateTime.Now.Ticks.ToString();
            var r = 50;

            if (l == null) return;
            _geofenceService
                .StartMonitoring(
                    l.Latitude,
                    l.Longitude,
                    r,
                    id
                );

            var p = new Circle
            {
                FillColor = Color.Aqua,
                StrokeColor = Color.Blue,
                StrokeWidth = 2,
                Center = new Position(l.Latitude, l.Longitude),
                Radius = Distance.FromMeters(r)
            };
            p.MapElementId = id;

            Circles.Add(p);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            _geofenceService.Clear();
        }

        private async Task UpdateMyLocationPassively()
        {
            Debug.WriteLine("Starting to update location passively.");
            while (true)
            {
                Debug.WriteLine("Getting current Location");

                var l = await _locationService.GetCurrentLocationAsync();

                if (l != null)
                {
                    var place = await Geocoding.GetPlacemarksAsync(l);
                    if (place != null) Debug.WriteLine($"You are at {place.First()}");
                    Debug.WriteLine("Sending Heartbeat");
                    _client.SendAsync(new HeartBeatMessage("Claudio's iPhone", l));
                }

                Thread.Sleep(5000);
            }
        }
    }
}