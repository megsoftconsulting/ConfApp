using ConfApp.Services.Telemetry.Events;

namespace ConfApp.Services.Telemetry.Events
{
    public class AppLoadedEvent : TimedEventBase
    {
        public AppLoadedEvent() : base(EventTitles.ApplicationLoadedSuccessfully)
        {
        }
    }
}