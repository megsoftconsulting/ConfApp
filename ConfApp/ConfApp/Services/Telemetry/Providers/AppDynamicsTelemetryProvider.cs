using System;
using System.Collections.Generic;

namespace ConfApp.Services.Telemetry.Providers
{
    public class AppDynamicsTelemetryProvider : ITelemetryProvider
    {
        public void SetCurrentUser(string name, string id)
        {
            // throw new NotImplementedException();
        }

        public void TrackEvent(EventBase @event)
        {
            // throw new NotImplementedException();
        }

        public void TrackError(Exception ex, IDictionary<string, object> parameters = null)
        {
            // throw new NotImplementedException();
        }

        public void TrackSuperProperty<TValue>(string name, TValue value)
        {
            //throw new NotImplementedException();
        }

        public void ClearSuperProperty(string key)
        {
            throw new NotImplementedException();
        }
    }
}