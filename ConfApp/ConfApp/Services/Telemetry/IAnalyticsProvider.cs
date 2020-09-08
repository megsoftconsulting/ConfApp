using System;
using System.Collections.Generic;
using ConfApp.Services.Telemetry.Events;

namespace ConfApp.Services.Telemetry
{
    public interface IAnalyticsProvider
    {
        void SetCurrentUser(string name, string id);
        void TrackEvent(EventBase @event);
        void TrackError(Exception ex, IDictionary<string, object> parameters = null);
        void TrackSuperProperty<TValue>(string name, TValue value);
        void ClearSuperProperty(string key);
    }
}