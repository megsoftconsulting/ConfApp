using System;
using System.Collections.Generic;

namespace ConfApp.Services.Telemetry
{
    public interface ITelemetryProvider
    {
        void SetCurrentUser(string name, string id);
        void TrackEvent(EventBase @event);
        void TrackError(Exception ex, IDictionary<string, object> parameters = null);
        void TrackSuperProperty<TValue>(string name, TValue value);
        void ClearSuperProperty(string key);
    }
}