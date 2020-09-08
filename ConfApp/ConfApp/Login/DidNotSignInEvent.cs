using ConfApp.Services;
using ConfApp.Services.Telemetry;
using ConfApp.Services.Telemetry.Events;

namespace ConfApp.Login
{
    public class DidNotSignInEvent : EventBase
    {
        public DidNotSignInEvent() : base(EventTitles.DidNotSignIn)
        {
        }
    }
}