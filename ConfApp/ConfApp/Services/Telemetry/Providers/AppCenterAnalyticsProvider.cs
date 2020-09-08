using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ConfApp.Services.Telemetry.Events;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace ConfApp.Services.Telemetry.Providers
{
    public class AppCenterAnalyticsProvider : IAnalyticsProvider
    {
        public void SetCurrentUser(string name, string id)
        {
            AppCenterSetCurrentUser(name, id);
        }

        public void TrackEvent(EventBase @event)
        {
            AppCenterTrack(@event);
        }

        void IAnalyticsProvider.TrackSuperProperty<TValue>(string name, TValue value)
        {
            AppCenterTrackSuperProperty(name, value);
        }

        public void TrackError(Exception ex, IDictionary<string, object> parameters = null)
        {
            AppCenterTrackError(ex);
        }

        public void ClearSuperProperty(string key)
        {
            AppCenterClearSuperProperty(key);
        }
       
        private void AppCenterClearSuperProperty(string key)
        {
            var cp = new CustomProperties();
            cp.Clear(key);
            AppCenter.SetCustomProperties(cp);
        }
        private void AppCenterTrackSuperProperty<TValue>(string name, TValue value)
        {
            var cp = new CustomProperties();
            var t = typeof(TValue);

            try
            {
                if (t == typeof(double)) cp.Set(name, Convert.ToInt16(value));
                if (t == typeof(float)) cp.Set(name, Convert.ToSingle(value));
                if (t == typeof(int)) cp.Set(name, Convert.ToInt32(value));
                if (t == typeof(long)) cp.Set(name, Convert.ToInt64(value));
                if (t == typeof(string)) cp.Set(name, Convert.ToString(value));
                if (t == typeof(bool)) cp.Set(name, Convert.ToBoolean(value));
                if (t == typeof(decimal)) cp.Set(name, Convert.ToDecimal(value));
                if (t == typeof(DateTime)) cp.Set(name, Convert.ToDateTime(value));
            }
            catch (Exception)
            {
                Debug.WriteLine($"SuperProperty Type not supported. Will ignore Property {name}, of type {t}");
            }

            AppCenter.SetCustomProperties(cp);
        }


        private async void AppCenterTrack(EventBase @event)
        {
            if (!await Analytics.IsEnabledAsync())
            {
                Debug.WriteLine("AppCenter Analytics is not enabled.");
                return;
            }

            var p = new Dictionary<string, string>();

            @event
                .Parameters
                .ToList()
                .ForEach(i => p.Add(i.Key, i.Value.ToString()));

            Analytics.TrackEvent(@event.Name, @event.Parameters.Count > 0 ? p : null);
        }

        private void AppCenterSetCurrentUser(string name, string id)
        {
            var cp = new CustomProperties();
            cp.Set("UserName", name);
            AppCenter.SetUserId(id);
            AppCenter.SetCustomProperties(cp);
        }

        private static void AppCenterTrackError(Exception ex)
        {
            Crashes.TrackError(ex,
                null,
                ErrorAttachmentLog
                    .AttachmentWithText("{Sample error data}", "fakelog.txt"));
        }
    }
}