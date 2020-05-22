using ConfApp.Services.Telemetry;
using ConfApp.ViewModels;
using Prism.Navigation;

namespace ConfApp.Sync
{
    public class SyncViewModel : ViewModelBase
    {
        public SyncViewModel(INavigationService navigationService, 
            ITelemetryService telemetryService) : base(
            navigationService, telemetryService)
        {
        }
    }
}