using ConfApp.Services;
using ConfApp.Services.Telemetry;

namespace ConfApp.Login
{
    public class DidNotSignInEvent : EventBase
    {
        public DidNotSignInEvent() : base("Did not Sign In")
        {
        }
    }
}