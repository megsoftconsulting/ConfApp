using System;
using ConfApp.Services;
using ConfApp.Services.Telemetry;
using ConfApp.Services.Telemetry.Events;

namespace ConfApp.Login
{
    public class SignedInEvent: EventBase
    {
        public SignedInEvent(string providerName) : base("Signed In")
        {
            AddParameter(nameof(providerName), providerName);
        }

       
    }
}