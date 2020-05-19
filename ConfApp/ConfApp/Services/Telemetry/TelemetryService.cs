using System;
using System.Collections.Generic;
using ConfApp.Services.Telemetry.Providers;

namespace ConfApp.Services.Telemetry
{
    public class TelemetryService : ITelemetryService
    {
        private readonly ILocationService _locationService;

        private readonly List<ITelemetryProvider> _providers = new List<ITelemetryProvider>
        {
            new AppCenterTelemetryProvider(),
            new AppDynamicsTelemetryProvider()
        };

        public TelemetryService(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async void TrackEvent(EventBase @event)
        {
            var lastKnownLocation = await _locationService.GetLastKnownLocationAsync();
            if (lastKnownLocation != null)
                @event.AddParameter("Latitude", lastKnownLocation.Latitude)
                    .AddParameter("Longitude", lastKnownLocation.Longitude);
            // TODO: Consider optimizing it to do this in parallel.
            _providers.ForEach(p => p.TrackEvent(@event));
        }

        public void SetCurrentUser(string name, string id)
        {
            _providers.ForEach(p => p.SetCurrentUser(name, id));
        }

        public void TrackError(Exception ex, IDictionary<string, object> parameters)
        {
            _providers.ForEach(p => p.TrackError(ex));
        }

        public void TrackSuperProperty<TValue>(string name, TValue value) where TValue : struct
        {
            _providers.ForEach(p => p.TrackSuperProperty(name, value));
        }

        public void ClearSuperProperty(string key)
        {
            _providers.ForEach(p => p.ClearSuperProperty(key));
        }
    }
}