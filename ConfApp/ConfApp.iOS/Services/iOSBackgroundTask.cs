using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Prism.Events;
using UIKit;

namespace ConfApp.iOS.Services
{
    // ReSharper disable once InconsistentNaming
    public class iOSBackgroundTask
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly string _taskName;
        private CancellationTokenSource _cts;
        private Func<CancellationToken, Task> _run;
        private bool _runForever;

        public iOSBackgroundTask(IEventAggregator eventAggregator = null)
        {
            _eventAggregator = eventAggregator;
            _taskName = nameof(iOSBackgroundTask); //Perhaps later use <T> as the 
        }

        ///private ITheThingThatRuns _runner;
        public nint TaskId { get; private set; }

        public virtual async Task Start(
            Func<CancellationToken, Task> run,
            Action whenItExpires,
            bool runForever = false)
        {
            _run = run;
            _runForever = runForever;

            _cts = new CancellationTokenSource();

            TaskId = UIApplication
                .SharedApplication
                .BeginBackgroundTask(_taskName, ExpirationHandler);
            Debug.WriteLine($"iOS Assigned me a task ID of {TaskId}");
            try
            {
                Debug.WriteLine("Starting to execute.");
                await _run(_cts.Token);
            }
            catch (OperationCanceledException exception)
            {
                Debug.WriteLine("The task just got cancelled.");
            }
            finally
            {
                if (_cts.IsCancellationRequested)
                {
                    Debug.WriteLine($"Cancellation requested for Task {TaskId}");
                    whenItExpires?.Invoke();
                }
            }

            EndBackgroundTask();
        }

        public void Stop()
        {
            Debug.WriteLine("Task being canceled by the Caller - by Calling Stop()");
            _cts.Cancel();
        }

        private void EndBackgroundTask()
        {
            UIApplication.SharedApplication.EndBackgroundTask(TaskId);
        }

        private void ExpirationHandler()
        {
            if (!_runForever) return;

            var id = UIApplication.SharedApplication.BeginBackgroundTask("DummyTask",
                () => { Debug.Write("DummyTask time is ending. ExpirationHandler being called by the OS."); });
            UIApplication.SharedApplication.EndBackgroundTask(id);
        }
    }
}