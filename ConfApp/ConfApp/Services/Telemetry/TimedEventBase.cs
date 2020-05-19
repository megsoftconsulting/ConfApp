using System.Diagnostics;
using System.Globalization;

namespace ConfApp.Services.Telemetry
{
    public class TimedEventBase : EventBase
    {
        private readonly Stopwatch _watch = new Stopwatch();

        public TimedEventBase(string name) : base(name)
        {
            _watch.Start();
        }

        public override string ToString()
        {
            return $"Event {Name} took {_watch.Elapsed.TotalSeconds} to complete.";
        }

        public void EventCompleted()
        {
            _watch.Stop();
            Parameters.Add("TimeElapsed", _watch.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
        }
    }
}