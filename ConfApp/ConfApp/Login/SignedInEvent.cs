using System;
using ConfApp.Services;
using ConfApp.Services.Telemetry;

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