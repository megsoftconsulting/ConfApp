using System;
using System.Collections.Generic;
using System.Globalization;
using ImTools;

namespace ConfApp.Services.Telemetry
{
    public class UserCheckInOnVisitEvent : EventBase
    {
        public UserCheckInOnVisitEvent() : base("User Checked In on Visit")
        {
        }
    }

    public class EventBase
    {
        public EventBase(string name)
        {
            Name = name;
            TimeStamp = DateTimeOffset.Now;
        }

        public string Name { get; }
        public DateTimeOffset TimeStamp { get; }

        public Dictionary<string, string> Parameters { get; }
            = new Dictionary<string, string>();

        public EventBase AddParameter(string key, object value)
        {
            Parameters.Add(key, value.ToString());
            return this;
        }
    }
}