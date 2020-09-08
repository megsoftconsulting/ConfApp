using System;
using System.Collections.Generic;

namespace ConfApp.Services.Telemetry.Events
{
    public class EventBase
    {
        public EventBase(string name)
        {
            Name = name;
            TimeStamp = DateTimeOffset.Now;
        }

        public string Name
        {
            get => GetValue<string>(nameof(Name));
            set => SetValue(nameof(Name), value);
        }

        public DateTimeOffset TimeStamp
        {
            get => GetValue<DateTimeOffset>(nameof(TimeStamp));
            set => SetValue(nameof(TimeStamp), value);
        }

        public Dictionary<string, object> Parameters { get; }
            = new Dictionary<string, object>();


        protected void SetValue<TValue>
            (string propertyName, TValue value)
        {
            if (!Parameters.ContainsKey(propertyName))
                Parameters.Add(propertyName, value);
            else
                Parameters[propertyName] = value;
        }


        protected TValue GetValue<TValue>(string propertyName)
        {
            if (Parameters.TryGetValue(propertyName, out var value))
                return (TValue) value;
            return default;
        }


        public EventBase AddParameter<TValue>(string key, TValue value)
        {
            Parameters.Add(key, value);
            return this;
        }
    }
}