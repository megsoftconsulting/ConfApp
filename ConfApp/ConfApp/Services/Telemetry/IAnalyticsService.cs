using System;
using System.Collections.Generic;
using ConfApp.Services.Telemetry.Events;

namespace ConfApp.Services.Telemetry
{
    public interface IAnalyticsService
    {
        void TrackEvent(EventBase @event);
        void SetCurrentUser(string name, string id);
        void TrackError(Exception ex, IDictionary<string, object> parameters = null);
        void TrackSuperProperty<TValue>(string name, TValue value) where TValue : struct;
        void ClearSuperProperty(string key);
    }
}